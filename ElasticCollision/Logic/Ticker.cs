using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticCollision.Logic
{
    /// __not__ thread safe, doesn't need to be
    public class Ticker
    {
        public delegate void CallMe();

        private CallMe callback { get; }
        private int cycleLength { get; }

        public bool running { get; private set; } = false;

        private Task loop;

        public Ticker(CallMe callback, int msDelay)
        {
            this.callback = callback;
            this.cycleLength = msDelay;
        }

        private void Spin()
        {
            while (running)
            {
                var timer = Stopwatch.StartNew();
                callback.Invoke();
                timer.Stop();
                int remaining = (cycleLength - (int)timer.ElapsedMilliseconds);
                if (remaining > 0)
                {
                    Thread.Sleep(remaining);
                }
            }
        }

        public void Start()
        {
            if (!running)
            {
                running = true;
                loop = Task.Run(Spin);
            }

        }
        public async void Stop()
        {
            if (running)
            {
                running = false;
                await loop;
            }
        }
    }
}
