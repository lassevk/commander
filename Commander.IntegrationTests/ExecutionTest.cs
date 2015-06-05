using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Commander.Events;
using Commander.Monitors;
using Commander.TestExecutable;

// ReSharper disable AccessToDisposedClosure

namespace Commander.IntegrationTests
{
    public class ExecutionTest
    {
        protected List<ProcessEvent> Execute(string argument, params IProcessMonitor[] monitors)
        {
            using (var timeoutMonitor = new ProcessTimeoutMonitor(TimeSpan.FromSeconds(10)))
            {
                return Task.Run(async () =>
                {
                    var monitor = new ProcessCollectOutputMonitor();
                    var fileName = typeof(Program).Assembly.Location;
                    var psi = new ProcessStartInfo(fileName, argument);

                    monitors = monitors.Concat(new IProcessMonitor[]
                    {
                        monitor,
                        timeoutMonitor
                    }).ToArray();
                    await ProcessEx.ExecuteAsync(psi, monitors);

                    return monitor.Events;
                }).Result;
            }
        }
    }
}