using System;
using JetBrains.Annotations;

namespace Commander.Events
{
    [PublicAPI]
    public abstract class ProcessOutputEvent : ProcessEvent
    {
        [PublicAPI]
        protected ProcessOutputEvent(TimeSpan relativeTimestamp, DateTime timestamp, [NotNull] string line)
            : base(relativeTimestamp, timestamp)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            Line = line;
        }

        [PublicAPI, NotNull]
        public string Line
        {
            get;
        }


        [PublicAPI, NotNull]
        public override string ToString()
        {
            return $"{base.ToString()}: {Line}";
        }
    }
}