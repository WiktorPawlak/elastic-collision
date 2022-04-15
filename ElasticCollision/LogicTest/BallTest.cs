using System;
using ElasticCollision.Logic;
using Xunit;
using static ElasticCollision.Logic.Vector;

namespace LogicTest
{
    public class BallTest
    {
        [Fact]
        public void TestModificationWorksTheWayIThinkItWorks()
        {
            Ball a = new(10, 20, vec(1, 2), vec(3, 4));
            var b = a with { Location = vec(10, 20) };
            Assert.Equal(vec(1, 2), a.Location);
            Assert.Equal(vec(10, 20), b.Location);
            Assert.Equal(vec(3, 4), b.Velocity);

        }
        [Fact]
        public void TestBallsTouching()
        {
            Ball a = new(5, 0, vec(0, 0), vec(0, 0));
            Ball d = new(15, 0, vec(0, 0), vec(0, 0));
            Ball b = new(5, 0, vec(9, 1), vec(0, 0));
            Ball c = new(5, 0, vec(18, 0), vec(0, 0));
            Assert.True(a.Touching(d));
            Assert.True(a.Touching(b));
            Assert.True(b.Touching(c));
            Assert.False(a.Touching(c));
            Assert.True(d.Touching(c));
        }
        [Fact]
        public void TestBallingArea()
        {
            Ball a = new(5, 0, vec(0, 0), vec(0, 0));
            Ball b = new(5, 0, vec(20, 20), vec(0, 0));
            Ball d = new(15, 0, vec(0, 0), vec(0, 0));
            Area ar1 = new(vec(-10, -10), vec(10, 10));
            Assert.True(a.Within(ar1));
            Assert.False(b.Within(ar1));
            Assert.False(d.Within(ar1));
        }

    }
}
