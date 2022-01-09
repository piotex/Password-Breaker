﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations
{
    public class ThreadDictionaryRecordBasic : ThreadDictionary
    {
        public int DoSth(string data)
        {
            SayThreadHello();
            Thread.Sleep(5000);

            //lock
            Program.BreakedPassword.Pwd = "todo";
            WaitHandle.SignalAndWait(Program.eventBreakedPassword, Program.eventModifiedFileData);
            //free lock

            for (int i = 0; true; i++)
            {
                Console.WriteLine("w " + i);
                Thread.Sleep(500);

                if (ValidateEnd())
                    return 0;
            }

            return 1;
        }

        protected override string GetThreadName()
        {
            return "ThreadDictionaryRecordBasic";
        }
    }
}