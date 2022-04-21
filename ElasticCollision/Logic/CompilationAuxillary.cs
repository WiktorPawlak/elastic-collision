using System;
using System.ComponentModel;
namespace ExtensionMethods
{
    // why not provide this
    public static class MyExtensions
    {
        public static double NextDoubleInRange(this Random rng, double minimum, double maximum)
        {
            return rng.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}


namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit { }
}
