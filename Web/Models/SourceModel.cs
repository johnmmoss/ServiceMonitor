using System.Collections.Generic;

namespace ServiceMonitor.Web.Models
{
    public class SourceModel
    {
        public IList<SourceItemModel> Items { get; set; }
        public IList<BuildModel> Builds { get; set; }
    }
}
