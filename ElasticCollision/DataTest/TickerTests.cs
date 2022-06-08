using ElasticCollision.Data;
using System.Threading;
using Xunit;
namespace DataTest
{
    public class TickerTests
    {
        private class Counter
        {
            public int count;
            public void inc() => count++;
        }
        [Fact]
        public void TestInitialization()
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            Thread.Sleep(20);
            Assert.Equal(0, ctr.count);
        }

        [Fact]
        public void TestStarting()
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 100);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(50);
            Assert.Equal(1, ctr.count);
        }

        [Fact]
        public void TestStopping()
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(15);
            Assert.True(3 > ctr.count);
        }

        [Fact]
        public void TestSpeed2()
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(100);
            Assert.True(4 < ctr.count);
            Assert.True(20 > ctr.count);
        }

        private class SlowCounter
        {
            public int count;
            int delay;
            public SlowCounter(int delay) => this.delay = delay;
            public void inc()
            {
                Thread.Sleep(delay);
                count++;
            }
        }
        [Fact]
        public void TestSlowCounter()
        {
            var ctr = new SlowCounter(0);
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(100);
            Assert.True(4 < ctr.count);
            Assert.True(20 > ctr.count);
        }
        [Fact]
        public void TestTimeIndependence()
        {
            var ctr = new SlowCounter(3);
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(100);
            Assert.True(4 < ctr.count);
            Assert.True(20 > ctr.count);
        }

    }
}
