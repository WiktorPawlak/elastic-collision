using ElasticCollision.Logic;
using System;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        private readonly LogicAPI _collisionLogic = default;
        public readonly int Radius = 15;
        public readonly int Width = 550;
        public readonly int Height = 400;
        public Vector Velocity = Vector.vec(5, 5);

        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
        }

        public Ball GiveBall()
        {
            //rand loc & validate; if false next rand loc
            //const speed
            //mass??
            //Vector location = _collisionLogic.GetRandomLocation(Width, Height);
            //return _collisionLogic.CreateBall(Radius, 10, location, Velocity);
            //TODO::Random location&predefined speed
        }
    }
}
