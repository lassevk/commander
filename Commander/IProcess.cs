using System;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public interface IProcess
    {
        [PublicAPI]
        void Write([NotNull] string text);

        [PublicAPI]
        void WriteLine([NotNull] string line);

        [PublicAPI]
        void Terminate();

        [PublicAPI]
        TimeSpan ExecutionDuration
        {
            get;
        }

        [PublicAPI]
        int Id
        {
            get;
        }
    }
}