using Repository.DataLayer.ProjectModels;

namespace Repository.DataLayer.ProjectDetailModels
{
    public class LogInsertModel
    {
        public Log log { get; set; }
        public ProjectDetail project { get; set; }
    }
}
