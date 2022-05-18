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

    }
}
