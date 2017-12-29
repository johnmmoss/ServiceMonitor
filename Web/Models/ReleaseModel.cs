using System;

namespace ServiceMonitor.Web.Models
{
    public class ReleaseModel
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
    }
}
