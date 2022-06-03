namespace ElasticCollision.Data
{
    public record Ball(
         double Radius,
         double Mass,
         Vector Location,
         Vector Velocity
    )
    {
        public override string ToString()
        {
            return "loc: " + Location.ToString() +
              " vel: " + Velocity.ToString();
        }
        public Ball Budge(double Δt) => this with { Location = Location + (Velocity * Δt) };

        public Vector Momentum { get { return Velocity * Mass; } }

        public Ball ApplyImpulse(Vector Momentum) => this with { Velocity = Velocity + Momentum * (1 / Mass) };
    }

    public record BallWithJunk(
         double Radius,
         double Mass,
         Vector Location,
         Vector Velocity,
         int id,
         PushMe cb,
         RequestUpdate info
    ) : Ball(Radius, Mass, Location, Velocity)
    {
        public static BallWithJunk addJunk(Ball ball, int id, PushMe cb, RequestUpdate info)
        {
            return new BallWithJunk(
                ball.Radius, ball.Mass, ball.Location, ball.Velocity, id, cb, info
            );
        }
    }

    public delegate void PushMe(Vector impulse);
    public delegate void UpdateBall(BallWithJunk b);
    public delegate BallWithJunk RequestUpdate();

    public class MobileBall
    {
        private readonly Ticker _ticker;
        private Ball _ball;
        private readonly int _id;
        private readonly UpdateBall CheckCollision;
        private object onlyone = new object();
        private Logger _log;

        public MobileBall(Ball ball, int id, UpdateBall onBallMoved, Logger log)
        {
            _ticker = new Ticker(Proceed, 5);
            _ball = ball;
            _id = id;
            _log = log;
            CheckCollision = onBallMoved;
            _ticker.Start();
        }

        public void Proceed()
        {
            lock (onlyone)
                _ball = _ball.Budge(0.03);

            CheckCollision.Invoke(update());
            _log.Log(ToString());
        }
        public override string ToString() { return "ID: " + _id + " " + _ball.ToString(); }

        public BallWithJunk update() => BallWithJunk.addJunk(_ball, _id, Poke, update);

        public void Poke(Vector momentum)
        {
            lock (onlyone)
                _ball = _ball.ApplyImpulse(momentum);
        }
    }
}
