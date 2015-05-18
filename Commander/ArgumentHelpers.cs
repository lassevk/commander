using System;
using JetBrains.Annotations;

namespace Commander
{
    internal static class ArgumentHelpers
    {
        internal static T ArgumentNotNull<T>([NotNull] this T value, string argumentName)
            where T : class
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);

            return value;
        }
    }
}
