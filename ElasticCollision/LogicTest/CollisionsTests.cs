using Xunit;
using ElasticCollision.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticCollision.Data;

using static ElasticCollision.Data.Vector;

namespace ElasticCollision.Logic.Tests
{
    public class CollisionsTests
    {
        [Fact]
        public void TestWallCollision()
        {
            Area a = Area.FromCorners(vec(-10, -10), vec(10, 10));
            Ball b = new(5, 1, vec(0, 0), vec(1, 0));
            Assert.True(b.Budge(5).Within(a));
            Assert.Equal(vec(0, 0), Collisions.CollideWalls(a, b));
            Assert.Equal(b.ApplyImpulse(Collisions.CollideWalls(a, b)), b);
            Assert.False(b.Budge(6).Within(a));
            Ball budged = b.Budge(6);
            Assert.Equal(budged.ApplyImpulse(Collisions.CollideWalls(a, budged)).Velocity, vec(-1, 0));
            Ball c = new(5, 1, vec(4, -4), vec(1, 1));
            Ball budgedC = c.Budge(3);
            Assert.Equal(budgedC.ApplyImpulse(Collisions.CollideWalls(a, budgedC)).Velocity, vec(-1, 1));
        }
    }
}