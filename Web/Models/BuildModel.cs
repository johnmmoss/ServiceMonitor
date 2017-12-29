using System;

namespace ServiceMonitor.Web.Models
{
    public class BuildModel
    {
        public string Number { get; set; }
        public string Status { get; set; }
        public DateTime Finished { get; set; }
        public string Result { get; set; }
    }
}