using ServiceMonitor.Web.Models;
using System.Collections.Generic;

namespace Web.Models
{
    public class TfsProjectModel
    {
        public string Name { get; set; }

        public List<PipelineInfoModel> PipelineInfoModels { get; set; }
    }
}
