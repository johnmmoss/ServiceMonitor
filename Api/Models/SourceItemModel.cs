using System.Collections.Generic;

namespace ApiPinger.Controllers
{
    public class SourceItemModel
    {
        public string Name { get; set; }
        public bool QaUp { get; set; }
        public bool IntegrationUp { get; set; }
        public string IntegrationUrl { get; set; }
        public string QaUrl { get; set; }
        public IList<BuildModel> Build { get; set; }
        public IList<ReleaseModel> Release { get; set; }
    }
}
