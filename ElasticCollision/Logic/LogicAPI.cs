using ElasticCollision.Data;
using System.Collections.Generic;
using System.Linq;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {
        public abstract Observable<List<BallLogic>> Observable { get; }
        public abstract void AddBalls(int count, double radius, double mass);
        public static LogicAPI CreateCollisionLogic(DataAPI data = default)
        {
            return new CollisionLogic(data ?? DataAPI.CreateBallData());
        }

        private class CollisionLogic : LogicAPI
        {
            private readonly DataAPI _dataLayer;
            public override Observable<List<BallLogic>> Observable { get; } = new Observable<List<BallLogic>>();
            private BallDictionary _ballDictionary = new BallDictionary();
            private object _guard = new();

            private class BallDictionary
            {
                private Dictionary<int, BallWithJunk> _map = new Dictionary<int, BallWithJunk>();

                public void Update(BallWithJunk ball) => _map[-ball.Id] = ball;

                public List<Ball> GetNeighbours(Area area, Ball ball) => GetTree(area).Neighbors(ball);

                public List<BallLogic> GetLogicBalls() => _map.Values.Select(ball => new BallLogic(ball)).ToList();

                private NonBinaryTree GetTree(Area area) => NonBinaryTree.MakeTree(area.Shrink(-20), _map.Values);
            }

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _dataLayer = dataLayerAPI;
                _dataLayer.CheckCollision = Collide;
            }

            public override void AddBalls(int count, double radius, double mass)
            {
                _dataLayer.AddBalls(count, radius, mass);
            }


            private void Collide(BallWithJunk ball)
            {
                lock (_guard)
                {
                    _ballDictionary.Update(ball);
                    Observable.Notify(_ballDictionary.GetLogicBalls());

                    var changedBalls = Collisions
                        .Collide(ball, _dataLayer.Area, _ballDictionary.GetNeighbours(_dataLayer.Area, ball));

                    foreach (BallWithJunk bl in changedBalls)
                    {
                        _ballDictionary.Update(bl);
                    }
                    _ballDictionary.Update(ball.Info());
                }
            }
        }
    }
}
