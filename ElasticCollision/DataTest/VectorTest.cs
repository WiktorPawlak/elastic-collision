using System;
using Xunit;
using static Vector;

namespace DataTest
{
    public class VectorTest
    {
        [Fact]
        public void TestCreation()
        {
            Vector v1 = vec(0, 0);
            Assert.Equal(0, v1.x);
            Assert.Equal(0, v1.y);
            Vector v2 = vec(5, -9);
            Assert.Equal(5, v2.x);
            Assert.Equal(-9, v2.y);
        }

        [Fact]
        public void TestAddition()
        {
            var a = vec(0, 0) + vec(0, 0);
            Assert.Equal(0, a.x);
            Assert.Equal(0, a.y);
            var b = vec(5, 5) + vec(5, 5);
            Assert.Equal(vec(10, 10), b);
        }

        [Fact]
        public void TestMagnitude()
        {
            Assert.Equal(0, vec(0, 0).magnitude());
            Assert.Equal(1, vec(1, 0).magnitude());
            Assert.Equal(2, vec(0, -2).magnitude());
            Assert.Equal(5, vec(3, 4).magnitude());
            Assert.Equal(5, vec(4, 3).magnitude());
            Assert.Equal(5, vec(-4, -3).magnitude());
        }

        [Fact]
        public void TestNegation()
        {
            Assert.Equal(vec(0, 0), -vec(0, 0));
            Assert.Equal(vec(5, -3), -vec(-5, 3));
        }

        [Fact]
        public void TestSubtraction()
        {
            Assert.Equal(vec(0, 0), vec(0, 0) - vec(0, 0));
            Assert.Equal(vec(0, 0), vec(0, 1) - vec(0, 1));
            Assert.Equal(vec(-4, 4), vec(1, 5) - vec(5, 1));
        }

        [Fact]
        public void TestDistance()
        {
            Assert.Equal(0, distance(vec(0, 0), vec(0, 0)));
            Assert.Equal(0, distance(vec(-1, 1), vec(-1, 1)));
            Assert.Equal(5, distance(vec(10, -10), vec(7, -6)));
        }



    }
}
