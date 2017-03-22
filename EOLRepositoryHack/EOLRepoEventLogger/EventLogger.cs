using System;
using System.Collections.Generic;
using EOLRepoEventLogger.Logic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace EOLRepoEventLogger
{
    public enum EventNames
    {
        update,
        transaction,
        setdefaults,
        transformation,
        validation,
        setdefault2,
        preprocess,
        updateusescomponents,
        updatemain,
        updateused,
        postprocess,
        audit,
        catche,
        log,
        commit,
        clearlastcache
    }

    public class EventLogger
    {
        EventModel rootModel;
        Stack<EventModel> _stack;

        private static EventLogger _instance;

        public static EventLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventLogger();
                }
                return _instance;
            }
        }

        public void Reset()
        {
            _stack = new Stack<Logic.EventModel>();
            rootModel = new EventModel
            {
                Name = "Events"
            };
            _stack.Push(rootModel);
        }

        private EventLogger()
        {
            Reset();
        }

        public void PersistPerExecutaionDay()
        {
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(rootModel);
            var fileName = GetFileNameByExecutionTime();
            System.IO.File.WriteAllText(fileName, jsonData);
        }

        public void BookStartTransaction(Guid id)
        {
            BookStartInnerProcess(id, Events.Transaction);
        }

        public void BookCommitTransaction(Guid id)
        {
            EventModel topModel = BookCompleteInnerProcess(id, Events.Transaction);
            topModel.Metadata = "Transaction is Commited";
        }

        public void BookRollbackTransaction(Guid id)
        {
            var model = BookCompleteInnerProcess(id, Events.Transaction);
            model.Metadata = "Transaction is Rolledback";
        }

        public void BookStartBCUpdate(Guid id, string bcName, string metadata)
        {
            var model = BookStartInnerProcess(id, Events.Update);
            model.BCName = bcName;
            model.Metadata = metadata;
        }

        public void BookEndBCUpdate(Guid id, string metadata)
        {
            var model = BookCompleteInnerProcess(id, Events.Update);
            if (!string.IsNullOrEmpty(metadata))
            {
                model.Metadata = metadata;
            }
        }

        private EventModel BookStartInnerProcess(Guid id, string eventName)
        {
            var model = CreateEventModel(id, eventName);

            var top = _stack.Peek();
            // A child like post process is open and before it ends a new update happened
            // there fore new event has to be register under that child.
            if (top.ChildEvents.Any() && top.ChildEvents.Last() != null && top.ChildEvents.Last().Duration == TimeSpan.Zero && _stack.Count > 1)
            {
                top.ChildEvents.Last().ChildEvents.Add(model);
            }
            else
            {
                top.ChildEvents.Add(model);
            }

            /*var top = _stack.Peek();
            if (top.Name != Logic.Events.Transaction &&
                top.ChildEvents.Count > 0
                && top.ChildEvents.Last().Name != Events.Setdefault2)
            {
                var last = top.ChildEvents.Last();
                last.ChildEvents.Add(model);
            }
            else { top.ChildEvents.Add(model); }*/

            _stack.Push(model);
            return model;
        }

        private EventModel BookCompleteInnerProcess(Guid id, string eventName)
        {
            var topModel = _stack.Peek();
            if (topModel.Name != eventName)
            {
                throw new Exception($"{eventName} is closed before being started");
            }
            if (topModel.Identifier != id.ToString())
            {
                throw new Exception($"{eventName} are not in order");
            }
            topModel.EndTime = DateTime.UtcNow;
            topModel.Duration = topModel.EndTime - topModel.StartTime;

            if (_stack.Count > 1)
            {
                _stack.Pop();
            }

            return topModel;
        }

        public void BookStart(Guid identifier, string BcName, string eventName, string metadata)
        {
            var topModel = _stack.Peek();
            var newModel = CreateEventModel(identifier, eventName);
            newModel.BCName = BcName;
            newModel.Metadata = metadata;
            topModel.ChildEvents.Add(newModel);
        }

        public void BookEnd(Guid identifier, string BcName, string eventName, string metadata)
        {
            var topModel = _stack.Peek();
            var lastEvent = topModel.ChildEvents.Last();
            if (lastEvent.Identifier != identifier.ToString())
            {
                throw new Exception("Functional components are not excecuted in order");
            }
            if (lastEvent.Name != eventName)
            {
                throw new Exception("Functional components are not excecuted in order");
            }

            lastEvent.EndTime = DateTime.UtcNow;
            lastEvent.Duration = lastEvent.EndTime - topModel.StartTime;
        }

        private EventModel CreateEventModel(Guid id, string eventName)
        {
            return new Logic.EventModel
            {
                Identifier = id.ToString(),
                StartTime = DateTime.UtcNow,
                Name = eventName,
                EventLocation = GetEventLocation()
            };
        }

        private string GetEventLocation()
        {
            StackTrace _trace = new StackTrace(true);
            var allFrames = _trace.GetFrames();
            var strBuilder = new StringBuilder();

            for (int i = 2; i < allFrames.Length; i++)
            {
                strBuilder.Append($"In {allFrames[i].GetMethod().Name} at {allFrames[i].GetFileName()} ({allFrames[i].GetFileLineNumber()}) $");
            }
            return strBuilder.ToString();
        }

        private string GetFileNameByExecutionTime()
        {
            var path = Environment.CurrentDirectory; //@"D:\code\eoldev\RepositoryHack\WebApp\Sources\";
            var d = DateTime.UtcNow;
            return $@"{path}\Report-{d.Year}-{d.Month}-{d.Day}.csv";
        }
    }
}
