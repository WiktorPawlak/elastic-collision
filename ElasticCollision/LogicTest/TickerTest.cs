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
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(15);
            Assert.Equal(1, ctr.count);
        }

        [Fact]
        public void TestSpeed() //unu
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 1);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(100);
            Assert.True(90 < ctr.count);
            Assert.True(110 > ctr.count);
        }

        [Fact]
        public void TestSpeed2() //unu
        {
            var ctr = new Counter();
            var ticker = new Ticker(ctr.inc, 10);
            Assert.Equal(0, ctr.count);
            ticker.Start();
            Thread.Sleep(100);
            Assert.True(8 < ctr.count);
            Assert.True(12 > ctr.count);
        }
    }
}
