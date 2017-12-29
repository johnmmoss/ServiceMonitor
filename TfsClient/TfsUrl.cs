namespace Tfs.Client
{
    public class TfsUrl
    {
        private string buildTemplate = "https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/build/builds?api-version=2.0";
        private string releaseTemplate = "https://{0}.vsrm.visualstudio.com/DefaultCollection/{1}/_apis/release/releases?$expand=environments&definitionId=";
        private string pullRequestsTemplate = "https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/git/pullRequests?status=active";
        private string pullRequestTemplate = "https://{0}.visualstudio.com/{1}/ZenithDev/_git/{2}/pullrequest/{3}?view=discussion"; // TODO Move ZenithDev to options

        public string Instance { get; private set; }
        public string Project { get; private set; }
        public string ReleaseApi { get; private set; }
        public string BuildApi { get; private set; }
        public string PullRequestsApi { get; private set; }

        public TfsUrl(string instance, string project)
        {
           Instance = instance;
           Project = project; 

           BuildApi = string.Format(buildTemplate, instance, project);
           ReleaseApi = string.Format(releaseTemplate, instance, project);
           PullRequestsApi = string.Format(pullRequestsTemplate, instance, project);
        }

        public string GetReleaseUrl(int definitionId)
        {
            return $"{ReleaseApi}&definitions={definitionId}";
        }

        public string BuildPullRequestUrl(string repoName, int pullRequestId)
        {
            return string.Format(pullRequestTemplate, Instance, Project, repoName, pullRequestId);
        }
    }
}