namespace Api.Models
{
    public class PullRequestModel
    {
        public string Name { get; set; }
        public string RepositoryName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}