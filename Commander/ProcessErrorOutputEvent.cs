using System;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public class ProcessErrorOutputEvent : ProcessOutputEvent
    {
        [PublicAPI]
        public ProcessErrorOutputEvent(TimeSpan relativeTimestamp, DateTime timestamp, [NotNull] string line)
            : base(relativeTimestamp, timestamp, line)
        {
        }
    }
}