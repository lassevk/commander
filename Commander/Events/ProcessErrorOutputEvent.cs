using System;
using JetBrains.Annotations;

namespace Commander.Events
{
    [PublicAPI]
    public class ProcessErrorOutputEvent : ProcessOutputEvent
    {
        [PublicAPI]
        public ProcessErrorOutputEvent(TimeSpan relativeTimestamp, DateTime timestamp, [NotNull] string line)
            : base(relativeTimestamp, timestamp, line)
        {
        }

        // ReSharper disable once AnnotationRedundancyInHierarchy
        [PublicAPI, NotNull]
        public override string ToString()
        {
            return $"{base.ToString()} <error>";
        }
    }
}