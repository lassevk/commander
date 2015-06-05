using System;
using System.Collections.Generic;
using Commander.Events;
using JetBrains.Annotations;

namespace Commander.Monitors
{
    [PublicAPI]
    public class ProcessCollectOutputMonitor : IProcessMonitor
    {
        [NotNull]
        private readonly List<ProcessEvent> _Events = new List<ProcessEvent>();

        [PublicAPI]
        public ProcessCollectOutputMonitor()
        {
        }

        void IProcessMonitor.Started(IProcess process)
        {
            _Events.Add(new ProcessStartedEvent(process.ExecutionDuration, DateTime.Now));
        }

        void IProcessMonitor.Exited(IProcess process, int exitCode)
        {
            _Events.Add(new ProcessExitedEvent(process.ExecutionDuration, DateTime.Now, exitCode));
        }

        void IProcessMonitor.Error(IProcess process, string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            _Events.Add(new ProcessErrorOutputEvent(process.ExecutionDuration, DateTime.Now, line));
        }

        void IProcessMonitor.Output(IProcess process, string line)
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