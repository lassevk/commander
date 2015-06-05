using System;
using JetBrains.Annotations;

namespace Commander
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
    }
}