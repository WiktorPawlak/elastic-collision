using System;
using Xunit;

namespace DataTest
{
    public class VectorTest
    {
        [Fact]
        public void TestCreation()
        {
            Vector v1 = new Vector(0, 0);
            Assert.Equal(0, v1.x);
            Assert.Equal(0, v1.y);
            Vector v2 = new Vector(5, -9);
            Assert.Equal(5, v2.x);
            Assert.Equal(-9, v2.y);
        }
    }
}
