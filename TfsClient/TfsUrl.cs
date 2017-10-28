namespace Tfs.Client
{
    public class TfsUrl
    {
        private string buildTemplate = "https://{0}.visualstudio.com/DefaultCollection/{1}/_apis/build/builds?api-version=2.0";
        private string releaseTemplate = "https://{0}.vsrm.visualstudio.com/DefaultCollection/{1}/_apis/release/releases?api-version=4.0-preview.4";

        public string Instance { get; private set; }
        public string Project { get; private set; }
        public string ReleaseApi { get; private set; }
        public string BuildApi { get; private set; }

        public TfsUrl(string instance, string project)
        {
           Instance = instance;
           Project = project; 

           BuildApi = string.Format(buildTemplate, instance, project);
           ReleaseApi = string.Format(releaseTemplate, instance, project);
        }

        public string GetReleaseUrl(int definitionId)
        {
            return $"{ReleaseApi}&definitions={definitionId}";
        }
    }
}