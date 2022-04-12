using System;

namespace ElasticCollision.Presentation
{
    public class ViewModel
    {
        public ViewModel(Model collisionModel = default)
        {
            CollisionModel = collisionModel ?? new Model();
        }

        private Model CollisionModel { get; set; }
    }
}
