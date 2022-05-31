using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticCollision.Data;
using ElasticCollision.Logic;
using Xunit;
using static ElasticCollision.Data.Vector;
using static ElasticCollision.Logic.Collisions;

namespace ElasticCollision.Logic.Tests
{
    public class CollisionsTests
    {
        //[Fact]
        //public void TestWallCollision()
        //{
        //    Area a = Area.FromCorners(vec(-10, -10), vec(10, 10));
        //    Ball b = new(5, 1, vec(0, 0), vec(1, 0));
        //    Assert.True(a.FullyContains(b.Budge(5)));
        //    Assert.Equal(vec(0, 0), CollideWalls(a, b));
        //    Assert.Equal(b.ApplyImpulse(CollideWalls(a, b)), b);
        //    Assert.False(a.FullyContains(b.Budge(6)));
        //    Ball budged = b.Budge(6);
        //    Assert.Equal(budged.ApplyImpulse(CollideWalls(a, budged)).Velocity, vec(-1, 0));
        //    Ball c = new(5, 1, vec(4, -4), vec(1, 1));
        //    Ball budgedC = c.Budge(3);
        //    Assert.Equal(budgedC.ApplyImpulse(CollideWalls(a, budgedC)).Velocity, vec(-1, 1));
        //}
        //[Fact]
        //public void TestBallImpact() //unu
        //{
        //    Ball a = new(10, 10, vec(-4, 0), vec(1, 0));
        //    Ball b = new(10, 10, vec(4, 0), vec(-1, 0));
        //    Ball c = new(10, 10, vec(4, 3), vec(-1, 0));
        //    Assert.Equal(CollisionImpulse(a, b), -CollisionImpulse(b, a));
        //    Assert.NotEqual(CollisionImpulse(a, b), CollisionImpulse(a, c));
        //    Assert.True(CollisionImpulse(a, c).Y < 0);
        //    Assert.Equal(CollisionImpulse(a, b), vec(-20, 0));
        //}

        [Fact]
        public void TestBallApproaching()
        {
            Ball a = new(10, 10, vec(0, 0), vec(0, 0));
            Ball b_f = new(10, 10, vec(-1, 5), vec(1, 0));
            Ball b_b = new(10, 10, vec(-1, 5), vec(-1, 0));
            Ball a_f = new(10, 10, vec(1, 0), vec(1, 0));
            Ball a_b = new(10, 10, vec(1, 0), vec(-10, 0));

            Assert.True(Approaching(b_f, a));
            Assert.True(Approaching(a_b, a));

            Assert.False(Approaching(a, a));
            Assert.False(Approaching(a_f, a_f));

            Assert.False(Approaching(b_b, a));
            Assert.False(Approaching(a_f, a));
        }
        [Fact]
        public void TestBallsTouching()
        {
            Ball a = new(5, 0, vec(0, 0), vec(0, 0));
            Ball d = new(15, 0, vec(0, 0), vec(0, 0));
            Ball b = new(5, 0, vec(9, 1), vec(0, 0));
            Ball c = new(5, 0, vec(18, 0), vec(0, 0));
            Assert.True(Touching(a, d));
            Assert.True(Touching(a, b));
            Assert.True(Touching(b, c));
            Assert.False(Touching(a, c));
            Assert.True(Touching(d, c));
        }
    }
}
