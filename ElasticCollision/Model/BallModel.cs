using ElasticCollision.Logic;
using System;

namespace ElasticCollision.Presentation
{
    public class BallModel
    {
        private BallLogic _ball;
        public double X
        {
            get { return _ball.Location.X; }
        }
        public double Y
        {
            get { return _ball.Location.Y; }
        }
        public double Radius
        {
            get { return _ball.Radius; }
        }

        public BallModel(BallLogic ball)
        {
            _ball = ball;
        }
    }
}
