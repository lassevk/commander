using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public interface IOutputLine
    {
        [PublicAPI]
        string Text
        {
            get;
        }

        [PublicAPI]
        bool IsError
        {
            get;
        }
    }
}