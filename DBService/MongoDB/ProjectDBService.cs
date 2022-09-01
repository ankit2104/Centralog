using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Repository;
using Repository.DataLayer.ProjectDetailModels;
using Repository.DataLayer.ProjectModels;

namespace DBService.MongoDB
{

    public class ProjectDBService : IMongoDBService
    {
        private readonly IConfiguration _configuration;
        private readonly MongoDBSettings _mongoDbSettings;
        private readonly IMongoCollection<Project> _projectCollection;
        public ProjectDBService(IConfiguration config)
        {
            _configuration = config;
            _mongoDbSettings = _configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            MongoClient client = new MongoClient(_mongoDbSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase("CentraLog");
            _projectCollection = database.GetCollection<Project>("Project");
        }
        public async Task<bool> checkExistProjectByUserId(string userId)
        {
            var count = await _projectCollection.CountDocumentsAsync(new BsonDocument("user_id", userId));
            return count > 0;
        }
        //do not use this method 
        public async Task<List<Project>> getAllProjectList()
        {
            var list = await _projectCollection.Find<Project>(new BsonDocument()).ToListAsync<Project>();
            return list;
        }
        //use this one
        public async Task<List<ProjectDetail>> getProjectDetailsByUser(string userId)
        {
            var list = await _projectCollection.Find<Project>(new BsonDocument("user_id", userId)).ToListAsync<Project>();
            List<ProjectDetail> newList = list.Select<Project, ProjectDetail>(
                    p => new ProjectDetail()
                    {
                        id = p._id,
                        user_id = p.user_id,
                        name = p.ProjectName,
                        created = p.Created,
                        updated = p.Updated,
                        logCount = p.LogCount

                    }).ToList();
            return newList;
        }
        public async Task<bool> updateProjectDetail(string projectId, string ProjectName)
        {
            var filter = new BsonDocument("_id", projectId);
            var update = new BsonDocument("$set", new BsonDocument { { "ProjectName", ProjectName }, { "Updated", DateTime.Now } });
            var project = await _projectCollection.UpdateOneAsync(filter, update);
            return project.IsAcknowledged;
        }
        public async Task createProject(string userId, string ProjectName)
        {
            await _projectCollection.InsertOneAsync(new Project() { _id = "", user_id = userId, ProjectName = ProjectName, Created = DateTime.Now, Updated = DateTime.Now, LogCount = 0, Logs = new Log[1] });
            var update = new BsonDocument("$pull", new BsonDocument { { "Logs", BsonNull.Value } });
            await _projectCollection.UpdateManyAsync(new BsonDocument(), update);

            return;
        }
        public async Task<bool> addLog(Log log, string ProjectId)
        {

            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("_id", ProjectId);
            List<Project> proj = await _projectCollection.Find(filter).ToListAsync();
            log._id = proj.First().LogCount + 1;
            UpdateDefinition<Project> update = Builders<Project>.Update.AddToSet<Log>("Logs", log).Inc("LogCount", 1);
            var project = await _projectCollection.UpdateOneAsync(filter, update);
            return project.IsAcknowledged;
        }
        public async Task<List<Log>> getLogs(string ProjectId)
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("_id", ProjectId);
            List<Project> project = await _projectCollection.Find<Project>(filter).ToListAsync<Project>();
            var logs = project.First<Project>().Logs.ToList();
            return logs;
        }
        public async Task<List<Log>> getLatestLogs(string ProjectId)
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("_id", ProjectId);
            List<Project> project = await _projectCollection.Find<Project>(filter).ToListAsync<Project>();
            List<Log> logs = project.First<Project>().Logs.ToList();
            return logs.OrderByDescending(x => x._id).Take(10).ToList();
        }
        public async Task<bool> deleteProject(string ProjectId)
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("_id", ProjectId);
            var result = await _projectCollection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }
        public async Task<bool> checkExistProjectById(string ProjectId)
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("_id", ProjectId);
            var result = await _projectCollection.CountDocumentsAsync(filter);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
