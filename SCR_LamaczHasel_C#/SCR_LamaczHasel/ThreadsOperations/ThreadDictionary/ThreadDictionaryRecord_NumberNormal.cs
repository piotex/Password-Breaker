using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public class ThreadDictionaryRecord_NumberNormal : ThreadDictionaryRecord
    {
        public override int BreakAllPasswords(PwdModify pwdModify)                     //Func<string, string> ThreadModifyPwd  ->  przekazuje tutaj funkcje modyfikujaca stringa jako parametr - delegaty
        {
            for (int i = 0; i < Program.Dictionary.Length; i++)
            {
                for (int j = 0; j < 100000; j++)
                {
                    string pwd = pwdModify.ThreadModifyPwd(String.Concat(j,Program.Dictionary[i]));                              //32pwd
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
                    pwd = pwdModify.ThreadModifyPwd(String.Concat(j.ToString("D" + 5),Program.Dictionary[i]));                   //00032pwd
                    index = GetPwdIndexInDb(pwd);
                    if (index != -1)
                    {
                        lock (Program._pwdChanging_locker)
                        {
                            Program.BreakedPassword.Index = index;
                            Program.BreakedPassword.Pwd = pwd;
                            WaitHandle.SignalAndWait(Program.eventBreakedPassword, Program.eventModifiedFileData);
                        }
                    }
                    if (ValidateEnd())
                        return 0;
                }
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
