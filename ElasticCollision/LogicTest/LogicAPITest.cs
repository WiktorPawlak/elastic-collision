using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElasticCollision.Logic;
using Xunit;
using static LogicTest.DataAPITestFixture;

namespace LogicTest
{
    public class LogicAPITest
    {
        private readonly LogicAPI sub = LogicAPI.CreateCollisionLogic(new DataAPITestFixture());

        private class CallCounter
        {
            public int count;
            public void Update(List<BallLogic> state)
            {
                count++;
            }
        }
        [Fact]
        public void TestSimulationHappens()
        {
            var ctr = new CallCounter();
            sub.Observable.Add(ctr.Update);
            Assert.Equal(0, ctr.count);
            sub.AddBalls(10, 10, 10);
            Thread.Sleep(10);
            Assert.Equal(1, ctr.count);
            sub.NextTick();
            Thread.Sleep(10);
            Assert.Equal(2, ctr.count);
            sub.StartSimulation();
            Thread.Sleep(50);
            sub.StopSimulation();
            Thread.Sleep(10);
            Assert.True(ctr.count > 2);
            Assert.True(50 > ctr.count); // aż tak szybko nie chcemy
        }

    }
}
