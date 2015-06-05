using System;
using JetBrains.Annotations;

namespace Commander
{
    [PublicAPI]
    public interface IProcessFeeded
    {
        [PublicAPI]
        void Feed(string text);

        [PublicAPI]
        void FeedLine(string line);
    }
}