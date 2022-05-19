using System;
using System.Collections.Generic;
using System.Linq;
using ElasticCollision.Data;

/// allows for efficient checking of possibly overlapping balls
namespace ElasticCollision.Logic
{
    public enum Direction { Horizontal, Vertical };
    public abstract class NonBinaryTree
    {
        // either left/right or top/bottom
        private Area A;
        private Area B;
    }
    public abstract class VerticalBranch : NonBinaryTree
    {

    }
    public abstract class HorizontalBranch : NonBinaryTree
    {

    }



    public class BinaryTree
    {
        public List<Ball> balls { get; }
        public Direction dir { get; }
        public Interval A { get; }
        public Interval B { get; }
        public BinaryTree childA { get; private set; }
        public BinaryTree childB { get; private set; }

        public BinaryTree(Direction dir, Interval basis)
        {
            (A, B) = basis.Split();
            this.dir = dir;
            balls = new List<Ball>();
        }

        public void Insert(Ball ball)
        {
            if (Fits(A, ball))
            {
                if (childA == null) childA = new BinaryTree(dir, A);
                childA.Insert(ball);
            }
            else if (Fits(B, ball))
            {
                if (childB == null) childB = new BinaryTree(dir, B);
                childB.Insert(ball);
            }
            else
            {
                balls.Add(ball);
            }
        }

        public List<Ball> Neighbors(Ball ball)
        {
            IEnumerable<Ball> tmp = balls;
            if (Intersects(A, ball) && childA != null) tmp = tmp.Concat(childA.Neighbors(ball));
            if (Intersects(B, ball) && childB != null) tmp = tmp.Concat(childB.Neighbors(ball));
            return new List<Ball>(tmp);
        }
        private double RelevantLocation(Vector loc)
        {
            return (dir == Direction.Horizontal) ? loc.X : loc.Y;

        }
        private bool Fits(Interval interval, Ball ball)
        {
            return interval.Shrink(ball.Radius).contains(RelevantLocation(ball.Location));
        }
        private bool Intersects(Interval interval, Ball ball)
        {
            return interval.Shrink(-2 * ball.Radius).contains(RelevantLocation(ball.Location));
        }
    }
}
