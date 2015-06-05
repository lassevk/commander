using System;
using JetBrains.Annotations;

namespace Commander.Monitors
{
    [PublicAPI]
    public class ProcessOutputEventArgs : ProcessEventArgs
    {
        [PublicAPI]
        public ProcessOutputEventArgs(DateTime timestamp, TimeSpan relativeTimestamp, [NotNull] IProcess process, [NotNull] string line)
            : base(timestamp, relativeTimestamp, process)
        {
            Line = line;
            if (line == null)
                throw new ArgumentNullException(nameof(line));
        }

        [PublicAPI, NotNull]
        public string Line
        {
            get;
        }
    }
}