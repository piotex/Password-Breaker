using SCR_LamaczHasel.Pwd;
using SCR_LamaczHasel.ThreadsOperations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel
{
    public class MainUtils
    {
        public const int ThreadCount = 3;

        private const string _path_dic = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\slownik.txt";
        private const string _path_pwd = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\hasla.txt";

        public static void _init_threadList(ref Thread[] threadList)
        {
            threadList[0] = new Thread(() => new ThreadDictionaryChecker().MarkBreakedPwds(""));        //wywolanie pojedynczego watku - pojedynczej funkcji - jak sie wykona do konca, watek sie konczy
            threadList[1] = new Thread(() => new ThreadDictionaryRecordBasic().DoSth(""));
            threadList[2] = new Thread(() => new ThreadDictionaryRecordFirstUpper().DoSth(""));


            foreach (Thread thread in threadList)
            {
                if (thread != null)
                    thread.Start();
            }
        }
        public static void _set_Pwd()
        {
            string[] tmp = File.ReadAllLines(_path_pwd);
            Program.Passwords = new PwdModel[tmp.Length];

            for (int i = 0; i < tmp.Length; i++)
                Program.Passwords[i] = new PwdModel() { Pwd = tmp[i], Breaked = false };
        }
        public static void _set_Dic(string input)
        {
            if (input == "-")
                Program.Dictionary = File.ReadAllLines(_path_dic);
            else
                Program.Dictionary = File.ReadAllLines(input);
        }
        public static void _abort_threadList(ref Thread[] threadList)
        {
            Program.TimeToDie = true;
            WaitHandle.SignalAndWait(Program.eventBreakedPassword, Program.eventModifiedFileData);          //wyslane w celu zatrzymania Checkera

            while (Interlocked.Read(ref Program.DiedThreads) < ThreadCount)
            {
                Console.WriteLine("abort -- wait");
                Thread.Sleep(500);
            }
            while (Interlocked.Read(ref Program.DiedThreads) > 0)
            {
                Interlocked.Decrement(ref Program.DiedThreads);
            }
            Program.TimeToDie = false;
            Console.WriteLine("abort -- finish -----------------------------------------------------------");
        }
    }
}
