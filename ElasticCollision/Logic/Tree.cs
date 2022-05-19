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
        public class Child
        {
            public Interval interval { get; }
            private BinaryTree _tree;
            private Direction _dir;
            public bool exists { get { return _tree != null; } }
            public BinaryTree subtree
            {
                get
                {
                    if (_tree == null) _tree = new BinaryTree(_dir, interval);
                    return _tree;
                }
            }
            public Child(Interval basis, Direction dir)
            {
                interval = basis;
                _dir = dir;
            }
        }


        public List<Ball> balls { get; }
        public Direction dir { get; }
        public Child A;
        public Child B;
        public BinaryTree(Direction dir, Interval basis)
        {
            var (a, b) = basis.Split();
            A = new(a, dir);
            B = new(b, dir);
            this.dir = dir;
            balls = new List<Ball>();
        }

        public void Insert(Ball ball)
        {
            if (Fits(A.interval, ball))
            {
                A.subtree.Insert(ball);
            }
            else if (Fits(B.interval, ball))
            {
                B.subtree.Insert(ball);
            }
            else
            {
                balls.Add(ball);
            }
        }

        public List<Ball> Neighbors(Ball ball)
        {
            IEnumerable<Ball> tmp = balls;
            if (Intersects(A.interval, ball) && A.exists) tmp = tmp.Concat(A.subtree.Neighbors(ball));
            if (Intersects(B.interval, ball) && B.exists) tmp = tmp.Concat(B.subtree.Neighbors(ball));
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
