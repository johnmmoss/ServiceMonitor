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
using Web.Models;

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

        public async Task<IActionResult> Sources()
        {
            _logger.LogInformation("Loading vsts projects...");

            var tfsProjects = _tfsProjectsRepository.GetAll();
            var tfsProjectsModel = new TfsProjectsModel();
            tfsProjectsModel.TfsProjects = new List<TfsProjectModel>();

            foreach(var tfsProject in tfsProjects)
            {
                var tfsProjectModel = new TfsProjectModel();
                tfsProjectModel.Name = tfsProject.Name;
                tfsProjectModel.PipelineInfoModels = await LoadPipelineInfos(tfsProject);
                tfsProjectsModel.TfsProjects.Add(tfsProjectModel);
            }

            return Json(tfsProjectsModel);
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

        private async Task<List<PipelineInfoModel>> LoadPipelineInfos(TfsProject tfsProject)
        {
            var pipelineInfoModels = new List<PipelineInfoModel>();
            try
            {
                foreach (var pipelineInfo in tfsProject.PipelineInfos)
                {
                    var pipelineInfoModel = new PipelineInfoModel();

                    pipelineInfoModel.Name = pipelineInfo.Name;
                    pipelineInfoModel.QaUrl = pipelineInfo.QaUrl;
                    pipelineInfoModel.IntegrationUrl = pipelineInfo.IntegrationUrl;
                    pipelineInfoModel.Build = await GetBuildModel(tfsProject.Name, pipelineInfo.BuildDefinitionId);
                    pipelineInfoModel.ReleaseIntegration = await GetEnvironmentOneReleaseModel(tfsProject.Name, pipelineInfo.ReleaseDefinitionId);
                    pipelineInfoModel.ReleaseQa = await GetEnvironmentTwoReleaseModel(tfsProject.Name,pipelineInfo.ReleaseDefinitionId);
                    pipelineInfoModel.IntegrationUp = await PingAsync(pipelineInfo.IntegrationUrl);
                    pipelineInfoModel.QaUp = await PingAsync(pipelineInfo.QaUrl);

                    pipelineInfoModels.Add(pipelineInfoModel);
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return pipelineInfoModels;
        }

        private async Task<IList<BuildModel>> GetBuildModel(string projectName, int definitionId)
        {
            var builds = await _tfsRepository.GetTfsBuildsAsync(projectName, definitionId);

            var model = builds.Select(x => new BuildModel()
            {
                Status = x.status,
                Result = x.result,
                Finished = x.finishTime,
                Number = x.buildNumber
            });

            return model.OrderByDescending(x => x.Finished).ToList();
        }

        private async Task<ReleaseModel> GetEnvironmentOneReleaseModel(string projectName, int definitionId)
        {
            var releases = await _tfsRepository.GetTfsReleaseAsync(projectName, definitionId);
            var current = releases.FirstOrDefault();

            if(current != null && current.environments != null)
            {
                var environment = current.environments
                    .Find(x => x.name.ToLower() == ENVIRONMENT_ONE_NAME.ToLower() 
                            && x.status.ToLower() != "notstarted");

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

        // https://docs.microsoft.com/en-us/rest/api/azure/devops/release/releases/update%20release%20environment?view=azure-devops-rest-5.0#environmentstatus
        private async Task<ReleaseModel> GetEnvironmentTwoReleaseModel(string projectName, int definitionId)
        {
            var releases = await _tfsRepository.GetTfsReleaseAsync(projectName, definitionId);
            var release = releases.FirstOrDefault( x=> x.environments.
                                    Where(y => y.name.ToLower() == ENVIRONMENT_TWO_NAME.ToLower() 
                                        &&  (y.status.ToLower() == "succeeded"
                                            || y.status.ToLower() == "rejected"))
                                    .Any());

            if (release != null)
            {
                var releaseModel = new ReleaseModel();
                var environment = release.environments.First(x => x.name.ToLower() == ENVIRONMENT_TWO_NAME.ToLower());
                releaseModel.Status= environment.status;
                releaseModel.Name = release.name.Contains("-") ? release.name.Split('-')[1] : release.name;
                return releaseModel;
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
