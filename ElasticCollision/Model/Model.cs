using ElasticCollision.Logic;
using System;
using System.Collections.ObjectModel;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        private readonly LogicAPI _collisionLogic = default;
        private ObservableCollection<object> _balls;

        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
        }
    }
}
