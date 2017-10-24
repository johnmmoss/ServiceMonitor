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
                new ApiSource("Site 2", "http://www.google.co.uk", "ht://www.google.co.uk"),
                new ApiSource("Site 3", "http://www.google.co.uk", "http://www.google.co.uk"),
                new ApiSource("Site 4", "http://www.google.co.uk", "http://www.google.co.uk")
            };
        }

        public async Task<IActionResult> Index()
        {
            var url = Request.Host.Value;
            var t = $"{Request.Scheme}://{Request.Host.Value}";
            return View(new IndexModel(){ HostUrl = t });
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
