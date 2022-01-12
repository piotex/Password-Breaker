using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using SCR_LamaczHasel.ThreadsOperations.ThreadDictionary;
using System;
using System.IO;

namespace SCR_LamaczHasel.ThreadsOperations
{
    public class ThreadDictionaryChecker : ThreadDictionaryRecord
    {
        private string _pathToBreaked = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\zlamane.txt";

        public ThreadDictionaryChecker()
        {
            File.Create(_pathToBreaked).Close();
        }

        public int MarkBreakedPwds(string data)
        {
            while (true)
            {
                Program.eventBreakedPassword.WaitOne();
                MarkPwdInMemory();
                Program.eventModifiedFileData.Set();


                if (ValidateEnd())
                    return 0;
            }
        }
        private static int _laizyCounter = 1;
        protected void MarkPwdInMemory()
        {
            Console.WriteLine("#{2}# Crack the password: inx: {0}      {1}  ", Program.BreakedPassword.Index, Program.BreakedPassword.Pwd, _laizyCounter++);
            Program.Passwords[Program.BreakedPassword.Index].Breaked = true;          //Mark breaked password in memory
            using (StreamWriter sw = File.AppendText(_pathToBreaked))
            {
                sw.WriteLine(Program.BreakedPassword.Pwd);
            }
        }
        public override int BreakAllPasswords(PwdModify pwdModify)
        {
            throw new NotImplementedException();
        }
    }
}

//https://docs.microsoft.com/pl-pl/dotnet/api/system.io.file.appendtext?view=net-6.0
