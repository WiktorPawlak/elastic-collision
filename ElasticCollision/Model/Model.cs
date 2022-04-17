using ElasticCollision.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        private readonly LogicAPI _collisionLogic = default;
        private FrameUpdater _frameUpdater;
        public IEnumerable<BallModel> BallModels;
        public readonly int Radius = 15;
        public readonly int Width = 550;
        public readonly int Height = 400;
        public readonly int Mass = 1;

        public delegate void FrameUpdater(IEnumerable<BallModel> ballModels);
        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
            _collisionLogic.AddWatcher(Update);
        }

        public void GiveBalls(int ballsCount)
        {
            _collisionLogic.StopSimulation();
            _collisionLogic.AddBalls(ballsCount, Radius, Mass);
            _collisionLogic.StartSimulation();
        }

        public void Update(WorldState state)
        {
            BallModels = state.Balls.Select(ball => new BallModel(ball));
            _frameUpdater.Invoke(BallModels);
        }

        public void AddFrameUpdater(FrameUpdater frameUpdater)
        {
            _frameUpdater = frameUpdater;
        }
    }
}
