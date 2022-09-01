namespace Repository.DataLayer.ProjectModels
{
    public class Log
    {
        public long _id { get; set; }

        public string LogMessage { get; set; }

        public string ArchData { get; set; }

        public string Type { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
