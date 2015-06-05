using System;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public class ProcessExitedEvent : ProcessEvent
    {
        [PublicAPI]
        public ProcessExitedEvent(TimeSpan executionDuration, DateTime timestamp, int exitCode)
            : base(executionDuration, timestamp)
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