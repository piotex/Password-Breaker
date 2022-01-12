using SCR_LamaczHasel.Pwd;
using SCR_LamaczHasel.ThreadsOperations;
using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using SCR_LamaczHasel.ThreadsOperations.ThreadDictionary;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel
{
    public class MainUtils
    {
        public const int ThreadCount = 23;

        private const string _path_dic = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\slownik.txt";
        private const string _path_pwd = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\hasla.txt";
        private const string _path_pwdMD5 = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\haslaMD5.txt";

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
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[10] = new Thread(() => new ThreadDictionaryRecord_NumberNormalNumber().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));
            threadList[11] = new Thread(() => new ThreadDictionaryRecord_NumberNormalNumber().BreakAllPasswords(new ThreadDictionaryRecord_FirstUpper()));
            threadList[12] = new Thread(() => new ThreadDictionaryRecord_NumberNormalNumber().BreakAllPasswords(new ThreadDictionaryRecord_AllUpper()));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[13] = new Thread(() => new ThreadDictionaryRecord_NormalChar().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));
            threadList[14] = new Thread(() => new ThreadDictionaryRecord_NormalChar().BreakAllPasswords(new ThreadDictionaryRecord_FirstUpper()));
            threadList[15] = new Thread(() => new ThreadDictionaryRecord_NormalChar().BreakAllPasswords(new ThreadDictionaryRecord_AllUpper()));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[16] = new Thread(() => new ThreadDictionaryRecord_CharNormal().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));
            threadList[17] = new Thread(() => new ThreadDictionaryRecord_CharNormal().BreakAllPasswords(new ThreadDictionaryRecord_FirstUpper()));
            threadList[18] = new Thread(() => new ThreadDictionaryRecord_CharNormal().BreakAllPasswords(new ThreadDictionaryRecord_AllUpper()));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[19] = new Thread(() => new ThreadDictionaryRecord_CharNormalChar().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));
            threadList[20] = new Thread(() => new ThreadDictionaryRecord_CharNormalChar().BreakAllPasswords(new ThreadDictionaryRecord_FirstUpper()));
            threadList[21] = new Thread(() => new ThreadDictionaryRecord_CharNormalChar().BreakAllPasswords(new ThreadDictionaryRecord_AllUpper()));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            threadList[22] = new Thread(() => new ThreadDictionaryRecord_NormalALL().BreakAllPasswords(new ThreadDictionaryRecord_Basic()));


            foreach (Thread thread in threadList)
            {
                if (thread != null)
                {
                    thread.Start();
                    Thread.Sleep(10);                                   //small delay added to make sure that each thread will operate on different pwd in the same time
                }
            }
        }
        public static void _set_Pwd()
        {
            //string[] tmp = File.ReadAllLines(_path_pwd);
            string[] tmp = File.ReadAllLines(_path_pwdMD5);
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
        public static void MakeHashMD5PwdFile()
        {
            var tmp = File.ReadAllLines(_path_pwd);
            File.Create(_path_pwdMD5).Close();
            for (int i = 0; i < tmp.Length; i++)
            {
                string pwd = tmp[i];
                pwd = CreateMD5(pwd);
                using (StreamWriter sw = File.AppendText(_path_pwdMD5))
                {
                    sw.WriteLine(pwd);
                }
            }
        }
        public static string CreateMD5(string pwd)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pwd);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));             //hexadecimat format
                }
                return sb.ToString();
            }
        }
    }
}
