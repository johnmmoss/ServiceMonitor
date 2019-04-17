using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceMonitor.Data 
{
    public class TfsProjectsRepository
    {
        private List<PipelineInfo> _projects;

        public TfsProjectsRepository()
        {
            _projects = new List<PipelineInfo>()
            {
                New("Radix.Identity.Api", 24, 7,
                    "http://lgssvm10:7002/swagger/ui/index",
                    "http://lgssvm11:8002/swagger/ui/index"),

                New("Radix.AllocationEngine.Api", 11, 3,
                    "http://lgssvm10:7007/swagger/ui/index",
                    "http://lgssvm11:8007/swagger/ui/index"),

                 New("Radix.FileStorage.Api", 13, 2,
                    "http://lgssvm10:7007/swagger/ui/index",
                    "http://lgssvm11:8007/swagger/ui/index"),

                 New("Radix.LegacyWrapper.Api", 22, 6,
                    "http://lgssvm10:7009/swagger/ui/index",
                    "http://lgssvm11:8009/swagger/ui/index")
            };
        }
        
        public IList<PipelineInfo> GetAll()
        {
            return _projects;
        }

        public PipelineInfo Get(int id)
        {
            return _projects.First(x => x.Id == id);
        }

        private PipelineInfo New(string name, int buildDefinitionId, 
                                int releaseDefinitionId, string integrationUrl,
                                string qaUrl)
        {
            return new PipelineInfo()
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