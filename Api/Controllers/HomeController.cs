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
                    modelItem.IntegrationUp = await CheckStatusAsync(httpClient, apiSource.IntegrationUrl);
                    modelItem.QaUp = await CheckStatusAsync(httpClient, apiSource.IntegrationUrl);

                    model.Items.Add(modelItem);
                }
            }

            return Json(model);
        }

        public async Task<IList<BuildModel>> GetBuildModel(int definitionId)
        {
            var builds = await _tfsRepository.GetTfsBuildsAsync(definitionId);

            return builds.Select(x => new BuildModel()
            {
                Status = x.status,
                Result = x.result,
                Finished = x.finishTime
            }).OrderByDescending(x => x.Finished).ToList();
        }

        public async Task<IList<ReleaseModel>> GetReleaseModel(int definitionId)
        {
            var releases = await _tfsRepository.GetTfsReleasesAsync(definitionId);
            return releases.Select(x => new ReleaseModel()
            {
                Status = x.status,
                CreatedOn = x.createdOn,
                Name = x.name
            }).OrderByDescending(x => x.Name).ToList();
        }

        private async Task<bool> CheckStatusAsync(HttpClient httpClient, string url)
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
