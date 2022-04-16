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

        public BallModel GiveBall()
        {
            //Ball ball = _collisionLogic.
            throw new NotImplementedException();
        }
    }
}
