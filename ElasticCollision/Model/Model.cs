using ElasticCollision.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        private readonly LogicAPI _collisionLogic = default;
        private ObservableCollection<BallModel> BallModels;
        public readonly int Radius = 15;
        public readonly int Width = 550;
        public readonly int Height = 400;
        public readonly int Mass = 1;

        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
            _collisionLogic.AddWatcher(Update);
        }

        public ObservableCollection<BallModel> GiveBalls(int ballsCount)
        {
            _collisionLogic.AddBalls(ballsCount, Radius, Mass);
            _collisionLogic.StartSimulation();
            return BallModels;
        }

        public void Update(WorldState state)
        {
            var list = state.Balls.Select(ball => new BallModel(ball));
            BallModels = new(list);
        }
    }
}
