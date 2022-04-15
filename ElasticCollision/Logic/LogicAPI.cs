using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {
        private Random _randomNumPool = new Random();

        public abstract Ball CreateBall(double radius, double mass, Vector loc, Vector velocity);
        public abstract Vector GetRandomLocation(int width, int height);
        public static LogicAPI CreateCollisionLogic(DataAPI data = default)
        {
            return new CollisionLogic(data == null ? DataAPI.CreateBallData() : data);
        }

        private class CollisionLogic : LogicAPI
        {
            private readonly DataAPI _ballData;

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _ballData = dataLayerAPI;
            }

            public override Ball CreateBall(double radius, double mass, Vector loc, Vector velocity)
            {
                return new(radius, mass, loc, velocity);
                //if (this._collisionSolver.IsPositionOccupied(ball))
                //{
                //    throw new ArgumentException("Position is already ocupied.");
                //}
            }

            public override Vector GetRandomLocation(int width, int height)
            {
                Vector loc = new Vector(
                    _randomNumPool.Next(0, width),
                    _randomNumPool.Next(0, height)
                    );
                return loc;
            }


        }
    }
}
