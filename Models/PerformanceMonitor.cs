using System.Diagnostics;

namespace Models
{
    public class PerformanceMonitor
    {
        private Stopwatch stopWatch;

        public PerformanceMonitor()
        {
            stopWatch = new Stopwatch();
        }

        public void Start()
        {
            stopWatch.Start();
        }

        public void Stop()
        {
            stopWatch.Stop();
            Console.WriteLine($"Seconds: {stopWatch.Elapsed.Seconds}\nMilliseconds: {stopWatch.ElapsedMilliseconds}");
        }
    }
}
