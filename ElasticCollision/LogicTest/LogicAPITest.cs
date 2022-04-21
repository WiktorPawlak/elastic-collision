using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            sub.AddBalls(1, 1, 1);
            Assert.Single(sub.GetCurrentState().Balls);
            sub.AddBalls(10, 1, 1);
            Assert.Equal(11, sub.GetCurrentState().Balls.Count());
        }

    }
}
