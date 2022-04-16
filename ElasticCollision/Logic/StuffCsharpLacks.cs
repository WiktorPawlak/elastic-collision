using System;
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
