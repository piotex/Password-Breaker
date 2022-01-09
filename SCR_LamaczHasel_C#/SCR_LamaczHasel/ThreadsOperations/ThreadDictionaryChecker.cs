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
            SayThreadHello();
            while (true)
            {
                Program.eventBreakedPassword.WaitOne();
                Console.WriteLine("# Crack the password:         {0} ", Program.BreakedPassword.Pwd);
                Program.eventModifiedFileData.Set();


                if (ValidateEnd())
                    return 0;
            }
        }

        protected override string GetThreadName()
        {
            return "ThreadDictionaryChecker";
        }
    }
}
