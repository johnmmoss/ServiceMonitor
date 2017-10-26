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
    }
