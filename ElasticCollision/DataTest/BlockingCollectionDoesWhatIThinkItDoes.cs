using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using ElasticCollision.Data;
using Xunit;
using static ElasticCollision.Data.Vector;

namespace DataTest
{
    public class BlockingCollectionDoesWhatIthinkItDoes
    {

        public class helper
        {
            public List<int> lst = new List<int>();
            public void consume(int i) => lst.Add(i);
            public void accept(IEnumerable<int> data)
            {
                foreach (int i in data)
                    consume(i);
            }

            public void attach(BlockingCollection<int> queue)
            {
                Task.Run(() => accept(queue.GetConsumingEnumerable()));
            }

            [Fact]
            public void TestA()
            {
                var coll = new BlockingCollection<int>();
                var IntEater = new helper();
                IntEater.attach(coll);

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
