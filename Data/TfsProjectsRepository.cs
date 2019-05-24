using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceMonitor.Data 
{
    public class TfsProjectsRepository
    {
        private List<TfsProject> _projects;

        public TfsProjectsRepository()
        {
            _projects = new List<TfsProject>();

            _projects.Add(new TfsProject()
            {
                Name = "Radix",
                PipelineInfos = new List<PipelineInfo>()
                {
                    New("Radix.Identity.Api", 24, 7,
                        "http://lgssvm10:7002/swagger/ui/index",
                        "http://lgssvm11:8002/swagger/ui/index"),

                    New("Radix.AllocationEngine.Api", 11, 3,
                        "http://lgssvm10:7007/swagger/ui/index",
                        "http://lgssvm11:8007/swagger/ui/index"),

                     New("Radix.FileStorage.Api", 13, 2,
                        "http://lgssvm10:7006/swagger/ui/index",
                        "http://lgssvm11:8006/swagger/ui/index"),

                     New("Radix.LegacyWrapper.Api", 22, 6,
                        "http://lgssvm10:7009/swagger/ui/index",
                        "http://lgssvm11:8009/swagger/ui/index")
                }
            });

            _projects.Add(new TfsProject()
            {
                Name = "SmartrValuation",
                PipelineInfos = new List<PipelineInfo>()
                {
                    New("SmartrValuation.Portal.Web", 36, 4 ,
                        "http://lgssvm10:7000/login",
                        "http://lgssvm11:8000/login"),

                    New("SmartrValuation.Portal.Api", 2, 1,
                        "http://lgssvm10:7000/login",
                        "http://lgssvm11:8000/login"),

                   New("SmartRValuation.PropertyRules.Api", 9, 7,
                        "http://lgssvm10:7000/login",
                        "http://lgssvm11:8000/login"),
                }
            });

            _projects.Add(new TfsProject()
            {
                Name = "SmartrFirmAdmin",
                PipelineInfos = new List<PipelineInfo>()
                {
                    New("SmartRFirmAdmin.Api", 32, 3 ,
                        "http://lgssvm10:7005/swagger/ui/index",
                        "http://lgssvm11:8005/swagger/ui/index"),

                    New("SmartRFirmAdmin.Web", 35, 4,
                        "http://lgssvm10:7008/login",
                        "http://lgssvm11:8008/login"),
                }
            });
        }
        
        public IList<TfsProject> GetAll()
        {
            return _projects;
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