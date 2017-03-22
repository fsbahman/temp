using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOLRepositoryHack.Logic
{
    public class ReportModel
    {
        public string Identifier { get; set; }
        public DateTime EventTime { get; set; }
        public string EventName { get; set; }
        public string BCName { get; set; }
        public string  Metadata { get; set; }

        public override string ToString()
        {
            return $"{EventName}/{BCName}/{Metadata}";
        }
    }
}
