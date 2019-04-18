using System;

namespace Tfs.Client
{
    public class TfsUrl
    {
        private string _buildTemplate = "https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/build/builds?api-version=2.0";
        private string _releaseTemplate = "https://{0}.vsrm.visualstudio.com/DefaultCollection/{1}/_apis/release/releases?$expand=environments&definitionId=";
        private string _pullRequestsTemplate = "https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/git/pullRequests?status=active";
        private string _pullRequestTemplate = "https://{0}.visualstudio.com/{1}/ZenithDev/_git/{2}/pullrequest/{3}?view=discussion"; // TODO Move ZenithDev to options

        private string Instance { get; set; }

        public string PullRequestsApi { get; private set; }

        public TfsUrl(string instance)
        {
           Instance = instance;

           //PullRequestsApi = string.Format(_pullRequestsTemplate, instance, project);
        }

        public string GetTfsBuildApi(string projectName)
        {
           return string.Format(_buildTemplate, Instance, projectName);
        }

        public string GetReleaseUrl(string projectName, int definitionId)
        {
            return string.Format(_releaseTemplate, Instance, projectName) + definitionId; 
        }

        public string BuildPullRequestUrl(string repoName, int pullRequestId)
        {
            return string.Empty;
            // TODO pull request functionality currently "off"
            //return string.Format(_pullRequestTemplate, Instance, Project, repoName, pullRequestId);
        }
    }
}