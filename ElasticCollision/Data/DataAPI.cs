﻿using ExtensionMethods;
using System;

namespace ElasticCollision.Data
{
    public abstract class DataAPI
    {
        public abstract UpdateBall CheckCollision { get; set; }
        public abstract Area Area { get; }
        public abstract void AddBalls(int count, double radius, double mass);
        public static DataAPI CreateBallData()
        {
            return new BallData();
        }

        private class BallData : DataAPI
        {
            private static readonly Random _rng = new Random();
            private Logger _log = new Logger("Ball.log");
            private static int _ballCounter = 0;
            public override Area Area { get; }
            public override UpdateBall CheckCollision { get; set; }

            public BallData()
            {
                Vector _orientationPoint = Vector.vec(0, 0);
                Vector _worldDimensions = Vector.vec(500, 500);
                Area = Area.FromCorners(_orientationPoint, _worldDimensions);
            }

            public override void AddBalls(int count, double radius, double mass)
            {
                for (int i = 0; i < count; i++)
                {
                    AddBall(radius, mass);
                }
            }

            private void AddBall(double radius, double mass)
            {
                var location = Area.Shrink(radius).GetRandomLocation();

                double x = _rng.NextDoubleInRange(-100, 100);
                double y = _rng.NextDoubleInRange(-100, 100);

                var velocity = new Vector(x, y);

                Ball ball = new Ball(radius, mass, location, velocity);
                new MobileBall(ball, _ballCounter, CheckCollision, _log);
                _ballCounter++;
            }
        }
    }
}
