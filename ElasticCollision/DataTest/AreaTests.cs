using System;
using ElasticCollision.Data;
using Xunit;
using static ElasticCollision.Data.Vector;

namespace DataTest
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
        [Fact]
        public void TestBorders()
        {
            var TopLeft = vec(0, 10);
            var BottomRight = vec(20, 60);
            Area a = new(TopLeft, BottomRight);
            Assert.Equal(0, a.Left);
            Assert.Equal(20, a.Right);
            Assert.Equal(10, a.Top);
            Assert.Equal(60, a.Bottom);
        }

        [Fact]
        public void TestSplitting()
        {
            var TopLeft = vec(0, 0);
            var BottomRight = vec(20, 60);
            Area parent = new(TopLeft, BottomRight);
            var (h1, h2) = parent.SplitHorizontally();
            var (v1, v2) = parent.SplitVertically();
            // unchanged
            Assert.Equal(BottomRight, h2.LowerRightCorner);
            Assert.Equal(TopLeft, h1.UpperLeftCorner);

            Assert.Equal(TopLeft, v1.UpperLeftCorner);
            Assert.Equal(BottomRight, v2.LowerRightCorner);
            // midpoints
            Assert.Equal(vec(20, 30), h1.LowerRightCorner);
            Assert.Equal(vec(0, 30), h2.UpperLeftCorner);

            Assert.Equal(vec(10, 60), v1.LowerRightCorner);
            Assert.Equal(vec(10, 0), v2.UpperLeftCorner);



        }
    }
}
