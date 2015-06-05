using System;
using JetBrains.Annotations;

namespace Commander.Monitors
{
    [PublicAPI]
    public class ProcessEventMonitor : IProcessMonitor
    {
        [PublicAPI]
        public ProcessEventMonitor()
        {
        }

        [PublicAPI]
        public EventHandler<ProcessStartedEventArgs> Started;

        [PublicAPI]
        public EventHandler<ProcessExitedEventArgs> Exited;

        [PublicAPI]
        public EventHandler<ProcessOutputEventArgs> StandardOutput;

        [PublicAPI]
        public EventHandler<ProcessOutputEventArgs> ErrorOutput;

        void IProcessMonitor.Started(IProcess process)
        {
            Started?.Invoke(this, new ProcessStartedEventArgs(DateTime.Now, process.ExecutionDuration, process));
        }

        void IProcessMonitor.Exited(IProcess process, int exitCode)
        {
            Exited?.Invoke(this, new ProcessExitedEventArgs(DateTime.Now, process.ExecutionDuration, process, exitCode));
        }

        void IProcessMonitor.Error(IProcess process, string line)
        {
            ErrorOutput?.Invoke(this, new ProcessOutputEventArgs(DateTime.Now, process.ExecutionDuration, process, line));
        }

        void IProcessMonitor.Output(IProcess process, string line)
        {
            StandardOutput?.Invoke(this, new ProcessOutputEventArgs(DateTime.Now, process.ExecutionDuration, process, line));
        }
    }
}