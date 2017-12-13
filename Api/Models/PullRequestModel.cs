using ApiPinger.Models;
using System.Collections.Generic;

namespace Api.Models
{
    public class PullRequestModel
    {
        public string Name { get; set; }
        public string RepositoryName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public ReviewersModel ZenithDevReview { get; set; }

        public ReviewersModel ZenithDevCodeReview { get; set; }
    }
}