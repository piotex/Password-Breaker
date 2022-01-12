using SCR_LamaczHasel.ThreadsOperations.PwdModif;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SCR_LamaczHasel.ThreadsOperations.ThreadDictionary
{
    public class ThreadDictionaryRecord_NormalALL : ThreadDictionaryRecord
    {
        public override int BreakAllPasswords(Func<string, string> ThreadModifyPwd)        
        {
            for (int i = 1; i < _ALLcharsList.Length; i++)
            {
                for (int j = 0; j < _ALLcharsList.Length; j++)
                {
                    for (int k = 0; k < _ALLcharsList.Length; k++)
                    {
                        for (int l = 0; l < _ALLcharsList.Length; l++)
                        {
                            for (int m = 0; m < _ALLcharsList.Length; m++)
                            {
                                for (int n = 0; n < _ALLcharsList.Length; n++)
                                {
                                    for (int o = 0; o < _ALLcharsList.Length; o++)
                                    {
                                        for (int p = 0; p < _ALLcharsList.Length; p++)
                                        {
                                            for (int r = 0; r < _ALLcharsList.Length; r++)
                                            {
                                                string pwd = "";
                                                if (_ALLcharsList[i] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[i], pwd);
                                                if (_ALLcharsList[j] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[j], pwd);
                                                if (_ALLcharsList[k] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[k], pwd);
                                                if (_ALLcharsList[l] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[l], pwd);
                                                if (_ALLcharsList[m] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[m], pwd);
                                                if (_ALLcharsList[n] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[n], pwd);
                                                if (_ALLcharsList[o] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[o], pwd);
                                                if (_ALLcharsList[p] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[p], pwd);
                                                if (_ALLcharsList[r] != '\0')
                                                    pwd = String.Concat(_ALLcharsList[r], pwd);

                                                ChangeBreakedPassword(pwd);
                                                if (ValidateEnd())
                                                    return 0;
                                            }
                                        }
                                    }
                                }
                            }
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
