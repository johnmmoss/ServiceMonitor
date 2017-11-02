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

namespace ApiPinger.Controllers
{
    public class HomeController : Controller
    {
        private TfsRepository _tfsRepository;
        private TfsClientOptions _tfsOptions;
        private ApiSourceRepository _apiSourceRepository;

        public HomeController(
            ApiSourceRepository apiSourceRepository,
            IOptions<TfsClientOptions> tfsOptions)
        {
            _tfsOptions = tfsOptions.Value;
            _apiSourceRepository = apiSourceRepository;
            _tfsRepository = new TfsRepository(_tfsOptions);
        }

        public IActionResult Index()
        {
            var url = $"{Request.Scheme}://{Request.Host.Value}";

            return View(new IndexModel() { HostUrl = url });
        }

        public async Task<IActionResult> Sources()
        {
            var model = new SourceModel();
            model.Items = new List<SourceItemModel>();
            using (var httpClient = new HttpClient())
            {
                foreach (var apiSource in _apiSourceRepository.GetAll())
                {
                    var modelItem = new SourceItemModel();

                    modelItem.Name = apiSource.Name;
                    modelItem.QaUrl = apiSource.QaUrl;
                    modelItem.IntegrationUrl = apiSource.IntegrationUrl;
                    modelItem.Build = await GetBuildModel(apiSource.BuildDefinitionId);
                    modelItem.Release = await GetReleaseModel(apiSource.BuildDefinitionId);
                    modelItem.IntegrationUp = await PingAsync(httpClient, apiSource.IntegrationUrl);
                    modelItem.QaUp = await PingAsync(httpClient, apiSource.IntegrationUrl);

                    model.Items.Add(modelItem);
                }
            }

            return Json(model);
        }

        [HttpGet]
        public async Task<bool> PingIntegration(int id)
        {
            var apiSource = _apiSourceRepository.Get(id);

            using(var httpClient = new HttpClient())
            {
                return await PingAsync(httpClient, apiSource.IntegrationUrl);
            }
        }

        [HttpGet]
        public async Task<bool> PingQa(int id)
        {
            var apiSource = _apiSourceRepository.Get(id);

            using(var httpClient = new HttpClient())
            {
                return await PingAsync(httpClient, apiSource.QaUrl);
            }
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
            var releases = await _tfsRepository.GetTfsReleasesAsync(definitionId);
            return releases.Select(x => new ReleaseModel()
            {
                Status = x.status,
                CreatedOn = x.createdOn,
                Name = x.name
            }).OrderByDescending(x => x.Name).ToList();
        }

        private async Task<bool> PingAsync(HttpClient httpClient, string url)
        {
            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await httpClient.GetAsync(url);
                return responseMessage.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
