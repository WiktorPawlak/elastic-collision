using ElasticCollision.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogicTest
{
    public class LogicAPITest
    {
        private readonly LogicAPI _logicAPITest = LogicAPI.CreateCollisionLogic(500,
            500, new DataAPITestFixture());

        [Fact]
        public void TestCreateBallNegative()
        {
            _logicAPITest.StartSimulation();
            Ball ball = new(5, 5, Vector.vec(0, 0), Vector.vec(0, 0));
            Assert.Throws<Exception>(() => _logicAPITest.CreateBall(ball));
        }

        [Fact]
        public void TestAddBallsNegative()
        {
            _logicAPITest.StartSimulation();
            Assert.Throws<Exception>(() => _logicAPITest.AddBalls(10, 10, 10));
        }
    }
}
