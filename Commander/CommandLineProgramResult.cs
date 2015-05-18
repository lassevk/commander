using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public class CommandLineProgramResult
    {
        private int _ExitCode;
        private readonly List<IOutputLine> _Output;

        [PublicAPI]
        public CommandLineProgramResult(int exitCode, IEnumerable<IOutputLine> output)
        {
            _ExitCode = exitCode;
            _Output = output.ToList();
        }

        [PublicAPI]
        public int ExitCode
        {
            get
            {
                return _ExitCode;
            }
        }
    }
}