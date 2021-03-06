using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public class ThreadDictionaryRecord_NumberNormal : ThreadDictionaryRecord
    {
        public override int BreakAllPasswords(Func<string, string> ThreadModifyPwd)                     //Func<string, string> ThreadModifyPwd  ->  przekazuje tutaj funkcje modyfikujaca stringa jako parametr - delegaty
        {
            int sizeOfTesting = 100000;                             //can be changed to ulong -> 18 446 744 073 709 551 615
            for (int i = 0; i < Program.Dictionary.Length; i++)
            {
                for (int j = 0; j < sizeOfTesting; j++)
                {
                    string pwd = ThreadModifyPwd(Program.Dictionary[i]);                              //32pwd
                    pwd = String.Concat(j, pwd);
                    ChangeBreakedPassword(pwd);

                    pwd = ThreadModifyPwd(Program.Dictionary[i]);                                     //00032Pwd
                    pwd = String.Concat(j, pwd);
                    ChangeBreakedPassword(pwd);

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
