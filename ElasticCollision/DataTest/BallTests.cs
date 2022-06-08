using ElasticCollision.Data;
using Xunit;
using static ElasticCollision.Data.Vector;

namespace DataTest
{
    public class BallTests
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
        public void TestBallingArea()
        {
            Ball a = new(5, 0, vec(0, 0), vec(0, 0));
            Ball b = new(5, 0, vec(20, 20), vec(0, 0));
            Ball d = new(15, 0, vec(0, 0), vec(0, 0));
            Area ar1 = Area.FromCorners(vec(-10, -10), vec(10, 10));
            Assert.True(ar1.FullyContains(a));
            Assert.False(ar1.FullyContains(b));
            Assert.False(ar1.FullyContains(d));
        }
        [Fact]
        public void TestMovement()
        {
            Ball a = new(5, 0, vec(0, 0), vec(0, 0));
            Ball b = new(5, 0, vec(0, 0), vec(1, 0));
            Ball c = new(5, 0, vec(5, 0), vec(0, 5));
            Assert.Equal(a.Budge(0).Location, vec(0, 0));
            Assert.Equal(a.Budge(20).Location, vec(0, 0));
            Assert.Equal(b.Budge(0).Location, vec(0, 0));
            Assert.Equal(b.Budge(1).Location, vec(1, 0));
            Assert.Equal(b.Budge(10).Location, vec(10, 0));
            Assert.Equal(c.Budge(10).Location, vec(5, 50));
        }

        [Fact]
        public void TestMomentum()
        {
            Ball a = new(10, 10, vec(0, 0), vec(1, 0));
            Ball b = new(10, 10, vec(0, 0), vec(0, 0));
            Ball c = new(1, 1, vec(0, 0), vec(0, 0));
            Assert.Equal(vec(10, 0), a.Momentum);
            Assert.Equal(vec(0, 0), b.Momentum);
            Assert.Equal(vec(1, 0), b.ApplyImpulse(vec(10, 0)).Velocity);
            Assert.Equal(vec(0, 0), a.ApplyImpulse(vec(-10, 0)).Velocity);
            Assert.Equal(vec(10, 0), c.ApplyImpulse(vec(10, 0)).Velocity);
        }


    }
}
