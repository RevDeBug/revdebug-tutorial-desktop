using System;
using System.Text;
using System.Threading;

namespace Starter.Examples.MultiThread
{
    public class MultiThreadTest
    {
        static System.Diagnostics.Stopwatch stopwatch;

        private StringBuilder sb;
        public void Execute()
        {
            sb= new StringBuilder();

            System.AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            int threadCount = 30;
            for (int threadNo = 0; threadNo < threadCount; threadNo++)
            {
                var newThreadClass = new MyThread();
                newThreadClass.ThreadStateChanged += ThreadStateChanged;
                var newThread = new Thread(newThreadClass.SomeThread);
                newThread.Name = (threadNo + 1).ToString();
                newThread.Start();
            }
        }

        private void CurrentDomain_ProcessExit(object sender, System.EventArgs e)
        {
            stopwatch.Stop();
            System.Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds.ToString());
            sb.AppendLine(stopwatch.ElapsedMilliseconds.ToString());
        }

        public event EventHandler ThreadStateChanged;
    }
}
