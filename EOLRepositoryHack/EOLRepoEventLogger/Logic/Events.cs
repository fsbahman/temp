namespace EOLRepoEventLogger.Logic
{
    public static class Events
    {
        public static bool IsUpdateStart(string eventName)
        {
            return eventName == Update + Started;
        }

        public static bool IsUpdateFinished(string eventName)
        {
            return eventName == Update + Finished ||
                eventName == Finishwithexception ||
                eventName == Finishnoaction;
        }

        public static bool IsStartEvent(string eventName)
        {
            return eventName.EndsWith(Started);
        }

        public static bool IsFinishedEvent(string eventName)
        {
            return eventName.EndsWith(Finished) ||
                eventName == Finishwithexception ||
                eventName == Finishnoaction; ;
        }

        public const string Started = "started";
        public const string Finished = "finished";

        public const string Finishwithexception = "finishwithexception";
        public const string Finishnoaction = "finishnoaction";

        public const string Transaction = "transaction";

        public const string Update = "update";

        public const string Setdefaults = "setdefaults";

        public const string Transformation = "transformation";

        public const string Validation = "validation";

        public const string Setdefault2 = "setdefault2";

        public const string Preprocess = "preprocess";

        public const string Updateusescomponents = "updateusescomponents";

        public const string Updatemain = "updatemain";

        public const string Updateused = "updateused";

        public const string Postprocess = "postprocess";

        public const string Audit = "audit";

        public const string Catch = "catch";

        public const string Log = "log";

        public const string Commit = "commit";

        public const string Clearlastcache = "clearlastcache";
        
        public readonly static string[] AllEvents = {
            "finishwithexception",
            "finishnoaction",
            "update",
            "setdefaults",
            "transformation",
            "validation",
            "setdefault2",
            "preprocess",
            "updateusescomponents",
            "updatemain",
            "updateused",
            "postprocess",
            "audit",
            "catch",
            "log",
            "commit",
            "clearlastcache"
        };

    }
}
