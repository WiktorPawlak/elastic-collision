using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticCollision.Data
{
    /// __not__ thread safe, doesn't need to be
    public class Ticker
    {
        public delegate void CallMe();

        private CallMe Callback { get; }
        private int CycleLength { get; }

        public bool Running { get; private set; } = false;

        private Task loop;

        public Ticker(CallMe callback, int msDelay)
        {
            Callback = callback;
            CycleLength = msDelay;
        }

        private void Spin()
        {
            while (Running)
            {
                var timer = Stopwatch.StartNew();
                Callback.Invoke();
                timer.Stop();
                int remaining = (CycleLength - (int)timer.ElapsedMilliseconds);
                if (remaining > 0)
                {
                    Thread.Sleep(remaining);
                }
            }
        }

        public void Start()
        {
            if (!Running)
            {
                Running = true;
                loop = Task.Run(Spin);
            }

        }

        public async void Stop()
        {
            if (Running)
            {
                Running = false;
                await loop;
            }
        }
    }
}
