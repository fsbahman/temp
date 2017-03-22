using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOLRepositoryHack.Logic
{
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

        public bool IsSameEventFinished(ReportModel reportModel)
        {
            bool isSameContext = Identifier == reportModel.Identifier && Metadata == reportModel.Metadata;
            bool isUnusualFinish = (reportModel.EventName == Events.Finishnoaction && Name == Events.Update) ||
                                    (reportModel.EventName == Events.Finishwithexception && Name == Events.Update);
            bool isStartSameAsFinish = Name + Events.Finished == reportModel.EventName;

            bool result = (isUnusualFinish || isStartSameAsFinish) && isSameContext;

            return result;
        }

        public EventModel()
        {
            ChildEvents = new List<Logic.EventModel>();
        }

        public override string ToString()
        {
            return $"{Name}/{BCName}/{Metadata}";
        }
    }
}
