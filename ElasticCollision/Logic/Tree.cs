using System;
using System.Collections.Generic;
using System.Linq;
using ElasticCollision.Data;
using ExtensionMethods;

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
                bals = new BinaryTree(new VerticalInterval(area.Vertical.low, area.Vertical.high));
                (a, b) = area.SplitVertically();
            }
            else
            {
                // []
                bals = new BinaryTree(new HorizontalInterval(area.Horizontal.low, area.Horizontal.high));
                (a, b) = area.SplitHorizontally();

            }
            A = new Child(a);
            B = new Child(b);
        }
        public void Insert(Ball ball)
        {
            if (A.Area.FullyContains(ball)) { A.Subtree.Insert(ball); }
            else if (B.Area.FullyContains(ball)) { B.Subtree.Insert(ball); }
            else { bals.Insert(ball); }
        }
        public List<Ball> Neighbors(Ball ball)
        {
            IEnumerable<Ball> tmp = bals.Neighbors(ball);
            if (A.Area.Intersects(ball) && A.Exists) tmp = tmp.Concat(A.Subtree.Neighbors(ball));
            if (B.Area.Intersects(ball) && B.Exists) tmp = tmp.Concat(B.Subtree.Neighbors(ball));
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

    public interface BallContainer
    {
        public void Insert(Ball b);
        public List<Ball> Neighbors(Ball b);
    }

    public abstract class Tree : BallContainer
    {
        public BallContainer Container { get; protected set; }
        public Tree A { get; private set; }
        public Tree B { get; private set; }
        public Section Basis { get; private set; }
        public bool Initialized { get; private set; } = false;


        public Tree(Section basis)
        {
            Basis = basis;
        }

        private void MakeChildren()
        {
            if (!Initialized)
            {
                var (a, b) = Basis.SplitSection();
                A = Create(a);
                B = Create(b);
                Initialized = true;
            }
        }
        protected abstract Tree Create(Section s);

        public void Insert(Ball ball)
        {
            MakeChildren();
            if (A.Basis.FullyContains(ball)) { A.Insert(ball); }
            else if (B.Basis.FullyContains(ball)) { B.Insert(ball); }
            else { Container.Insert(ball); }
        }

        public List<Ball> Neighbors(Ball ball)
        {
            if (!Basis.Intersects(ball)) { return new List<Ball>(); }
            else if (Initialized)
            {
                return Container.Neighbors(ball)
                    .Concat(A.Neighbors(ball))
                    .Concat(B.Neighbors(ball))
                    .ToList();
            }
            else { return Container.Neighbors(ball); }
        }
    }
    public class BallList : BallContainer
    {
        private List<Ball> _lst;
        public BallList() => _lst = new List<Ball>();
        public void Insert(Ball b) => _lst.Add(b);
        public List<Ball> Neighbors(Ball b) => _lst;
    }

    public class BinaryTree : Tree
    {
        public BinaryTree(Section s) : base(s) => Container = new BallList();
        protected override Tree Create(Section s) => new BinaryTree(s);
    }
}
