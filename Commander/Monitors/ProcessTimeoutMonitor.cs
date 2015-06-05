using System;
using System.Threading;
using JetBrains.Annotations;

namespace Commander.Monitors
{
    [PublicAPI]
    public class ProcessTimeoutMonitor : IProcessMonitor, IDisposable
    {
        private readonly TimeSpan _Timeout;
        private readonly bool _ResetOnOutput;
        private IProcess _Process;
        private readonly object _Lock = new object();
        private Timer _Timer;

        [PublicAPI]
        public ProcessTimeoutMonitor(TimeSpan timeout, bool resetOnOutput = false)
        {
            _Timeout = timeout;
            _ResetOnOutput = resetOnOutput;
        }

        [PublicAPI]
        public ProcessTimeoutMonitor(bool resetOnOutput = false)
            : this(TimeSpan.FromSeconds(90), resetOnOutput)
        {
        }

        void IProcessMonitor.Started(IProcess process)
        {
            lock (_Lock)
            {
                _Timer = new Timer(TimerCallback, null, _Timeout, Timeout.InfiniteTimeSpan);
                _Process = process;
            }
        }

        private void TimerCallback(object state)
        {
            bool shouldTerminate;
            lock (_Lock)
            {
                shouldTerminate = _Timer != null;
                _Timer?.Dispose();
                _Timer = null;
            }

            if (shouldTerminate)
                _Process?.Terminate();
            _Process = null;
        }

        void IProcessMonitor.Exited(IProcess process, int exitCode)
        {
            lock (_Lock)
            {
                _Timer?.Dispose();
                _Timer = null;
                _Process = null;
            }
        }

        void IProcessMonitor.Error(IProcess process, string line)
        {
            if (_ResetOnOutput)
                lock (_Lock)
                    _Timer?.Change(_Timeout, Timeout.InfiniteTimeSpan);
        }

        void IProcessMonitor.Output(IProcess process, string line)
        {
            if (_ResetOnOutput)
                lock (_Lock)
                    _Timer?.Change(_Timeout, Timeout.InfiniteTimeSpan);
        }

        void IDisposable.Dispose()
        {
            lock (_Lock)
            {
                _Timer?.Dispose();
                _Timer = null;
                _Process = null;
            }
        }
    }
}
