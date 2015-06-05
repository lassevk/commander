using System.Diagnostics;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public class DebugProcessMonitor : IProcessMonitor
    {
        [PublicAPI]
        public DebugProcessMonitor()
        {
        }

        [PublicAPI]
        public void Started(IProcess process)
        {
            Debug.WriteLine($"{process.ExecutionDuration}: <started>");
        }

        [PublicAPI]
        public void Exited(IProcess process, int exitCode)
        {
            Debug.WriteLine($"{process.ExecutionDuration}: <exited: {exitCode}>");
        }

        [PublicAPI]
        public void Error(IProcess process, string line)
        {
            Debug.WriteLine($"{process.ExecutionDuration}: <error> {line}");
        }

        [PublicAPI]
        public void Output(IProcess process, string line)
        {
            Debug.WriteLine($"{process.ExecutionDuration}: {line}");
        }
    }
}