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
                New("Showroom Service", 11, 12, 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5005/api/", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5005/api/")
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