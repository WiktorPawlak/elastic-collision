using System;
using System.Threading;
using ElasticCollision.Data;
using ElasticCollision.Logic;
using Xunit;
namespace LogicTest
{
    public class TreeTest
    {
        [Fact]
        public void TestInitialization()
        {
            var tree = new BinaryTree(Direction.Horizontal, new(0, 100));
            Assert.False(tree.A.Exists);
            Assert.False(tree.B.Exists);
            tree.Insert(new Ball(10, 10, new(45, 0), new(0, 0)));
            Assert.False(tree.A.Exists);
            Assert.False(tree.B.Exists);
            Assert.Single(tree.Balls);
            tree.Insert(new Ball(10, 10, new(20, 0), new(0, 0)));
            Assert.Single(tree.A.Subtree.Balls);
            tree.Insert(new Ball(10, 10, new(10, 0), new(0, 0)));
            Assert.Single(tree.A.Subtree.Balls);
        }
        [Fact]
        public void TestNeighbors()
        {
            var tree = new BinaryTree(Direction.Horizontal, new(0, 100));
            Ball a = new Ball(10, 10, new(45, 0), new(0, 0));
            Ball b = new Ball(10, 10, new(40, 0), new(0, 0));
            Ball c = new Ball(1, 1, new(5, 0), new(0, 0));
            tree.Insert(a);
            Assert.Single(tree.Neighbors(a));
            tree.Insert(b);
            Assert.Equal(2, tree.Neighbors(a).Count);
            tree.Insert(c);
            Assert.Equal(2, tree.Neighbors(a).Count);

        }

    }
}
