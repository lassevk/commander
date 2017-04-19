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
                var task = Task.Run(async () =>
                {
                    var monitor = new ProcessCollectOutputMonitor();
                    var fileName = typeof(Program).Assembly.Location;

                    monitors = monitors.Concat(new IProcessMonitor[]
                    {
                        monitor,
                        timeoutMonitor
                    }).ToArray();
                    await ConsoleProcess.ExecuteAsync(typeof(Program).Assembly.Location, argument, monitors: monitors);

                    return monitor.Events;
                });
                return task?.Result ?? new List<ProcessEvent>();
            }
        }
    }
}