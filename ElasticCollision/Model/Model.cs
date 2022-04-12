using ElasticCollision.Logic;
using System;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        public Model(LogicAPI collisionLogic = null)
        {
            CollisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
        }

        private readonly LogicAPI CollisionLogic = default;
    }
}
