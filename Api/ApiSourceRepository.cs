using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPinger
{
    public class ApiSourceRepository
    {
        private List<ApiSource> _apiSources;

        public ApiSourceRepository()
        {
            _apiSources = new List<ApiSource>() 
            {
                New("Showroom Service", 12, 11, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5005/swagger/", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5005/swagger/"),

                New("Identity Service", 11, 12, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5004/api/docs", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5004/api/docs"),

                New("Vehicle Image Service", 1, 3, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5001/api/docs", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5001/api/docs"),

                New("Credit Check Service", 4, 4, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5002/swagger", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5002/swagger"),

                New("Order Service", 14, 15, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5006/swagger", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5006/swagger"),

                New("Email Service", 15, 16, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5015/swagger", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5015/swagger"),

                New("My Account Service", 16, 17, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5007/swagger", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5007/swagger"),

                New("Underwriting Service", 18, 19, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5010/swagger", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5010/swagger"),

                New("Document Signing Service", 20, 22, 
                    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5019/swagger", 
                    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5019/swagger"),

                //New("Internal Website", 23, 19, 
                //    "http://zen-int-api-01.ukwest.cloudapp.azure.com:5007/swagger", 
                //    "http://zen-qa-api-01.ukwest.cloudapp.azure.com:5007/swagger")
        
            };
        }
        
        public IList<ApiSource> GetAll()
        {
            return _apiSources;
        }

        public ApiSource Get(int id)
        {
            return _apiSources.First(x => x.Id == id);
        }

        private ApiSource New(string name, int buildDefinitionId, 
                                int releaseDefinitionId, string integrationUrl,
                                string qaUrl)
        {
            return new ApiSource()
            {
                Name = name,
                BuildDefinitionId = buildDefinitionId,
                ReleaseDefinitionId = releaseDefinitionId,
                IntegrationUrl = integrationUrl,
                QaUrl = qaUrl
            };
        }
    }
}