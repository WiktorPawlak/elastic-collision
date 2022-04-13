using System;
using Xunit;
using ElasticCollision.Data;

namespace DataTest
{
    public class VectorTest
    {
        public delegate Vector Vec(double x, double y);
        public readonly Vec vec = Vector.CreateVector;

        [Fact]
        public void TestCreation()
        {
            Vector v1 = vec(0, 0);
            Assert.Equal(0, v1.X);
            Assert.Equal(0, v1.Y);
            Vector v2 = vec(5, -9);
            Assert.Equal(5, v2.X);
            Assert.Equal(-9, v2.Y);
        }

        [Fact]
        public void TestAddition()
        {
            var a = vec(0, 0) + vec(0, 0);
            Assert.Equal(0, a.X);
            Assert.Equal(0, a.Y);
            var b = vec(5, 5) + vec(5, 5);
            Assert.Equal(vec(10, 10), b);
        }

        [Fact]
        public void TestMagnitude()
        {
            Assert.Equal(0, vec(0, 0).Magnitude());
            Assert.Equal(1, vec(1, 0).Magnitude());
            Assert.Equal(2, vec(0, -2).Magnitude());
            Assert.Equal(5, vec(3, 4).Magnitude());
            Assert.Equal(5, vec(4, 3).Magnitude());
            Assert.Equal(5, vec(-4, -3).Magnitude());
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
            Assert.Equal(0, Vector.Distance(vec(0, 0), vec(0, 0)));
            Assert.Equal(0, Vector.Distance(vec(-1, 1), vec(-1, 1)));
            Assert.Equal(5, Vector.Distance(vec(10, -10), vec(7, -6)));
        }

        [Fact]
        public void TestScaling()
        {
            Assert.Equal(vec(0, 0), vec(53, 531) * 0);
            Assert.Equal(vec(0, 0), 0 * vec(53, 531));
            Assert.Equal(vec(-1, 1), -0.5 * vec(2, -2));
        }
    }
}
