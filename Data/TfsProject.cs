using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitor.Data
{
    public class TfsProject
    {
        public string Name { get; set; }

        public IList<PipelineInfo> PipelineInfos { get; set; }
    }
}
