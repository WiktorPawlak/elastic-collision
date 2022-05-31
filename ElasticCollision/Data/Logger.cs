using System.Collections.Concurrent;
using System.Threading.Tasks;
namespace ElasticCollision.Data
{
    public interface ILoggable
    {
        string entry();
    }
    public class Logger<T> where T : ILoggable
    {
        BlockingCollection<T> fifo;

        public void consume(T i)
        {
            i.entry();
        }

        public void endlessLoop()
        {
            foreach (T i in fifo.GetConsumingEnumerable())
                consume(i);
        }

        public Logger()
        {
            fifo = new BlockingCollection<T>();
            Task.Run(endlessLoop);
        }
    }
}
