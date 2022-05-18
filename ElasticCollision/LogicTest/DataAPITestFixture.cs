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
            throw new NotImplementedException();
        }

        public override WorldState GetState()
        {
            throw new NotImplementedException();
        }

        public override void SetState(WorldState newState)
        {
            throw new NotImplementedException();
        }

        public override void AddBalls(int count, double radius, double mass)
        {
            throw new NotImplementedException();
        }

        public override void MoveBalls(double v)
        {
            throw new NotImplementedException();
        }
    }
}
