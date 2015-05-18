using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public class CommandLineProgram
    {
        [NotNull]
        private readonly string _ExecutableFilename;

        [NotNull]
        private readonly string _Arguments;
        
        [PublicAPI]
        public CommandLineProgram([NotNull] string executableFilename, [NotNull] string arguments)
        {
            if (executableFilename == null)
                throw new ArgumentNullException(nameof(executableFilename));
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            _ExecutableFilename = executableFilename;
            _Arguments = arguments;
        }

        [PublicAPI]
        public CommandLineProgram([NotNull] string executableFilename)
            : this(executableFilename, string.Empty)
        {
        }

        [PublicAPI]
        public CommandLineProgram([NotNull] string executableFilename, [NotNull] params string[] arguments)
            : this(executableFilename, string.Join(" ", arguments.ArgumentNotNull("arguments")))
        {
        }

        [PublicAPI, NotNull]
        public Task<CommandLineProgramResult> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
