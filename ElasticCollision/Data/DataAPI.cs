using System;
using System.Collections.Generic;

namespace ElasticCollision.Data
{
    public abstract class DataAPI
    {
        public abstract void ApplyForces(List<Vector> forces);
        public abstract WorldState GetState();
        public abstract void SetState(WorldState newState); // temp
        public abstract void AddBalls(int count, double radius, double mass);
        public abstract void MoveBalls(double v);
        public static DataAPI CreateBallData()
        {
            return new BallData();
        }

        private class BallData : DataAPI
        {
            private WorldState _state;
            private readonly Vector _orientationPoint;
            private readonly Vector _worldDimensions;

            public BallData()
            {
                _orientationPoint = Vector.vec(0, 0);
                _worldDimensions = Vector.vec(500, 500);
                _state = new(new List<Ball>(), new Area(_orientationPoint, _worldDimensions));
            }

            public override void ApplyForces(List<Vector> forces)
            {
                _state = _state.ApplyForces(forces);
            }

            public override WorldState GetState()
            {
                return _state;
            }

            public override void SetState(WorldState newState)
            {
                _state = newState;
            }

            public override void AddBalls(int count, double radius, double mass)
            {
                _state = _state.AddBalls(count, radius, mass);
            }

            public override void MoveBalls(double v)
            {
                _state = _state.Proceed(v);
            }
        }
    }
}
