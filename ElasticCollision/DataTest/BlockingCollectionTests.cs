using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DataTest
{
    public class BlockingCollectionTests
    {

        public class TestHelper
        {
            public List<int> lst = new List<int>();
            public void Consume(int i) => lst.Add(i);
            public void Accept(IEnumerable<int> data)
            {
                foreach (int i in data)
                    Consume(i);
            }

            public void Attach(BlockingCollection<int> queue)
            {
                Task.Run(() => Accept(queue.GetConsumingEnumerable()));
            }

            [Fact]
            public void TestA()
            {
                var coll = new BlockingCollection<int>();
                var IntEater = new TestHelper();
                IntEater.Attach(coll);

                void test(int i)
                {
                    Thread.Sleep(10);
                    Assert.Equal(i, IntEater.lst.Count);
                }

                test(0);
                coll.Add(5);
                test(1);
                coll.Add(5);
                coll.Add(6);
                coll.Add(7);
                test(4);
                for (int i = 0; i < 100; i++)
                {
                    Task.Run(() => coll.Add(0));
                }
                test(104);
            }
        }
    }
}
