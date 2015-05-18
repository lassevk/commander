using JetBrains.Annotations;

namespace Commander
{
    internal static class Assume
    {
        [ContractAnnotation("false => halt")]
        internal static void That(bool expression)
        {
            // Do nothing
        }
    }
}
