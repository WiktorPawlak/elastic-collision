using System.Collections.Concurrent;
using System.Threading.Tasks;
using System;
using System.IO;
namespace ElasticCollision.Data
{
    public class Logger : IDisposable
    {
        BlockingCollection<string> _fifo;
        StreamWriter _sw;

        private void EndlessLoop()
        {
            try
            {
                foreach (string entry in _fifo.GetConsumingEnumerable())
                    _sw.WriteLine(entry);
            }
            finally
            {
                Dispose();
            }
        }

        public Logger(string filename)
        {
            _fifo = new BlockingCollection<string>();
            _sw = new(filename);
            Task.Run(EndlessLoop);
        }

        public void Log(string t) => _fifo.Add(DateTime.Now.ToString("HH:mm:ss ") + t);

        public void Dispose()
        {
            _sw.Dispose();
            _fifo.Dispose();
        }
    }
}
