using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ElasticCollision.Logic;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        public readonly Observable<IEnumerable<BallModel>> Observable = new();
        private readonly LogicAPI _collisionLogic = default;
        public IEnumerable<BallModel> BallModels;
        public readonly int Radius = 15;
        public readonly int Width = 500;
        public readonly int Height = 500;
        public readonly int Mass = 1;
        private object _frameDrop = new();

        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
            _collisionLogic.Observable.Add(Update);
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
                    Observable.Notify(BallModels);
                }
                finally
                {
                    Monitor.Exit(_frameDrop);
                }
            }
        }
    }
}
