using Xunit;
using ConcurrentProgramming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentProgramming.Tests
{
    public class FractionTests
    {
        Fraction fraction = new(3, 4);

        [Fact()]
        public void EvaluateTest()
        {
            Assert.Equal(0.75, fraction.Evaluate());
        }

        [Fact()]
        public void ToStringTest()
        {
            Assert.Equal("3/4", fraction.ToString());
        }
    }
}