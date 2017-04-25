using System;
using System.Threading;

namespace Starter.Examples.MultiThread
{
    class MyThread
    {
        private int _number;
        private Random _random;
        private static int _sharedNumber;

        public void SomeThread()
        {
            _random = new Random();
            var charHeight = 0;
            int threadID = Int32.Parse(Thread.CurrentThread.Name);
            var max = 1000 + 20 * threadID;
            OnThreadStarted(new ThreadEventArgs { Message = $"Thread {Thread.CurrentThread.Name} will process loop {max} times." });
            while (_number < max)
            {
                ++_number;
                --_sharedNumber;
                if ((threadID % 3) == 0 &&
                    (_sharedNumber % 20) == 0)
                    charHeight = this.getCharHeight();
                var text = string.Format("Thread {0}: {1}", Thread.CurrentThread.Name, _number);
                var testFormat = _sharedNumber * 2;
            }
            OnThreadStarted(new ThreadEventArgs { Message = $"Thread {Thread.CurrentThread.Name} has ended." });
        }

        private int getCharHeight()
        {
            // just a tribute to MS Word ;)
            return 8;
        }


        public event EventHandler ThreadStateChanged;

        protected virtual void OnThreadStarted(ThreadEventArgs e)
        {
            ThreadStateChanged?.Invoke(this, e);
        }
    }
}
