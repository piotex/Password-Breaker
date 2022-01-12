using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public abstract class ThreadDictionaryRecord 
    {
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
    }
}
