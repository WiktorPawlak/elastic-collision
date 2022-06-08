using System;
using System.ComponentModel;

// Target framework of all projects aside from View is .NET standard 2.0.
// This project uses records to facilitate implementation of data integrity,
// thus enforcing us to support compiler in the following way. 

namespace ExtensionMethods
{
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
