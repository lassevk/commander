using System;
using JetBrains.Annotations;

namespace Commander.Monitors
{
    [PublicAPI]
    public abstract class ProcessEventArgs : EventArgs
    {
        [PublicAPI]
        protected ProcessEventArgs(DateTime timestamp, TimeSpan relativeTimestamp, [NotNull] IProcess process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            Timestamp = timestamp;
            RelativeTimestamp = relativeTimestamp;
            Process = process;
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
        public IProcess Process
        {
            get;
        }
    }
}