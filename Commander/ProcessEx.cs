using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public static class ProcessEx
    {
        [PublicAPI, NotNull]
        public static Task<int> ExecuteAsync([NotNull] ProcessStartInfo processStartInfo, [NotNull] params IProcessMonitor[] monitors)
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
