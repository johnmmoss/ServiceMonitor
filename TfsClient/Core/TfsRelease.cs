using System;
using System.Collections.Generic;
namespace Tfs.ReleaseDto
{
    public class ModifiedBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class CreatedBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class Variables
    {
    }

    public class EnvironmentOptions
    {
        public string emailNotificationType { get; set; }
        public string emailRecipients { get; set; }
        public bool skipArtifactsDownload { get; set; }
        public int timeoutInMinutes { get; set; }
        public bool enableAccessToken { get; set; }
        public bool publishDeploymentStatus { get; set; }
    }

    public class Owner
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class Web
    {
        public string href { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Web web { get; set; }
        public Self self { get; set; }
    }

    public class Release
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Links _links { get; set; }
    }

    public class Web2
    {
        public string href { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Web2 web { get; set; }
        public Self2 self { get; set; }
    }

    public class ReleaseDefinition
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Links2 _links { get; set; }
    }

    public class ReleaseCreatedBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
    }

    public class ProcessParameters
    {
    }

    public class Environment
    {
        public int id { get; set; }
        public int releaseId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public Variables variables { get; set; }
        public List<object> variableGroups { get; set; }
        public List<object> deploySteps { get; set; }
        public int rank { get; set; }
        public int definitionEnvironmentId { get; set; }
        public EnvironmentOptions environmentOptions { get; set; }
        public List<object> demands { get; set; }
        public List<object> conditions { get; set; }
        public List<object> workflowTasks { get; set; }
        public List<object> deployPhasesSnapshot { get; set; }
        public Owner owner { get; set; }
        public List<object> schedules { get; set; }
        public Release release { get; set; }
        public ReleaseDefinition releaseDefinition { get; set; }
        public ReleaseCreatedBy releaseCreatedBy { get; set; }
        public string triggerReason { get; set; }
        public ProcessParameters processParameters { get; set; }
    }

    public class Variables2
    {
    }

    public class Self3
    {
        public string href { get; set; }
    }

    public class Web3
    {
        public string href { get; set; }
    }

    public class Links3
    {
        public Self3 self { get; set; }
        public Web3 web { get; set; }
    }

    public class ReleaseDefinition2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Links3 _links { get; set; }
    }

    public class Self4
    {
        public string href { get; set; }
    }

    public class Web4
    {
        public string href { get; set; }
    }

    public class Links4
    {
        public Self4 self { get; set; }
        public Web4 web { get; set; }
    }

    public class ProjectReference
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Properties
    {
    }

    public class TfsRelease
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public ModifiedBy modifiedBy { get; set; }
        public CreatedBy createdBy { get; set; }
        public List<Environment> environments { get; set; }
        public Variables2 variables { get; set; }
        public List<object> variableGroups { get; set; }
        public ReleaseDefinition2 releaseDefinition { get; set; }
        public string description { get; set; }
        public string reason { get; set; }
        public string releaseNameFormat { get; set; }
        public bool keepForever { get; set; }
        public int definitionSnapshotRevision { get; set; }
        public string logsContainerUrl { get; set; }
        public string url { get; set; }
        public Links4 _links { get; set; }
        public List<object> tags { get; set; }
        public ProjectReference projectReference { get; set; }
        public Properties properties { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public List<TfsRelease> value { get; set; }
    }
}