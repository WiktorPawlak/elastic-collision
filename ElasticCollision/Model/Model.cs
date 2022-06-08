using ElasticCollision.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            _collisionLogic.AddBalls(ballsCount, Radius, Mass);
        }

        public void Update(List<BallLogic> balls)
        {
            if (Monitor.TryEnter(_frameDrop))
            {
                try
                {
                    Task.Run(() => Observable.Notify(balls.Select(ball => new BallModel(ball))));
                }
                finally
                {
                    Monitor.Exit(_frameDrop);
                }
            }
        }
    }
}
