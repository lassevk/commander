using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public static class ConsoleProcess
    {
        [PublicAPI, NotNull]
        public static Task<int> ExecuteAsync([NotNull] string fileName, [CanBeNull] string arguments = null, [CanBeNull] string workingDirectory = null, [NotNull] params IProcessMonitor[] monitors)
        {
            return ExecuteAsync(new ProcessStartInfo(fileName)
            {
                Arguments = arguments ?? string.Empty,
                WorkingDirectory = workingDirectory ?? string.Empty,
            }, monitors);
        }

        [PublicAPI, NotNull]
        private static Task<int> ExecuteAsync([NotNull] ProcessStartInfo processStartInfo, [NotNull] params IProcessMonitor[] monitors)
        {
            if (processStartInfo == null)
                throw new ArgumentNullException(nameof(processStartInfo));
            if (monitors == null)
                throw new ArgumentNullException(nameof(monitors));

            var execution = new ProcessExecution(processStartInfo, monitors);
            var task = execution.ExecuteAsync();
            return task.ContinueWith(resultTask =>
            {
                ((IDisposable)execution).Dispose();
                return task.Result;
            });
        }
    }
}
