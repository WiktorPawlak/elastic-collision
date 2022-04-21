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
        public void TestWallCollision()
        {
            Area a = new(vec(-10, -10), vec(10, 10));
            Ball b = new(5, 0, vec(0, 0), vec(1, 0));
            Assert.True(b.Budge(5).Within(a));
            Assert.Equal(b.Collide(a), b);
            Assert.False(b.Budge(6).Within(a));
            Assert.Equal(b.Budge(6).Collide(a).Velocity, vec(-1, 0));
            Assert.True(b.Budge(6).Collide(a).Budge(2).Location.X <= 5);
            Ball c = new(5, 0, vec(4, -4), vec(1, 1));
            Assert.True(c.Budge(1).Within(a));
            Assert.Equal(c.Collide(a), c);
            Assert.False(c.Budge(3).Within(a));
            Assert.Equal(c.Budge(3).Collide(a).Velocity, vec(-1, 1));
        }

        [Fact]
        public void TestKEcalculation()
        {
            Ball b = new(10, 10, vec(0, 0), vec(1, 0));
            Assert.Equal(5, b.KineticEnergy);
        }


    }
}
