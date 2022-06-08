using System.Diagnostics;
using System.Threading;

namespace ElasticCollision.Data
{
    public class Ticker
    {
        public delegate void CallMe();

        private CallMe Callback { get; }
        private int CycleLength { get; }

        public bool Running { get; private set; } = false;

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
                new Thread(Spin).Start();
            }
        }
    }
}
