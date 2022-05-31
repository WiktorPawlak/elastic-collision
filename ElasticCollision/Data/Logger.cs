using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.IO;
namespace ElasticCollision.Data
{
    public class Logger
    {
        BlockingCollection<string> fifo;
        StreamWriter fs;

        private void endlessLoop()
        {
            foreach (string i in fifo.GetConsumingEnumerable())
                fs.WriteLine(i);
        }

        public Logger(string filename)
        {
            fifo = new BlockingCollection<string>();
            fs = new(filename);
            Task.Run(endlessLoop);
        }

        public void log(string t) => fifo.Add(t);
    }
}
