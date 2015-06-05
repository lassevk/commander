using System;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public class ProcessStartedEvent : ProcessEvent
    {
        [PublicAPI]
        public ProcessStartedEvent(TimeSpan relativeTimestamp, DateTime timestamp)
            : base(relativeTimestamp, timestamp)
        {
        }
    }
}