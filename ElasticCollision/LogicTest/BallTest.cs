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

    }
}
