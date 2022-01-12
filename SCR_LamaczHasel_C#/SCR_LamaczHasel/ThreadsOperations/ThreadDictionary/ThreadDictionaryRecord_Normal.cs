using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public class ThreadDictionaryRecord_Normal : ThreadDictionaryRecord
    {
        public override int BreakAllPasswords(Func<string, string> ThreadModifyPwd)        
        {
            for (int i = 0; i < Program.Dictionary.Length; i++)
            {
                string pwd = ThreadModifyPwd(Program.Dictionary[i]);
                ChangeBreakedPassword(pwd);
                if (ValidateEnd())
                    return 0;
            }
            while (true)                                //nieskoncona petla - zeby watek sie nie zakonczyl i mogl byc Aborted by Main()
            {
                if (ValidateEnd())
                    return 0;
                Thread.Sleep(500);
            }
            return 1;
        }
    }
}
