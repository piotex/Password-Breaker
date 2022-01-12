using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.PwdModif
{
    public class ThreadDictionaryRecord_Basic : PwdModify
    {
        public override string GetThreadName()
        {
            return "ThreadDictionaryRecord_Basic";
        }
        public override string ThreadModifyPwd(string pwd)
        {
            return pwd;
        }
    }
}
