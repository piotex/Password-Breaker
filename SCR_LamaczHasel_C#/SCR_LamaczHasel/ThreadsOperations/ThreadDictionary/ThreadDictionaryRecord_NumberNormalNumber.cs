using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public class ThreadDictionaryRecord_NumberNormalNumber : ThreadDictionaryRecord
    {
        public override int BreakAllPasswords(PwdModify pwdModify)                     //Func<string, string> ThreadModifyPwd  ->  przekazuje tutaj funkcje modyfikujaca stringa jako parametr - delegaty
        {
            for (int i = 0; i < Program.Dictionary.Length; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    for (int k = 0; k < 1000; k++)
                    {
                        string pwd = pwdModify.ThreadModifyPwd(Program.Dictionary[i]);                              //pwd32
                        pwd = String.Concat(pwd, j);
                        pwd = String.Concat(k, pwd);
                        ChangeBreakedPassword(pwd);

                        pwd = pwdModify.ThreadModifyPwd(Program.Dictionary[i]);                              //pwd00032
                        pwd = String.Concat(pwd, j.ToString("D" + 5));
                        pwd = String.Concat(k.ToString("D" + 5), pwd);
                        ChangeBreakedPassword(pwd);

                        if (ValidateEnd())
                            return 0;
                    }
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
