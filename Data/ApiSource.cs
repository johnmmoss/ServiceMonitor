namespace ServiceMonitor.Data
{
    public class ApiSource
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int BuildDefinitionId { get; set; }
            public int ReleaseDefinitionId { get; set; }
            public string QaUrl { get; set; }
            public string IntegrationUrl { get; set; }
        }
    }
