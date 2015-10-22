using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mystify
{
    class MyTimer
    {
        public delegate void func(object e);
        public int delayAmount;
        public Thread thread;
        public func myFunc;
        public object sender;
        public bool paused = false;
        bool killThread = false;
        ManualResetEvent mrse = new ManualResetEvent(false);
        public MyTimer(int delay, func newFunc, object e)
        {
            delayAmount = delay;
            myFunc = newFunc;
            sender = e;
            start();
        }
        ~MyTimer()
        {
            myFunc = null;
        }
        public void delay()
        {
            Thread.Sleep(Usefull.speed);
        }
        public void start()
        {
            thread = new Thread(() => loop());
            thread.Start();
            //thread.Join();
            //myFunc = null;
        }
        public void loop()
        {
            for (; !killThread;)
            {
                if (paused)
                {
                    mrse.WaitOne();
                }
                myFunc(sender);
                delay();
            }
        }
        public void pause()
        {
            paused = true;
            mrse.Reset();
        }
        public void resume()
        {
            paused = false;
            mrse.Set();
        }
        public void stop()
        {
            killThread = true;
        }
    }
}
