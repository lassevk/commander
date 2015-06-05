using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public interface IProcessMonitor
    {
        [PublicAPI]
        void Started([NotNull] IProcess process);

        [PublicAPI]
        void Exited([NotNull] IProcess process, int exitCode);

        [PublicAPI]
        void Error([NotNull] IProcess process, string line);

        [PublicAPI]
        void Output([NotNull] IProcess process, string line);
    }
}