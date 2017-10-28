using System.Collections.Generic;

namespace ApiPinger
{
    public class ApiSourceRepository
    {
        private List<ApiSource> _apiSources;

        public ApiSourceRepository()
        {
            _apiSources = new List<ApiSource>() 
            {
                New("Service1", 11, 12, 
                    "http://localhost:5005/api/", 
                    "http://localhost:5006/api/")
            };
        }
        
        public IList<ApiSource> GetAll()
        {
            return _apiSources;
        }

        private ApiSource New(string name, int buildDefinitionId, 
                                int releaseDefinitionId, string integrationUrl,
                                string qaUrl)
        {
            return new ApiSource()
            {
                Name = name,
                ReleaseDefinitionId = releaseDefinitionId,
                IntegrationUrl = integrationUrl,
                QaUrl = qaUrl
            };
        }
    }
}