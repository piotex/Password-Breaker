using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations
{
    public class ThreadDictionaryRecordBasic : ThreadDictionary
    {
        public int DoSth(string data)
        {
            Console.WriteLine("start -- ThreadDictionaryRecordBasic -- ");
            Thread.Sleep(5000);

            Console.WriteLine("wys sygn " );
            WaitHandle.SignalAndWait(Program.ewh, Program.clearCount);
            Console.WriteLine("zwalniam lock ");

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("w " + i);
                Thread.Sleep(500);

                if (Program.TimeToDie)
                {
                    Interlocked.Increment(ref Program.DiedThreads);
                    Console.WriteLine("-> END --- ThreadDictionaryRecordBasic ");
                    return 0;
                }
            }

            return 1;
        }
    }
}
