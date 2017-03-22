using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firehose.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string PayloadJson { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
