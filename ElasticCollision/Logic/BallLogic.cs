using ElasticCollision.Data;

namespace ElasticCollision.Logic
{
    public class BallLogic
    {
        private Ball _ball;

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

        public BallLogic(Ball ball)
        {
            _ball = ball;
        }
    }
}
