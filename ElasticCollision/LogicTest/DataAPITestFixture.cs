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
        public override void ApplyForces(List<Vector> forces)
        {
            return;
        }

        public override WorldState GetState()
        {
            return null;
        }

        public override void SetState(WorldState newState)
        {
            return;
        }

        public override void AddBalls(int count, double radius, double mass)
        {
            return;
        }

        public override void MoveBalls(double v)
        {
            return;
        }
    }
}
