using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.PwdModif
{
    public class ThreadDictionaryRecord_FirstUpper : PwdModify
    {
        //public override string GetThreadName()
        //{
        //    return "ThreadDictionaryRecord_FirstUpper";
        //}
        public override string ThreadModifyPwd(string pwd)
        {
            if (pwd == null)
                return null;
            if (pwd.Length > 1)
                return char.ToUpper(pwd[0]) + pwd.Substring(1);
            return pwd.ToUpper();
        }
    }
}
