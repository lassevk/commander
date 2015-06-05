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
        public static async Task<int> ExecuteAsync([NotNull] ProcessStartInfo processStartInfo, [NotNull] params IProcessMonitor[] monitors)
        {
            if (processStartInfo == null)
                throw new ArgumentNullException(nameof(processStartInfo));
            if (monitors == null)
                throw new ArgumentNullException(nameof(monitors));

            using (var execution = new ProcessExecution(processStartInfo, monitors))
            {
                return await execution.ExecuteAsync();
            }
        }
    }
}
