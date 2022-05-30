using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElasticCollision.Data;

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
            private object _frameDrop = new();

            private class BallDictionary
            {
                private Dictionary<int, Ball> _map = new Dictionary<int, Ball>();

                public Ball this[int index]
                {
                    set { _map[index] = value; }
                }

                public List<Ball> GetNeighbours(Area area, Ball ball) => GetTree(area).Neighbors(ball);

                public List<BallLogic> GetLogicBalls() => _map.Values.Select(ball => new BallLogic(ball)).ToList();

                private NonBinaryTree GetTree(Area area) => NonBinaryTree.MakeTree(area.Shrink(-20), _map.Values);
            }

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _dataLayer = dataLayerAPI;
                _dataLayer.CheckCollision = Collide;
            }

            public override void AddBalls(int count, double radius, double mass) => _dataLayer.AddBalls(count, radius, mass);

            private Vector Collide(Ball ball, int index)
            {
                lock (_frameDrop)
                {
                    _ballDictionary[index] = ball;
                    Task.Run(() => Observable.Notify(_ballDictionary.GetLogicBalls()));
                    return Collisions.CalculateForces(ball, _dataLayer.Area, _ballDictionary.GetNeighbours(_dataLayer.Area, ball));
                }
            }
        }
    }
}
