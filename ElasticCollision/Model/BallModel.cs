using ElasticCollision.Logic;

namespace ElasticCollision.Presentation
{
    public class BallModel
    {
        private BallLogic _ball;
        public double X
        {
            get { return _ball.X; }
        }
        public double Y
        {
            get { return _ball.Y; }
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
