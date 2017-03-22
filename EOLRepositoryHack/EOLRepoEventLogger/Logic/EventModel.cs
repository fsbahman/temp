using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOLRepoEventLogger.Logic
{
    [Serializable]
    public class EventModel
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string BCName { get; set; }
        public string Metadata { get; set; }
        public string EventLocation { get; set; }

        public List<EventModel> ChildEvents { get; set; }
        
        public EventModel()
        {
            ChildEvents = new List<Logic.EventModel>();
        }

        public override string ToString()
        {
            return $"{Name}/{BCName}/{Metadata}/({ChildEvents.Count})";
        }
    }
}
