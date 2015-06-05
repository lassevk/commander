using System;
using JetBrains.Annotations;

namespace Commander.Monitors
{
    [PublicAPI]
    public class ProcessExitedEventArgs : ProcessEventArgs
    {
        [PublicAPI]
        public ProcessExitedEventArgs(DateTime timestamp, TimeSpan relativeTimestamp, [NotNull] IProcess process, int exitCode)
            : base(timestamp, relativeTimestamp, process)
        {
            ExitCode = exitCode;
        }

        [PublicAPI]
        public int ExitCode
        {
            get;
        }
    }
}