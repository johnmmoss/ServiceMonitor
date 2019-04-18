using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceMonitor.Data;
using ServiceMonitor.Web.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tfs.Client;
using TfsClient;

namespace ServiceMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        const string ENVIRONMENT_ONE_NAME = "Dev";
        const string ENVIRONMENT_TWO_NAME = "UAT";

        private TfsRepository _tfsRepository;
        private TfsClientOptions _tfsOptions;
        private TfsProjectsRepository _tfsProjectsRepository;
        private ILogger _logger;

        public HomeController(
            TfsProjectsRepository apiSourceRepository,
            IOptions<TfsClientOptions> tfsOptions,
            ILogger<HomeController> logger)
        {
            _tfsOptions = tfsOptions.Value;
            _tfsProjectsRepository = apiSourceRepository;
            _tfsRepository = new TfsRepository(_tfsOptions);
            _logger = logger;
        }

        public IActionResult Index()
        {
            var url = $"{Request.Scheme}://{Request.Host.Value}";

            return View(new IndexModel() 
            { 
                HostUrl = url,
                PageTitle = $"{_tfsOptions.Instance}"

            });
        }

        public IActionResult Sources()
        {
            _logger.LogInformation("Loading sources...");

            var collection = new ConcurrentBag<SourceItemModel>(); 
            var tfsProjects = _tfsProjectsRepository.GetAll();

            Task.WaitAll(tfsProjects.Select((item) => Load(collection, item)).ToArray());
               
            var items = new List<SourceItemModel>();
            items = collection.ToList();
            items = items.OrderBy(x => x.Name).ToList();

            return Json(new SourceModel()
            {
                Items = items
            });
        }

        public async Task<IActionResult> PullRequests()
        {
            var pullRequests = await _tfsRepository.GetTfsPullRequestAsync();
            var zenithPullRequests = pullRequests
                    .Where(x => x.repository.name != "project-zen")
                    .OrderBy(x => x.repository.name).ThenBy(x => x.creationDate)
                    .ToList();

            var tfsUrl = new TfsUrl(_tfsOptions.Instance);
            return Json(zenithPullRequests.Select(x=> new PullRequestModel()
            {
                Name = x.title,
                RepositoryName = x.repository.name,
                CreatedBy = x.createdBy.displayName,
                CreatedDate = x.creationDate.ToString("dddd dd - HH:mm"),
                ZenithDevCodeReview = x.reviewers.Where(z => z.displayName == @"[project-zen]\ZenithCodeReviewers").Select(y => new ReviewersModel() { Status = ConvertToStatus(y.vote), Description = y.displayName }).First(),
                ZenithDevReview = x.reviewers.Where(z => z.displayName == @"[project-zen]\ZenithDev").Select(y => new ReviewersModel() { Status = ConvertToStatus(y.vote), Description = y.displayName }).First(),
                Url = tfsUrl.BuildPullRequestUrl(x.repository.name, x.pullRequestId) 
            }));
        }
        private string ConvertToStatus(int vote)
        {
            switch (vote)
            {
                case -10:
                    return "Rejected";
                case -5:
                    return "Waiting for author";
                case 5:
                    return "Approved with suggestions";
                case 10:
                    return "Approved";
                default: // 0
                    return "No Response";
            }
        }
        private async Task Load(ConcurrentBag<SourceItemModel> collection, TfsProject tfsProject)
        {
            try{
                foreach (var pipelineInfo in tfsProject.PipelineInfos)
                {
                    var modelItem = new SourceItemModel();

                    modelItem.Name = pipelineInfo.Name;
                    modelItem.QaUrl = pipelineInfo.QaUrl;
                    modelItem.IntegrationUrl = pipelineInfo.IntegrationUrl;
                    modelItem.Build = await GetBuildModel(tfsProject.Name, pipelineInfo.BuildDefinitionId);
                    modelItem.ReleaseIntegration = await GetEnvironmentOneReleaseModel(tfsProject.Name, pipelineInfo.ReleaseDefinitionId);
                    modelItem.ReleaseQa = await GetEnvironmentTwoReleaseModel(tfsProject.Name,pipelineInfo.ReleaseDefinitionId);
                    modelItem.IntegrationUp = await PingAsync(pipelineInfo.IntegrationUrl);
                    modelItem.QaUp = await PingAsync(pipelineInfo.QaUrl);

                    _logger.LogInformation($"Loading {pipelineInfo.Name} complete!");
                    collection.Add(modelItem);
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError("1", ex);
            }
        }

      private async Task<IList<BuildModel>> GetBuildModel(string projectName, int definitionId)
        {
            var builds = await _tfsRepository.GetTfsBuildsAsync(projectName, definitionId);

            return builds.Select(x => new BuildModel()
            {
                Status = x.status,
                Result = x.result,
                Finished = x.finishTime,
                Number = x.buildNumber
            }).OrderByDescending(x => x.Finished).ToList();
        }

        private async Task<ReleaseModel> GetEnvironmentOneReleaseModel(string projectName, int definitionId)
        {
            var releases = await _tfsRepository.GetTfsReleaseAsync(projectName, definitionId);
            var current = releases.FirstOrDefault();

            if(current != null && current.environments != null)
            {
                var environment = current.environments.Find(x => x.name.ToLower() == ENVIRONMENT_ONE_NAME.ToLower() && x.status.ToLower() == "succeeded");
                if (environment != null)
                {
                    var environmentOne = new ReleaseModel();
                    environmentOne.Status= environment.status;
                    environmentOne.Name = current.name.Contains("-") ? current.name.Split('-')[1] : current.name;
                    return environmentOne;
                }
            }
            return null;
        }

        // succeeded, notStarted, inProgress
        private async Task<ReleaseModel> GetEnvironmentTwoReleaseModel(string projectName, int definitionId)
        {
            var releases = await _tfsRepository.GetTfsReleaseAsync(projectName, definitionId);
            var environmentTwo = releases.FirstOrDefault( x=> x.environments.
                Where(y => y.name.ToLower() == ENVIRONMENT_TWO_NAME.ToLower() && y.status.ToLower() == "succeeded")
                .Any());

            if (environmentTwo != null)
            {
                var environmentOneModel = new ReleaseModel();
                var qaEnvironment = environmentTwo.environments.First(x => x.name.ToLower() == ENVIRONMENT_TWO_NAME.ToLower());
                environmentOneModel.Status= qaEnvironment.status;
                environmentOneModel.Name = environmentTwo.name.Contains("-") ? environmentTwo.name.Split('-')[1] : environmentTwo.name;
                return environmentOneModel;
            }

            return null;
        }

        private async Task<bool> PingAsync(string url)
        {
            using(var httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage;
                try
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    responseMessage = await httpClient.GetAsync(url);
                    return responseMessage.StatusCode == HttpStatusCode.OK;
                }
                catch
                {
                    return false;
                }
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
