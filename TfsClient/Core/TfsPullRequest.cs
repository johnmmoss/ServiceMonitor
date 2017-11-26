using System;
using System.Collections.Generic;

namespace Tfs.PullRequestDto
{
   public class Project
{
    public string id { get; set; }
    public string name { get; set; }
    public string state { get; set; }
    public string visibility { get; set; }
}

public class Repository
{
    public string id { get; set; }
    public string name { get; set; }
    public string url { get; set; }
    public Project project { get; set; }
}

public class CreatedBy
{
    public string id { get; set; }
    public string displayName { get; set; }
    public string uniqueName { get; set; }
    public string url { get; set; }
    public string imageUrl { get; set; }
}

public class LastMergeSourceCommit
{
    public string commitId { get; set; }
    public string url { get; set; }
}

public class LastMergeTargetCommit
{
    public string commitId { get; set; }
    public string url { get; set; }
}

public class LastMergeCommit
{
    public string commitId { get; set; }
    public string url { get; set; }
}

public class VotedFor
{
    public string reviewerUrl { get; set; }
    public int vote { get; set; }
    public string id { get; set; }
    public string displayName { get; set; }
    public string uniqueName { get; set; }
    public string url { get; set; }
    public string imageUrl { get; set; }
    public bool isContainer { get; set; }
}

public class Reviewer
{
    public string reviewerUrl { get; set; }
    public int vote { get; set; }
    public bool isRequired { get; set; }
    public string id { get; set; }
    public string displayName { get; set; }
    public string uniqueName { get; set; }
    public string url { get; set; }
    public string imageUrl { get; set; }
    public bool isContainer { get; set; }
    public List<VotedFor> votedFor { get; set; }
}

public class CompletionOptions
{
    public string mergeCommitMessage { get; set; }
    public bool deleteSourceBranch { get; set; }
    public bool squashMerge { get; set; }
    public string bypassReason { get; set; }
    public bool transitionWorkItems { get; set; }
}

public class TfsPullRequest
{
    public Repository repository { get; set; }
    public int pullRequestId { get; set; }
    public int codeReviewId { get; set; }
    public string status { get; set; }
    public CreatedBy createdBy { get; set; }
    public DateTime creationDate { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string sourceRefName { get; set; }
    public string targetRefName { get; set; }
    public string mergeStatus { get; set; }
    public string mergeId { get; set; }
    public LastMergeSourceCommit lastMergeSourceCommit { get; set; }
    public LastMergeTargetCommit lastMergeTargetCommit { get; set; }
    public LastMergeCommit lastMergeCommit { get; set; }
    public List<Reviewer> reviewers { get; set; }
    public string url { get; set; }
    public CompletionOptions completionOptions { get; set; }
    public bool supportsIterations { get; set; }
}

public class RootObject
{
    public List<TfsPullRequest> value { get; set; }
    public int count { get; set; }
} 
}