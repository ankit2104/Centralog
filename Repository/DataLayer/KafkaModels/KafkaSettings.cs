namespace Repository.DataLayer.KafkaModels
{
    public class KafkaSettings
    {
        public string Topic { get; set; }
        public string ServerUri { get; set; }
        public string GroupId { get; set; }
    }
}
