using System;
using System.Collections.Generic;
using System.Text;

namespace SCR_LamaczHasel.ThreadsOperations.PwdModif
{
    public abstract class PwdModify
    {
        public abstract string ThreadModifyPwd(string pwd);
        public abstract string GetThreadName();
        public virtual void SayHello()
        {
            Console.WriteLine("-> START --- " + GetThreadName());
        }
        public virtual void SayBy()
        {
            Console.WriteLine("-> End --- " + GetThreadName());
        }
    }
}
