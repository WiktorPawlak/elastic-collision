using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticCollision.Data
{
    public record Ball(
         double Radius,
         double Mass,
         Vector Location,
         Vector Velocity
    )
    {

        public Ball Budge(double Δt) => this with { Location = Location + (Velocity * Δt) };

        public Vector Momentum { get { return Velocity * Mass; } }

        public Ball ApplyImpulse(Vector Momentum) => this with { Velocity = Velocity + Momentum * (1 / Mass) };

        public double KineticEnergy
        {
            get
            {
                double speed = Velocity.Magnitude;
                return speed * speed * Mass * 0.5;
            }
        }
    }

    public class MobileBall
    {
        private readonly Ticker _ticker;
        private Ball _ball;
        private readonly int _index;

        public delegate Vector CheckCollisionDelegate(Ball b, int index);
        private CheckCollisionDelegate CheckCollision { get; }

        public MobileBall(Ball ball, int index, CheckCollisionDelegate onBallMoved)
        {
            _ticker = new Ticker(Proceed, 5);
            _ball = ball;
            _index = index;
            CheckCollision = onBallMoved;
            _ticker.Start();
        }

        public void Proceed()
        {
            _ball = _ball.Budge(0.03);
            Poke(CheckCollision.Invoke(_ball, _index));
        }

        public void Poke(Vector momentum)
        {
            _ball = _ball.ApplyImpulse(momentum);
        }
    }
}
