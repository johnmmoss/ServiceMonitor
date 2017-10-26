using System;
using System.Collections.Generic;

namespace Tfs.Definition
{
public class Self
{
    public string href { get; set; }
}

public class Web
{
    public string href { get; set; }
}

public class Editor
{
    public string href { get; set; }
}

public class Links
{
    public Self self { get; set; }
    public Web web { get; set; }
    public Editor editor { get; set; }
}

public class AuthoredBy
{
    public string id { get; set; }
    public string displayName { get; set; }
    public string uniqueName { get; set; }
    public string url { get; set; }
    public string imageUrl { get; set; }
}

public class Pool
{
    public int id { get; set; }
    public string name { get; set; }
    public bool isHosted { get; set; }
}

public class Queue
{
    public int id { get; set; }
    public string name { get; set; }
    public Pool pool { get; set; }
}

public class Project
{
    public string id { get; set; }
    public string name { get; set; }
    public string url { get; set; }
    public string state { get; set; }
    public int revision { get; set; }
    public string visibility { get; set; }
}

public class Value
{
    public Links _links { get; set; }
    public string quality { get; set; }
    public AuthoredBy authoredBy { get; set; }
    public List<object> drafts { get; set; }
    public Queue queue { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public string url { get; set; }
    public string uri { get; set; }
    public string path { get; set; }
    public string type { get; set; }
    public string queueStatus { get; set; }
    public int revision { get; set; }
    public DateTime createdDate { get; set; }
    public Project project { get; set; }
}

public class RootObject
{
    public int count { get; set; }
    public List<Value> value { get; set; }
}
}