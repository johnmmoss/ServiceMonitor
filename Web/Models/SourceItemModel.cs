using System.Collections.Generic;

namespace ServiceMonitor.Web.Models
{
    public class SourceItemModel
    {
        public string Name { get; set; }
        public bool QaUp { get; set; }
        public bool IntegrationUp { get; set; }
        public string IntegrationUrl { get; set; }
        public string QaUrl { get; set; }
        public IList<BuildModel> Build { get; set; }
        public ReleaseModel ReleaseIntegration { get; set; }
        public ReleaseModel ReleaseQa { get; set; }
    }
}
