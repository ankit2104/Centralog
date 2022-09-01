using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentraLogClient
{
    public class KafkaSettings
    {
        public string ProjectID { get; set; }
        public string Topic { get; set; }
        public string BootstrapServers { get; set; }
    }
}
