using System;
using JetBrains.Annotations;

namespace Commander.Events
{
    [PublicAPI]
    public class ProcessStartedEvent : ProcessEvent
    {
        [PublicAPI]
        public ProcessStartedEvent(TimeSpan relativeTimestamp, DateTime timestamp)
            : base(relativeTimestamp, timestamp)
        {
        }

        [PublicAPI, NotNull]
        public override string ToString()
        {
            return $"{base.ToString()}: <started>";
        }
    }
}