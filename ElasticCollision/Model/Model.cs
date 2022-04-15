using ElasticCollision.Logic;
using System;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        private readonly LogicAPI _collisionLogic = default;

        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
        }

        public void AddBall(int radius)
        {
            //rand loc & validate; if false next rand loc
            //const speed
            //mass??
            _collisionLogic.CreateBall(radius, 10, Vector.vec(50, 50), Vector.vec(10, 10)); //TODO::Random location&predefined speed
        }
    }
}
