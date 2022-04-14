using ElasticCollision.Logic;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {
        private ObservableCollection<Ball> _balls;
        public NotifyCollectionChangedEventHandler CollectionChangedHandler;
        public abstract void RequestBall(double radius, double mass, Vector loc, Vector velocity);
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
                _balls = new ObservableCollection<Ball>();
                _balls.CollectionChanged += (sender, args) => CollectionChangedHandler.Invoke(sender, args);
            }

            public override void RequestBall(double radius, double mass, Vector loc, Vector velocity)
            {
                Ball ball = new(radius, mass, loc, velocity);
                //if (this._collisionSolver.IsPositionOccupied(ball))
                //{
                //    throw new ArgumentException("Position is already ocupied.");
                //}
                _balls.Add(ball);
            }

        }


    }
}
