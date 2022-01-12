using SCR_LamaczHasel.Pwd;
using SCR_LamaczHasel.ThreadsOperations;
using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using SCR_LamaczHasel.ThreadsOperations.ThreadDictionary;
using System;
using System.IO;
using System.Threading;

namespace SCR_LamaczHasel
{
    public class MainUtils
    {
        public const int ThreadCount = 10;

        private const string _path_dic = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\slownik.txt";
        private const string _path_pwd = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\hasla.txt";

        public static void _init_threadList(ref Thread[] threadList)
        {
            threadList[0] = new Thread(() => new ThreadDictionaryChecker().MarkBreakedPwds(""));        //wywolanie pojedynczego watku - pojedynczej funkcji - jak sie wykona do konca, watek sie konczy
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[1] = new Thread(() => new ThreadDictionaryRecord_Normal().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));
            threadList[2] = new Thread(() => new ThreadDictionaryRecord_Normal().BreakAllPasswords(new ThreadDictionaryRecord_FirstUpper()));
            threadList[3] = new Thread(() => new ThreadDictionaryRecord_Normal().BreakAllPasswords(new ThreadDictionaryRecord_AllUpper()));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[4] = new Thread(() => new ThreadDictionaryRecord_NormalNumber().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));
            threadList[5] = new Thread(() => new ThreadDictionaryRecord_NormalNumber().BreakAllPasswords(new ThreadDictionaryRecord_FirstUpper()));
            threadList[6] = new Thread(() => new ThreadDictionaryRecord_NormalNumber().BreakAllPasswords(new ThreadDictionaryRecord_AllUpper()));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[7] = new Thread(() => new ThreadDictionaryRecord_NumberNormal().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));
            threadList[8] = new Thread(() => new ThreadDictionaryRecord_NumberNormal().BreakAllPasswords(new ThreadDictionaryRecord_FirstUpper()));
            threadList[9] = new Thread(() => new ThreadDictionaryRecord_NumberNormal().BreakAllPasswords(new ThreadDictionaryRecord_AllUpper()));


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


        private static bool containsUppercaseCharOrNumber(string pwd)
        {
            for (int i = 0; i < pwd.Length; i++)
            {
                if (pwd[i] >= 65 && pwd[i] <= 90)   //upercase char
                    return true;
                if (pwd[i] >= 48 && pwd[i] <= 57)   //number
                    return true;
            }
            return false;
        }
        public static void UpdateDictionaty()
        {
            Program.Dictionary = File.ReadAllLines(_path_dic);
            File.Create(_path_dic).Close();
            for (int i = 0; i < Program.Dictionary.Length; i++)
            {
                string pwd = Program.Dictionary[i];
                if (!containsUppercaseCharOrNumber(pwd))
                {
                    using (StreamWriter sw = File.AppendText(_path_dic))
                    {
                        sw.WriteLine(pwd);
                    }
                }
            }
        }
    }
}
