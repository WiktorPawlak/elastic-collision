using System;
using System.Collections.Generic;
using System.Linq;
using ElasticCollision.Data;

/// allows for efficient checking of possibly overlapping balls
namespace ElasticCollision.Logic
{
    public enum Direction { Horizontal, Vertical };
    public class NonBinaryTree
    {
        public class Child
        {
            public Area area { get; }
            private NonBinaryTree _tree;
            public bool exists { get { return _tree != null; } }
            public NonBinaryTree subtree
            {
                get
                {
                    if (_tree == null) _tree = new NonBinaryTree(area);
                    return _tree;
                }
            }
            public Child(Area area)
            {
                this.area = area;
            }
        }


        // either left/right or top/bottom
        public Child A;
        public Child B;
        public BinaryTree bals;

        public NonBinaryTree(Area area)
        {
            Area a, b;
            if (area.Horizontal.length > area.Vertical.length)
            {
                // ________
                // [______]
                bals = new BinaryTree(Direction.Vertical, area.Vertical);
                (a, b) = area.SplitVertically();
            }
            else
            {
                // []
                bals = new BinaryTree(Direction.Horizontal, area.Horizontal);
                (a, b) = area.SplitHorizontally();

            }
            A = new Child(a);
            B = new Child(b);
        }
        public void Insert(Ball ball)
        {
            if (ball.Within(A.area)) { A.subtree.Insert(ball); }
            else if (ball.Within(B.area)) { B.subtree.Insert(ball); }
            else { bals.Insert(ball); }
        }
        public List<Ball> Neighbors(Ball ball)
        {
            IEnumerable<Ball> tmp = bals.Neighbors(ball);
            if (ball.Intersects(A.area) && A.exists) tmp = tmp.Concat(A.subtree.Neighbors(ball));
            if (ball.Intersects(B.area) && B.exists) tmp = tmp.Concat(B.subtree.Neighbors(ball));
            return new List<Ball>(tmp);
        }
        public static NonBinaryTree Create(Area area, IEnumerable<Ball> balls)
        {
            var tree = new NonBinaryTree(area);
            foreach (var ball in balls)
            {
                tree.Insert(ball);
            }
            return tree;
        }
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
            if (Fits(A.interval, ball)) { A.subtree.Insert(ball); }
            else if (Fits(B.interval, ball)) { B.subtree.Insert(ball); }
            else { balls.Add(ball); }
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
