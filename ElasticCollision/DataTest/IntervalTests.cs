using ElasticCollision.Data;
using Xunit;
using static ElasticCollision.Data.Vector;

namespace DataTest
{
    public class IntervalTest
    {
        [Fact]
        public void TestShrinking()
        {
            Interval i = new Interval(0, 10);
            Assert.True(i.Contains(5));
            Assert.False(i.Contains(-5));
            Assert.False(i.Contains(15));
        }
        [Fact]
        public void TestSplitting()
        {
            var (l, r) = new Interval(0, 10).Split();
            Assert.Equal(0, l.low);
            Assert.Equal(5, l.high);
            Assert.Equal(5, r.low);
            Assert.Equal(10, r.high);
            var h = new HorizontalInterval(0, 10);
            var (hl, hr) = h.Split();
            var (hll, hrr) = h.SplitSection();
            Assert.Same(h.GetType(), hl.GetType());
            Assert.Same(h.GetType(), hr.GetType());
            Assert.Same(hl.GetType(), hll.GetType());
            Assert.NotSame(l.GetType(), hll.GetType());
            Assert.Same(h.GetType(), hrr.GetType());

        }
        [Fact]
        public void TestSection()
        {
            var b1 = new Ball(5, 5, vec(0, 0), vec(0, 0));
            var b2 = new Ball(5, 5, vec(6, 6), vec(0, 0));
            var b3 = new Ball(5, 5, vec(6, -6), vec(0, 0));
            var b4 = new Ball(5, 5, vec(-6, 6), vec(0, 0));
            var h = new HorizontalInterval(0, 12);
            var v = new VerticalInterval(0, 12);

            Assert.True(v.Intersects(b1));
            Assert.True(h.Intersects(b1));
            Assert.False(v.FullyContains(b1));
            Assert.False(h.FullyContains(b1));

            Assert.True(v.Intersects(b2));
            Assert.True(h.Intersects(b2));
            Assert.True(v.FullyContains(b2));
            Assert.True(h.FullyContains(b2));


            Assert.False(v.Intersects(b3));
            Assert.True(h.Intersects(b3));
            Assert.False(v.FullyContains(b3));
            Assert.True(h.FullyContains(b3));

            Assert.True(v.Intersects(b4));
            Assert.False(h.Intersects(b4));
            Assert.True(v.FullyContains(b4));
            Assert.False(h.FullyContains(b4));

        }

    }
}
