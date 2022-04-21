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
        private readonly LogicAPI _logicAPITest = LogicAPI.CreateCollisionLogic(new DataAPITestFixture());

        [Fact]
        public void TestAddBallsNegative()
        {
            _logicAPITest.StartSimulation();
            Assert.Throws<Exception>(() => _logicAPITest.AddBalls(10, 10, 10));
        }
    }
}
