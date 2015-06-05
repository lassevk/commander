using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public class ProcessEventsMonitor : IProcessMonitor
    {
        [NotNull]
        private readonly List<ProcessEvent> _Events = new List<ProcessEvent>();

        [PublicAPI]
        public ProcessEventsMonitor()
        {
        }

        [PublicAPI]
        public void Started(IProcess process)
        {
            _Events.Add(new ProcessStartedEvent(process.ExecutionDuration, DateTime.Now));
        }

        [PublicAPI]
        public void Exited(IProcess process, int exitCode)
        {
            _Events.Add(new ProcessExitedEvent(process.ExecutionDuration, DateTime.Now, exitCode));
        }

        [PublicAPI]
        public void Error(IProcess process, string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            _Events.Add(new ProcessErrorOutputEvent(process.ExecutionDuration, DateTime.Now, line));
        }

        [PublicAPI]
        public void Output(IProcess process, string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            _Events.Add(new ProcessStandardOutputEvent(process.ExecutionDuration, DateTime.Now, line));
        }

        [PublicAPI, NotNull, ItemNotNull]
        public List<ProcessEvent> Events
        {
            get
            {
                return _Events;
            }
        }
    }
}