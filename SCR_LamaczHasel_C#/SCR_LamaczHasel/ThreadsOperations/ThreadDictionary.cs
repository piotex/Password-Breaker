using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations
{
    public abstract class ThreadDictionary
    {
        protected abstract string GetThreadName();
        protected bool ValidateEnd()
        {
            if (Program.TimeToDie)
            {
                Interlocked.Increment(ref Program.DiedThreads);
                Console.WriteLine("-> END --- " + GetThreadName());
                return true;
            }
            return false;
        }
        protected void SayThreadHello()
        {
            Console.WriteLine("-> START --- " + GetThreadName());
        }


    }
}
