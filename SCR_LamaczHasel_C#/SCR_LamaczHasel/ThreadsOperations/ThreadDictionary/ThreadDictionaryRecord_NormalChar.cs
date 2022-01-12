using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public class ThreadDictionaryRecord_NormalChar : ThreadDictionaryRecord
    {
        public override int BreakAllPasswords(PwdModify pwdModify)                     //Func<string, string> ThreadModifyPwd  ->  przekazuje tutaj funkcje modyfikujaca stringa jako parametr - delegaty
        {
            for (int i = 0; i < Program.Dictionary.Length; i++)                         // TODO -> ask teacher how to modify it to recurention function with depth param!
            {
                for (int j = 0; j < _charsList.Length; j++)
                {
                    for (int k = 0; k < _charsList.Length; k++)
                    {
                        for (int l = 0; l < _charsList.Length; l++)
                        {
                            string pwd = pwdModify.ThreadModifyPwd(Program.Dictionary[i]);
                            pwd = String.Concat(pwd, _charsList[j]);
                            pwd = String.Concat(pwd, _charsList[k]);
                            pwd = String.Concat(pwd, _charsList[l]);
                            ChangeBreakedPassword(pwd);

                            if (ValidateEnd())
                                return 0;
                        }
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
