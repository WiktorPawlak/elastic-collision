using ElasticCollision.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        private readonly LogicAPI _collisionLogic = default;
        private FrameUpdater _frameUpdater;
        public IEnumerable<BallModel> BallModels;
        public readonly int Radius = 15;
        public readonly int Width = 500;
        public readonly int Height = 500;
        public readonly int Mass = 1;
        private object _frameDrop = new();

        public delegate void FrameUpdater(IEnumerable<BallModel> ballModels);
        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic(Width, Height);
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
            if (Monitor.TryEnter(_frameDrop))
            {
                try
                {
                    BallModels = state.Balls.Select(ball => new BallModel(ball));
                    _frameUpdater.Invoke(BallModels);
                }
                finally
                {
                    Monitor.Exit(_frameDrop);
                }
            }
        }

        public void AddFrameUpdater(FrameUpdater frameUpdater)
        {
            _frameUpdater = frameUpdater;
        }
    }
}
