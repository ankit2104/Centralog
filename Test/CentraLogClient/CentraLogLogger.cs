using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentraLogClient
{
    public class CentraLogLogger
    {
        private readonly KafkaSettings _settings;
        private IProducer<Null, String> log_producer;
        private string json = "{\"Topic\": \"Logger\",\"BootstrapServers\": \"localhost:9092\",\"ProjectId\": \"62dfbb0c8e756f451d41f5c4\"}";
        public CentraLogLogger(string PathToSeetings)
        {

            this._settings = JsonConvert.DeserializeObject<KafkaSettings>(json);
            //setting producer properties
            var config = new ProducerConfig { BootstrapServers = _settings.BootstrapServers };
            //creating the producer
            this.log_producer = new ProducerBuilder<Null, string>(config).Build();
        }
        public async Task DebugAsync(string Message, string ExtraDetails)
        {
            var log = new Log()
            {
                LogMessage = Message,
                projectId = this._settings.ProjectID,
                ArchData = ExtraDetails,
                Type = "Debug",
                created = DateTime.Now
            };
            //creating producer record //passing topic and value then producing the data data
            var res = await this.log_producer.ProduceAsync(this._settings.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(log) });

        }
        public void Debug(string Message, string ExtraDetails)
        {
            var log = new Log()
            {
                LogMessage = Message,
                projectId = this._settings.ProjectID,
                ArchData = ExtraDetails,
                Type = "Debug",
                created = DateTime.Now
            };
            this.log_producer.Produce(this._settings.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(log) });
        }
        public async Task ErrorAsync(string Message, string ExtraDetails)
        {
            var log = new Log()
            {
                LogMessage = Message,
                projectId = this._settings.ProjectID,
                ArchData = ExtraDetails,
                Type = "Error",
                created = DateTime.Now
            };
            var res = await this.log_producer.ProduceAsync(this._settings.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(log) });

        }
        public void Error(string Message, string ExtraDetails)
        {
            var log = new Log()
            {
                LogMessage = Message,
                projectId = this._settings.ProjectID,
                ArchData = ExtraDetails,
                Type = "Error",
                created = DateTime.Now
            };
            this.log_producer.Produce(this._settings.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(log) });
        }
        public async Task LogAsync(string Message, string ExtraDetails)
        {
            var log = new Log()
            {
                LogMessage = Message,
                projectId = this._settings.ProjectID,
                ArchData = ExtraDetails,
                Type = "Log",
                created = DateTime.Now
            };
            var res = await this.log_producer.ProduceAsync(this._settings.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(log) });

        }
        public void Log(string Message, string ExtraDetails)
        {
            var log = new Log()
            {
                LogMessage = Message,
                projectId = this._settings.ProjectID,
                ArchData = ExtraDetails,
                Type = "Log",
                created = DateTime.Now
            };
            this.log_producer.Produce(this._settings.Topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(log) });
        }
    }
}


//Default partationar is sticky partationar
