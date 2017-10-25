    using System;
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

    namespace ApiPinger.Controllers
    {

        public class ApiSource
        {
            public ApiSource(string name, string integration, string qa)
            {
                Name = name;
                Integration = integration;
                QA = qa;
            }
            public string Name { get; private set; }
            public string QA { get; private set; }
            public string Integration { get; private set; }
        }
        public class IndexModel
        {
            public string HostUrl { get; set; }
        }
        public class SourceModel
        {
            public IList<SourceItemModel> Items { get; set; }
        }

        public class SourceItemModel
        {
            public string Name { get; set; }
            public bool QAUp { get; set; }
            public bool IntegrationUp { get; set; }
            public string IntegrationUrl { get; set; }
            public string QArl { get; set; }
        }
        public class HomeController : Controller
        {
            public IList<ApiSource> GetApiSources()
            {
                return new List<ApiSource>()
                {
                    new ApiSource("Site 1", "http://www.google.co.uk", "http://www.google.co.uk"),
                    new ApiSource("Site 2", "http://www.google.co.uk", "http://www.google.co.uk"),
                    new ApiSource("Site 3", "http://www.google.co.uk", "http://www.google.co.uk"),
                    new ApiSource("Site 4", "http://www.google.co.uk", "http://www.google.co.uk")
                };
            }


        public async Task<T> Get<T>(string url)
        {
            var personalaccesstoken = "";

            using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(responseBody);
                    }
                }
        }
        int SHOWROOM_REL_ID = 11;
        int SHOWROOM_DEF_ID = 12;
        int IDENTITY_DEF_ID = 11; 
        int VEHICLE_DEF_ID = 1; 
        string buildUrls = "https://<accountname>.visualstudio.com/DefaultCollection/project-zen/_apis/build/builds?api-version=2.";
        string releaseUrls = "https://<accountname>.vsrm.visualstudio.com/DefaultCollection/project-zen/_apis/release/releases?api-version=4.0-preview.4";
            public async Task<IActionResult> Index()
            {
                var showroomBuild = GetBuildModels(SHOWROOM_DEF_ID).First();
                var vehicleBuild = GetBuildModels(VEHICLE_DEF_ID).First();
                var identityBuild = GetBuildModels(IDENTITY_DEF_ID).First();

                var showroomRelease = GetReleaseModel(SHOWROOM_REL_ID);
                var url  = $"{Request.Scheme}://{Request.Host.Value}";

                return View(new IndexModel(){ HostUrl = url });
            }
            public IList<BuildModel> GetBuildModels(int defintionId)
            {
                var buildObjects = Get<Tfs.Build.RootObject>($"{buildUrls}&definitions={defintionId}").Result; 

                 return buildObjects.value.Select(x => new BuildModel()
                {
                    Status = x.status,
                    Result = x.result,
                    Finished = x.finishTime
               }).OrderByDescending(x => x.Finished).ToList();
            }
            public IList<ReleaseModel> GetReleaseModel(int definitionId)
            {
                    var buildObjects = Get<Tfs.Release.RootObject>($"{releaseUrls}&defintionId={definitionId}").Result; 
                    return buildObjects.value.Select(x => new ReleaseModel()
                    {
                        Status = x.status,
                        CreatedOn = x.createdOn,
                        Name = x.name
                    }).OrderByDescending(x => x.Name).ToList();
            }

        public class ReleaseModel
        {
            public string Name { get; set; }
            public DateTime CreatedOn { get; set; }
            public string Status { get; set; }
        }

            public class BuildModel
            {
                public string Status { get; set; }
                public DateTime Finished { get; set; }
                public string Result { get; set; }
            }

            public async Task<IActionResult> Sources()
            {
                var model = new SourceModel();
                model.Items = new List<SourceItemModel>();
                using (var httpClient = new HttpClient())
                {
                    foreach (var apiSource in GetApiSources())
                    {

                        var modelItem = new SourceItemModel();
                        modelItem.Name = apiSource.Name;
                        modelItem.QArl = apiSource.Integration;
                        modelItem.IntegrationUrl = apiSource.Integration;

                        HttpResponseMessage integrationResponse, qaResponse;

                        try
                        {
                            integrationResponse = await httpClient.GetAsync(apiSource.Integration);
                            modelItem.IntegrationUp = integrationResponse.StatusCode == HttpStatusCode.OK;
                        }
                        catch
                        {
                            modelItem.IntegrationUp = false;
                        }
                        try
                        {
                            qaResponse = await httpClient.GetAsync(apiSource.QA);
                            modelItem.QAUp = qaResponse.StatusCode == HttpStatusCode.OK;
                        }
                        catch
                        {

                            modelItem.QAUp = false;
                        }


                        model.Items.Add(modelItem);
                    }
                }

                return Json(model);
            }
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
