namespace ApiPinger.Controllers
{
    public class SourceItemModel
    {
        public string Name { get; set; }
        public bool QAUp { get; set; }
        public bool IntegrationUp { get; set; }
        public string IntegrationUrl { get; set; }
        public string QArl { get; set; }
    }
}
