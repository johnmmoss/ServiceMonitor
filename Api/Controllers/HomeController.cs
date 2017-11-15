using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiPinger.Models;
using System.Net.Http;
using System.Net;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TfsClient;
using Microsoft.Extensions.Options;
using Tfs.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace ApiPinger.Controllers
{
    public class HomeController : Controller
    {
        private TfsRepository _tfsRepository;
        private TfsClientOptions _tfsOptions;
        private ApiSourceRepository _apiSourceRepository;
        private ILogger _logger;

        public HomeController(
            ApiSourceRepository apiSourceRepository,
            IOptions<TfsClientOptions> tfsOptions,
            ILogger<HomeController> logger)
        {
            _tfsOptions = tfsOptions.Value;
            _apiSourceRepository = apiSourceRepository;
            _tfsRepository = new TfsRepository(_tfsOptions);
            _logger = logger;
        }

        public IActionResult Index()
        {
            var url = $"{Request.Scheme}://{Request.Host.Value}";

            return View(new IndexModel() { HostUrl = url });
        }

        public IActionResult Sources()
        {
            _logger.LogInformation("Loading sources...");

            var collection = new ConcurrentBag<SourceItemModel>(); 
            var apiSources = _apiSourceRepository.GetAll() as List<ApiSource>;

            Task.WaitAll(apiSources.Select((item) => Load(collection, item)).ToArray());
               
            var items = new List<SourceItemModel>();
            items = collection.ToList();
            items.OrderBy(x => x.Name);

            return Json(new SourceModel()
            {
                Items = items
            });
        }
        private async Task Load(ConcurrentBag<SourceItemModel> collection, ApiSource apiSource)
        {
            try{
                    _logger.LogInformation($"Loading {apiSource.Name}...");
                    var modelItem = new SourceItemModel();

                    modelItem.Name = apiSource.Name;
                    modelItem.QaUrl = apiSource.QaUrl;
                    modelItem.IntegrationUrl = apiSource.IntegrationUrl;
                    modelItem.Build = await GetBuildModel(apiSource.BuildDefinitionId);
                    //modelItem.Release = await GetReleaseModel(apiSource.BuildDefinitionId);
                    modelItem.IntegrationUp = await PingAsync(apiSource.IntegrationUrl);
                    modelItem.QaUp = await PingAsync(apiSource.QaUrl);

                    _logger.LogInformation($"Loading {apiSource.Name} complete!");
                    collection.Add(modelItem);
            } 
            catch (Exception ex)
            {
                _logger.LogError("1", ex);
            }
        }

        [HttpGet]
        public async Task<bool> PingIntegration(int id)
        {
            var apiSource = _apiSourceRepository.Get(id);

            using (var httpClient = new HttpClient())
            {
                return await PingAsync(apiSource.IntegrationUrl);
            }
        }

        [HttpGet]
        public async Task<bool> PingQa(int id)
        {
            var apiSource = _apiSourceRepository.Get(id);

            return await PingAsync(apiSource.QaUrl);
        }

        [HttpGet]
        public async Task<BuildModel> Build(int definitionId)
        {
            var builds = await GetBuildModel(definitionId);

            return builds.First();
        }

        private async Task<IList<BuildModel>> GetBuildModel(int definitionId)
        {
            var builds = await _tfsRepository.GetTfsBuildsAsync(definitionId);

            return builds.Select(x => new BuildModel()
            {
                Status = x.status,
                Result = x.result,
                Finished = x.finishTime,
                Number = x.buildNumber
            }).OrderByDescending(x => x.Finished).ToList();
        }

        private async Task<IList<ReleaseModel>> GetReleaseModel(int definitionId)
        {
            var release = await _tfsRepository.GetTfsReleaseAsync(definitionId);
            return release.environments.Select(x => new ReleaseModel()
            {
                Status = x.status,
                CreatedOn = x.createdOn,
                Name = x.name
            }).OrderByDescending(x => x.Name).ToList();
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
