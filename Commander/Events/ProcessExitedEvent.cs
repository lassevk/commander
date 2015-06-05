using System;
using JetBrains.Annotations;

namespace Commander.Events
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

        [PublicAPI, NotNull]
        public override string ToString()
        {
            return $"{base.ToString()}: <exited: {ExitCode}>";
        }
    }
}