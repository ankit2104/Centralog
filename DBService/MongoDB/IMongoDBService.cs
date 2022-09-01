using Repository.DataLayer.ProjectDetailModels;
using Repository.DataLayer.ProjectModels;
namespace DBService.MongoDB
{
    public interface IMongoDBService
    {
        public Task<bool> checkExistProjectByUserId(string userId);
        public Task<List<Project>> getAllProjectList();
        public Task<List<ProjectDetail>> getProjectDetailsByUser(string userId);
        public Task<bool> updateProjectDetail(string projectId, string ProjectName);
        public Task createProject(string userId, string ProjectName);
        public Task<bool> addLog(Log log, string ProjectId);
        public Task<List<Log>> getLogs(string ProjectId);
        public Task<List<Log>> getLatestLogs(string ProjectId);
        public Task<bool> deleteProject(string ProjectId);
        public Task<bool> checkExistProjectById(string ProjectId);

    }
}
