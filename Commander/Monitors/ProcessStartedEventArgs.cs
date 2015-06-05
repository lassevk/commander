using System;
using JetBrains.Annotations;

namespace Commander.Monitors
{
    [PublicAPI]
    public class ProcessStartedEventArgs : ProcessEventArgs
    {
        [PublicAPI]
        public ProcessStartedEventArgs(DateTime timestamp, TimeSpan relativeTimestamp, IProcess process)
            : base(timestamp, relativeTimestamp, process)
        {
        }
    }
}