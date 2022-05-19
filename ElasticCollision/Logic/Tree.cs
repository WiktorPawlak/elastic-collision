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
            public Area Area { get; }
            private NonBinaryTree _tree;
            public bool Exists { get { return _tree != null; } }
            public NonBinaryTree Subtree
            {
                get
                {
                    if (_tree == null) _tree = new NonBinaryTree(Area);
                    return _tree;
                }
            }
            public Child(Area area)
            {
                this.Area = area;
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
            if (ball.Within(A.Area)) { A.Subtree.Insert(ball); }
            else if (ball.Within(B.Area)) { B.Subtree.Insert(ball); }
            else { bals.Insert(ball); }
        }
        public List<Ball> Neighbors(Ball ball)
        {
            IEnumerable<Ball> tmp = bals.Neighbors(ball);
            if (ball.Intersects(A.Area) && A.Exists) tmp = tmp.Concat(A.Subtree.Neighbors(ball));
            if (ball.Intersects(B.Area) && B.Exists) tmp = tmp.Concat(B.Subtree.Neighbors(ball));
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
            public Interval Interval { get; }
            private BinaryTree _tree;
            private readonly Direction _dir;
            public bool Exists { get { return _tree != null; } }
            public BinaryTree Subtree
            {
                get
                {
                    if (_tree == null) _tree = new BinaryTree(_dir, Interval);
                    return _tree;
                }
            }
            public Child(Interval basis, Direction dir)
            {
                Interval = basis;
                _dir = dir;
            }
        }


        public List<Ball> Balls { get; }
        public Direction Dir { get; }
        public Child A;
        public Child B;
        public BinaryTree(Direction dir, Interval basis)
        {
            var (a, b) = basis.Split();
            A = new(a, dir);
            B = new(b, dir);
            this.Dir = dir;
            Balls = new List<Ball>();
        }

        public void Insert(Ball ball)
        {
            if (Fits(A.Interval, ball)) { A.Subtree.Insert(ball); }
            else if (Fits(B.Interval, ball)) { B.Subtree.Insert(ball); }
            else { Balls.Add(ball); }
        }

        public List<Ball> Neighbors(Ball ball)
        {
            IEnumerable<Ball> tmp = Balls;
            if (Intersects(A.Interval, ball) && A.Exists) tmp = tmp.Concat(A.Subtree.Neighbors(ball));
            if (Intersects(B.Interval, ball) && B.Exists) tmp = tmp.Concat(B.Subtree.Neighbors(ball));
            return new List<Ball>(tmp);
        }
        private double RelevantLocation(Vector loc)
        {
            return (Dir == Direction.Horizontal) ? loc.X : loc.Y;

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
