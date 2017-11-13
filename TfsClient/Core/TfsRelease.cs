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

    public class ASPNETCOREENVIRONMENT
    {
        public string value { get; set; }
    }

    public class Variables
    {
        public ASPNETCOREENVIRONMENT ASPNETCORE_ENVIRONMENT { get; set; }
    }

    public class Approval
    {
        public int rank { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int id { get; set; }
    }

    public class PreApprovalsSnapshot
    {
        public List<Approval> approvals { get; set; }
    }

    public class Approval2
    {
        public int rank { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int id { get; set; }
    }

    public class PostApprovalsSnapshot
    {
        public List<Approval2> approvals { get; set; }
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

    public class ParallelExecution
    {
        public string parallelExecutionType { get; set; }
    }

    public class ArtifactsDownloadInput
    {
        public List<object> downloadInputs { get; set; }
    }

    public class OverrideInputs
    {
    }

    public class DeploymentInput
    {
        public ParallelExecution parallelExecution { get; set; }
        public bool skipArtifactsDownload { get; set; }
        public ArtifactsDownloadInput artifactsDownloadInput { get; set; }
        public int queueId { get; set; }
        public List<object> demands { get; set; }
        public bool enableAccessToken { get; set; }
        public int timeoutInMinutes { get; set; }
        public int jobCancelTimeoutInMinutes { get; set; }
        public string condition { get; set; }
        public OverrideInputs overrideInputs { get; set; }
    }

    public class OverrideInputs2
    {
    }

    public class Inputs
    {
        public string ConnectedServiceNameSelector { get; set; }
        public string ConnectedServiceName { get; set; }
        public string ConnectedServiceNameARM { get; set; }
        public string ScriptType { get; set; }
        public string ScriptPath { get; set; }
        public string Inline { get; set; }
        public string ScriptArguments { get; set; }
        public string TargetAzurePs { get; set; }
        public string CustomTargetAzurePs { get; set; }
        public string SourcePath { get; set; }
        public string Destination { get; set; }
        public string StorageAccount { get; set; }
        public string StorageAccountRM { get; set; }
        public string ContainerName { get; set; }
        public string BlobPrefix { get; set; }
        public string EnvironmentName { get; set; }
        public string EnvironmentNameRM { get; set; }
        public string ResourceFilteringMethod { get; set; }
        public string MachineNames { get; set; }
        public string vmsAdminUserName { get; set; }
        public string vmsAdminPassword { get; set; }
        public string TargetPath { get; set; }
        public string AdditionalArguments { get; set; }
        public string enableCopyPrerequisites { get; set; }
        public string CopyFilesInParallel { get; set; }
        public string CleanTargetBeforeCopy { get; set; }
        public string skipCACheck { get; set; }
        public string outputStorageUri { get; set; }
        public string outputStorageContainerSasToken { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public string Protocol { get; set; }
        public string TestCertificate { get; set; }
        public string InitializationScriptPath { get; set; }
        public string SessionVariables { get; set; }
        public string RunPowershellInParallel { get; set; }
        public string command { get; set; }
        public string publishWebProjects { get; set; }
        public string projects { get; set; }
        public string custom { get; set; }
        public string arguments { get; set; }
        public string zipAfterPublish { get; set; }
        public string modifyOutputPath { get; set; }
        public string selectOrConfig { get; set; }
        public string feedRestore { get; set; }
        public string includeNuGetOrg { get; set; }
        public string nugetConfigPath { get; set; }
        public string externalEndpoints { get; set; }
        public string noCache { get; set; }
        public string packagesDirectory { get; set; }
        public string verbosityRestore { get; set; }
        public string searchPatternPush { get; set; }
        public string nuGetFeedType { get; set; }
        public string feedPublish { get; set; }
        public string externalEndpoint { get; set; }
        public string searchPatternPack { get; set; }
        public string configurationToPack { get; set; }
        public string outputDir { get; set; }
        public string nobuild { get; set; }
        public string versioningScheme { get; set; }
        public string versionEnvVar { get; set; }
        public string requestedMajorVersion { get; set; }
        public string requestedMinorVersion { get; set; }
        public string requestedPatchVersion { get; set; }
        public string buildProperties { get; set; }
        public string verbosityPack { get; set; }
        public string script { get; set; }
        public string workingDirectory { get; set; }
        public string failOnStderr { get; set; }
    }

    public class WorkflowTask
    {
        public string taskId { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public string refName { get; set; }
        public bool enabled { get; set; }
        public bool alwaysRun { get; set; }
        public bool continueOnError { get; set; }
        public int timeoutInMinutes { get; set; }
        public string definitionType { get; set; }
        public OverrideInputs2 overrideInputs { get; set; }
        public string condition { get; set; }
        public Inputs inputs { get; set; }
    }

    public class DeployPhasesSnapshot
    {
        public DeploymentInput deploymentInput { get; set; }
        public int rank { get; set; }
        public string phaseType { get; set; }
        public string name { get; set; }
        public List<WorkflowTask> workflowTasks { get; set; }
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

    public class PreDeploymentGatesSnapshot
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public List<object> gates { get; set; }
    }

    public class PostDeploymentGatesSnapshot
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public List<object> gates { get; set; }
    }

    public class Environment
    {
        public int id { get; set; }
        public int releaseId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public Variables variables { get; set; }
        public List<object> variableGroups { get; set; }
        public List<object> preDeployApprovals { get; set; }
        public List<object> postDeployApprovals { get; set; }
        public PreApprovalsSnapshot preApprovalsSnapshot { get; set; }
        public PostApprovalsSnapshot postApprovalsSnapshot { get; set; }
        public List<object> deploySteps { get; set; }
        public int rank { get; set; }
        public int definitionEnvironmentId { get; set; }
        public EnvironmentOptions environmentOptions { get; set; }
        public List<object> demands { get; set; }
        public List<object> conditions { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public List<object> workflowTasks { get; set; }
        public List<DeployPhasesSnapshot> deployPhasesSnapshot { get; set; }
        public Owner owner { get; set; }
        public List<object> schedules { get; set; }
        public Release release { get; set; }
        public ReleaseDefinition releaseDefinition { get; set; }
        public ReleaseCreatedBy releaseCreatedBy { get; set; }
        public string triggerReason { get; set; }
        public double timeToDeploy { get; set; }
        public ProcessParameters processParameters { get; set; }
        public PreDeploymentGatesSnapshot preDeploymentGatesSnapshot { get; set; }
        public PostDeploymentGatesSnapshot postDeploymentGatesSnapshot { get; set; }
    }

    public class AzureRegistry
    {
        public string value { get; set; }
    }

    public class ContainerName
    {
        public string value { get; set; }
    }

    public class ImageName
    {
        public string value { get; set; }
    }

    public class LoggingPathHost
    {
        public string value { get; set; }
    }

    public class MajorVersion
    {
        public string value { get; set; }
    }

    public class MinorVersion
    {
        public string value { get; set; }
    }

    public class Ports
    {
        public string value { get; set; }
    }

    public class RegistryPassword
    {
        public string value { get; set; }
    }

    public class RegistryUsername
    {
        public string value { get; set; }
    }

    public class ZenithIntegrationVMDns
    {
        public string value { get; set; }
    }

    public class ZenithQAVMDns
    {
        public string value { get; set; }
    }

    public class Variables2
    {
        public AzureRegistry AzureRegistry { get; set; }
        public ContainerName ContainerName { get; set; }
        public ImageName ImageName { get; set; }
        public LoggingPathHost LoggingPathHost { get; set; }
        public MajorVersion MajorVersion { get; set; }
        public MinorVersion MinorVersion { get; set; }
        public Ports Ports { get; set; }
        public RegistryPassword RegistryPassword { get; set; }
        public RegistryUsername RegistryUsername { get; set; }
        public ZenithIntegrationVMDns ZenithIntegrationVMDns { get; set; }
        public ZenithQAVMDns ZenithQAVMDns { get; set; }
    }

    public class ArtifactSourceDefinitionUrl
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class DefaultVersionBranch
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class DefaultVersionSpecific
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class DefaultVersionTags
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class DefaultVersionType
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Definition
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Version
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ArtifactSourceVersionUrl
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Branch
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class DefinitionReference
    {
        public ArtifactSourceDefinitionUrl artifactSourceDefinitionUrl { get; set; }
        public DefaultVersionBranch defaultVersionBranch { get; set; }
        public DefaultVersionSpecific defaultVersionSpecific { get; set; }
        public DefaultVersionTags defaultVersionTags { get; set; }
        public DefaultVersionType defaultVersionType { get; set; }
        public Definition definition { get; set; }
        public Project project { get; set; }
        public Version version { get; set; }
        public ArtifactSourceVersionUrl artifactSourceVersionUrl { get; set; }
        public Branch branch { get; set; }
    }

    public class Artifact
    {
        public string sourceId { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public DefinitionReference definitionReference { get; set; }
        public bool isPrimary { get; set; }
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
        public object name { get; set; }
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
        public List<Artifact> artifacts { get; set; }
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
}