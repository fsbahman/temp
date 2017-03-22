using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOLRepositoryHack.Logic
{
    public class EventGenerator
    {
        /*public List<EventModel> GetEvents(IEnumerable<ReportModel> data)
        {
            var list = new List<EventModel>();

            var stack = new Stack<EventModel>();

            foreach (var reportModel in data)
            {
                if (Events.IsUpdateStart(reportModel.EventName))
                {
                    EventModel newEvent = CerateEventModel(reportModel);
                    if (stack.Count > 0)//New BC Update and not a Chain
                    {
                        var parent = stack.Peek();
                        parent.ChildEvents.Add(newEvent);
                    }
                    stack.Push(newEvent);
                }
                else

                if (Events.IsUpdateFinished(reportModel.EventName))
                {
                    var topEvent = stack.Peek();
                    if (topEvent.Identifier != reportModel.Identifier)
                    {
                        throw new InvalidOperationException("Inner or Outer Event is finished at incorrect order.");
                    }

                    if (topEvent.Name != Events.Update)
                    {
                        throw new InvalidOperationException("Event finished before other events finished.");
                    }

                    topEvent.EndTime = reportModel.EventTime;
                    topEvent.Duration = topEvent.EndTime - topEvent.StartTime;
                    if (stack.Count == 1)//the only event in the stack
                    {
                        list.Add(topEvent);
                    }
                    stack.Pop();
                }
                else

                if (Events.IsStartEvent(reportModel.EventName))
                {
                    if (stack.Count == 0)//New BC Update and not a Chain
                    {
                        throw new InvalidOperationException("Non BCUpdate Event triggered without update");
                    }
                    EventModel newEvent = CerateEventModel(reportModel);
                    var parent = stack.Peek();
                    parent.ChildEvents.Add(newEvent);
                }
                else

                if (Events.IsFinishedEvent(reportModel.EventName))
                {
                    var topEvent = stack.Peek();

                    var startedEvent = topEvent.ChildEvents.FirstOrDefault(f => f.IsSameEventFinished(reportModel));
                    if (startedEvent == null)
                    {
                        throw new InvalidOperationException("Event is finished before being started");
                    }

                    startedEvent.EndTime = reportModel.EventTime;
                    startedEvent.Duration = startedEvent.EndTime - startedEvent.StartTime;
                }
            }

            return list;
        }
        */

        public EventModel GetEvents2(IEnumerable<ReportModel> data)
        {
            var stack = new Stack<EventModel>();

            stack.Push(new EventModel {
                Name="ROOT"
            });


            int counter = 0;
            foreach (var reportModel in data)
            {
                counter++;
                if (Events.IsStartEvent(reportModel.EventName))
                {
                    EventModel newEvent = CerateEventModel(reportModel);
                    var parent = stack.Peek();
                    if (Events.IsUpdateStart(reportModel.EventName))
                    {
                        if (stack.Count > 1)//New BC Update inside a Chain
                        {
                            var lastElement = parent.ChildEvents.Last();
                            lastElement.ChildEvents.Add(newEvent);
                        }
                        else
                        {
                            parent.ChildEvents.Add(newEvent);
                        }
                        stack.Push(newEvent);
                    }
                    else
                    {
                        parent.ChildEvents.Add(newEvent);
                    }
                }
                else if (Events.IsFinishedEvent(reportModel.EventName))
                {
                    if (Events.IsUpdateFinished(reportModel.EventName))
                    {
                        var topEvent = stack.Pop();
                        if (topEvent.Identifier != reportModel.Identifier)
                        {
                            throw new InvalidOperationException("Inner or Outer Event is finished at incorrect order.");
                        }

                        if (topEvent.Name != Events.Update)
                        {
                            throw new InvalidOperationException("Event finished before other events finished.");
                        }

                        topEvent.EndTime = reportModel.EventTime;
                        topEvent.Duration = topEvent.EndTime - topEvent.StartTime;
                    }
                    else
                    {
                        var parent = stack.Peek();
                        var startedEvent = parent.ChildEvents.FirstOrDefault(f => f.IsSameEventFinished(reportModel));

                        if (startedEvent == null)
                        {
                            throw new InvalidOperationException("Event is finished before being started");
                        }

                        startedEvent.EndTime = reportModel.EventTime;
                        startedEvent.Duration = startedEvent.EndTime - startedEvent.StartTime;
                    }
                }
            }
            return stack.Pop();
        }

        private static EventModel CerateEventModel(ReportModel reportModel)
        {
            try
            {
                return new EventModel
                {
                    BCName = reportModel.BCName,
                    Identifier = reportModel.Identifier,
                    Metadata = reportModel.Metadata,
                    Name = reportModel.EventName.Remove(reportModel.EventName.IndexOf("started", StringComparison.InvariantCulture)),
                    StartTime = reportModel.EventTime
                };
            }
            catch (Exception ex)
            {
                throw new TempoEx(ex) { Model = reportModel };
            }
        }

        public class TempoEx : Exception
        {
            public ReportModel Model { get; set; }
            public TempoEx(Exception inner) : base("", inner) { }
        }
    }
}
