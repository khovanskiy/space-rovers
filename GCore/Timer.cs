using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public class Timer : EventDispatcher, IDisposable
    {
        System.Timers.Timer timer;
        public Timer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += handler;
        }
        private void handler(object o, System.Timers.ElapsedEventArgs e)
        {
            dispatchEvent(new TimerEvent(this, TimerEvent.TIMER));
        }
        public double interval
        {
            get
            {
                return timer.Interval;
            }
            set
            {
                timer.Interval = value;
            }
        }
        public void start()
        {
            timer.Start();
        }
        public void stop()
        {
            timer.Stop();
        }
        ~Timer()
        {
            Dispose();
        }
        public void Dispose()
        {
            timer.Elapsed -= handler;
            timer.Dispose();
        }
    }
}
