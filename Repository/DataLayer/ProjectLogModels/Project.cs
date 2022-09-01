using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Repository.DataLayer.ProjectModels
{
    public class Project
    {
        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string ProjectName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string user_id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Updated { get; set; }

        [BsonRepresentation(BsonType.Int64)]
        public long LogCount { get; set; }

        public Log[] Logs { get; set; }

    }
}
