using System;
using System.Threading;
using ElasticCollision.Logic;
using Xunit;
namespace LogicTest
{
    public class TickerTest
    {
        private class Counter
        {
            public int count;
            public void inc() => count++;
        }
        [Fact]
        public void TestInitialization() //unu
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            Thread.Sleep(20);
            Assert.Equal(0, ctr.count);
        }

        [Fact]
        public void TestStarting() //unu
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 100);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(50);
            Assert.Equal(1, ctr.count);
        }

        [Fact]
        public void TestStopping() //unu
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 1);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            ticker.Stop();
            Thread.Sleep(15);
            Assert.True(3 > ctr.count);
        }

        [Fact]
        public void TestSpeed2() //unu
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
        public void TestSlowCounter() //unu
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
        public void TestTimeIndependence() //unu
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
