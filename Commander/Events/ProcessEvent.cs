using System;
using JetBrains.Annotations;

namespace Commander.Events
{
    [PublicAPI]
    public abstract class ProcessEvent
    {
        [PublicAPI]
        protected ProcessEvent(TimeSpan relativeTimestamp, DateTime timestamp)
        {
            RelativeTimestamp = relativeTimestamp;
            Timestamp = timestamp;
        }

        [PublicAPI]
        public DateTime Timestamp
        {
            get;
        }

        [PublicAPI]
        public TimeSpan RelativeTimestamp
        {
            get;
        }

        [PublicAPI, NotNull]
        public override string ToString()
        {
            return $"{Timestamp} (+{RelativeTimestamp})";
        }
    }
}