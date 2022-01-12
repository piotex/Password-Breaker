using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public abstract class ThreadDictionaryRecord 
    {
        protected char[] _charsList = new char[34];
        protected virtual void addCharsToList()
        {
            int cc = 1;
            for (int i = 33; i <= 47; i++)          //without SPACE
            {
                _charsList[cc] = (char)i;
                cc++;
            }
            for (int i = 58; i <= 64; i++)
            {
                _charsList[cc] = (char)i;
                cc++;
            }
            for (int i = 91; i <= 96; i++)
            {
                _charsList[cc] = (char)i;
                cc++;
            }
            for (int i = 123; i <= 126; i++)
            {
                _charsList[cc] = (char)i;
                cc++;
            }
        }
        public ThreadDictionaryRecord()
        {
            addCharsToList();
        }
        public abstract int BreakAllPasswords(PwdModify pwdModify);
        protected virtual int GetPwdIndexInDb(string pwd)
        {
            for (int i = 0; i < Program.Passwords.Length; i++)
            {
                if (pwd.Equals(Program.Passwords[i].Pwd) && !Program.Passwords[i].Breaked)
                    return i;
            }
            return -1;
        }
        protected bool ValidateEnd()
        {
            if (Program.TimeToDie)
            {
                Interlocked.Increment(ref Program.DiedThreads);
                return true;
            }
            return false;
        }
        public virtual void ChangeBreakedPassword(string pwd)
        {
            int index = GetPwdIndexInDb(pwd);

            if (index != -1)
            {
                lock (Program._pwdChanging_locker)
                {
                    Program.BreakedPassword.Index = index;
                    Program.BreakedPassword.Pwd = pwd;
                    WaitHandle.SignalAndWait(Program.eventBreakedPassword, Program.eventModifiedFileData);
                }
            }
        }
    }
}
