using ElasticCollision.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest
{
    public class DataAPITestFixture : DataAPI
    {
        public override UpdateBall CheckCollision { get; set; }

        public override Area Area { get; }

        public override void AddBalls(int count, double radius, double mass)
        {
            return;
        }
    }
}
