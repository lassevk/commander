using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Commander
{
    internal class ProcessExecution : IDisposable, IProcess
    {
        [NotNull]
        private readonly Process _Process;

        [NotNull]
        private readonly IProcessMonitor[] _Monitors;

        [NotNull]
        private readonly TaskCompletionSource<int> _ProcessExitedTaskCompletionSource;

        private Stopwatch _Stopwatch;

        public ProcessExecution([NotNull] ProcessStartInfo processStartInfo, [NotNull] IProcessMonitor[] monitors)
        {
            _Monitors = monitors;

            var standardOutputEncoding = Encoding.GetEncoding(850);

            _Process = new Process();
            _Process.StartInfo = processStartInfo;
            _Process.StartInfo.UseShellExecute = false;
            _Process.StartInfo.RedirectStandardError = true;
            _Process.StartInfo.StandardErrorEncoding = standardOutputEncoding;
            _Process.StartInfo.RedirectStandardInput = true;
            _Process.StartInfo.RedirectStandardOutput = true;
            _Process.StartInfo.StandardOutputEncoding = standardOutputEncoding;
            _Process.StartInfo.CreateNoWindow = true;
            _Process.EnableRaisingEvents = true;
            _Process.OutputDataReceived += ProcessOnOutputDataReceived;
            _Process.ErrorDataReceived += ProcessOnErrorDataReceived;
            _Process.Exited += ProcessOnExited;

            _ProcessExitedTaskCompletionSource = new TaskCompletionSource<int>();
        }

        private void ProcessOnExited(object sender, EventArgs eventArgs)
        {
            _Process.WaitForExit();
            foreach (var monitor in _Monitors)
                monitor?.Exited(this, _Process.ExitCode);
            _ProcessExitedTaskCompletionSource.SetResult(_Process.ExitCode);
        }

        private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (dataReceivedEventArgs.Data != null)
            {
                foreach (var monitor in _Monitors)
                    monitor?.Error(this, dataReceivedEventArgs.Data);
            }
        }

        private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (dataReceivedEventArgs.Data != null)
            {
                foreach (var monitor in _Monitors)
                    monitor?.Output(this, dataReceivedEventArgs.Data);
            }
        }

        public Task<int> ExecuteAsync()
        {
            _Process.Start();
            _Process.BeginErrorReadLine();
            _Process.BeginOutputReadLine();
            _Stopwatch = Stopwatch.StartNew();
            foreach (var monitor in _Monitors)
                monitor?.Started(this);

            return _ProcessExitedTaskCompletionSource.Task;
        }

        void IDisposable.Dispose()
        {
            _Process.Dispose();
        }

        void IProcess.Write(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            _Process.StandardInput.Write(text);
        }

        void IProcess.WriteLine(string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            _Process.StandardInput.WriteLine(line);
        }

        public void Terminate()
        {
            _Process.Kill();
        }

        public TimeSpan ExecutionDuration
        {
            get
            {
                return _Stopwatch.Elapsed;
            }
        }

        public int Id
        {
            get
            {
                return _Process.Id;
            }
        }
    }
}