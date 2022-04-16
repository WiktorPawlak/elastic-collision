using System;
using ElasticCollision.Logic;
using Xunit;
using static ElasticCollision.Logic.Vector;

namespace LogicTest
{
    public class AreaTest
    {
        [Fact]
        public void TestGettingRandomLocation()
        {
            Area a = new(vec(3, 3), vec(9, 9));
            for (var i = 0; i < 50; i++)
            {
                Assert.True(a.Contains(a.GetRandomLocation()));
            }
        }

    }
}
