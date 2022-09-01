using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentraLogClient
{
    public class Log
    {
        public string LogMessage { get; set; }
        public DateTime created { get; set; }
        public string ArchData { get; set; }
        public string projectId { get; set; }
        public string Type { get; set; }
    }
}
