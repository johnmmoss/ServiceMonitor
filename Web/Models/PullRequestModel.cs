
namespace ServiceMonitor.Web.Models
{
    public class PullRequestModel
    {
        public string Name { get; set; }
        public string RepositoryName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string Url { get; set; }
        public ReviewersModel ZenithDevReview { get; set; }

        public ReviewersModel ZenithDevCodeReview { get; set; }
    }
}