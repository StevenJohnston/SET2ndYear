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
        //int delay;
        public delegate void func(object e);
        public FuncTimer funcTimer;
        public MyTimer(int delay, func newFunc, object e)
        {
            funcTimer = new FuncTimer(delay,newFunc,e);
            funcTimer.start();
        }
    }
    class FuncTimer
    {
        //public delegate bool func(object e);
        public int delayAmount;
        public Thread thread;
        public Mystify.MyTimer.func myFunc;
        public object sender;
        public void delay()
        {
            Thread.Sleep(delayAmount);
        }
        public FuncTimer(int delay,MyTimer.func newfunc,object e)
        {
            delayAmount = delay;
            myFunc = newfunc;
            sender = e;
        }
        public void start()
        {
            thread = new Thread(() => loop());
            thread.Start();
        }
        public void loop()
        {
            for (;;)
            {
                myFunc(sender);
                delay();
            }
        }
    }
}
