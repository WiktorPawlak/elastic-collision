using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElasticCollision.Logic;
using Xunit;

namespace LogicTest
{
    public class LogicAPITest
    {
        private readonly LogicAPI sub = LogicAPI.CreateCollisionLogic(new DataAPITestFixture());

        [Fact]
        public void TestAddBallsNegative()
        {
            sub.StartSimulation();
            Assert.Throws<Exception>(() => sub.AddBalls(10, 10, 10));
        }
        [Fact]
        public void TestAddingBalls()
        {
            Assert.Empty(sub.GetCurrentState().Balls);
            sub.AddBalls(1, 5, 6);
            Assert.Equal(5, sub.GetCurrentState().Balls.First().Radius);
            Assert.Equal(6, sub.GetCurrentState().Balls.First().Mass);
            Assert.Single(sub.GetCurrentState().Balls);
            sub.AddBalls(10, 1, 1);
            Assert.Equal(11, sub.GetCurrentState().Balls.Count());
            sub.StartSimulation();
            sub.StopSimulation();
            sub.AddBalls(1, 1, 1);
            Assert.Equal(12, sub.GetCurrentState().Balls.Count());
        }
        [Fact]
        public void testSimulationSimple()
        {
            sub.AddBalls(10, 10, 10);
            var old = sub.GetCurrentState();
            Assert.Equal(old, sub.GetCurrentState());
            sub.NextTick();
            Assert.Equal(10, sub.GetCurrentState().Balls.Count());
            Assert.NotEqual(old, sub.GetCurrentState());

        }

        private class CallCounter
        {
            public int count { get; set; }
            public void Update(WorldState state)
            {
                count++;
            }
        }
        [Fact]
        public void TestSimulationHappens()
        {
            var ctr = new CallCounter();
            sub.AddObserver(ctr.Update);
            Assert.Equal(0, ctr.count);
            sub.StartSimulation();
            Thread.Sleep(50);
            sub.StopSimulation();
            Assert.NotEqual(0, ctr.count);
            Assert.True(50 > ctr.count); // aż tak szybko nie chcemy

        }

    }
}
