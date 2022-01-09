using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations
{
    public class ThreadDictionaryChecker : ThreadDictionary
    {
        public int MarkBreakedPwds(string data)
        {
            bool notDone = true;
            Console.WriteLine("start -- MarkBreakedPwds -- ");
            while (notDone)
            {
                Program.ewh.WaitOne();
                Console.WriteLine("# Crack the password:         {0} ", Program.BreakedPassword);
                Program.clearCount.Set();


                if (Program.TimeToDie)
                {
                    Interlocked.Increment(ref Program.DiedThreads);
                    Console.WriteLine("-> END --- ThreadDictionaryChecker ");
                    return 0;
                }
            }
            return 1;
        }
    }
}
