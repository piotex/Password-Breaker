using SCR_LamaczHasel.Pwd;
using SCR_LamaczHasel.ThreadsOperations;
using System;
using System.IO;
using System.Threading;

namespace SCR_LamaczHasel
{
    public class Program
    {
        public static long DiedThreads = 0;
        public static bool TimeToDie = false;

        public static string[] Dictionary;
        public static PwdModel[] Passwords;

        public static BreakedPwdModel BreakedPassword = new BreakedPwdModel();

        public static EventWaitHandle eventBreakedPassword = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle eventModifiedFileData = new EventWaitHandle(false, EventResetMode.AutoReset);

        public static readonly object _pwdChanging_locker = new object();

        public static void Main(string[] args)
        {
            MainUtils mainUtils = new MainUtils();
            //MainUtils.UpdateDictionaty();                                                 //function to cut wrong words from db/dictionary
            //MainUtils.MakeHashMD5PwdFile();
            int i = 0;
            bool cont = true;
            Thread[] threadList = new Thread[mainUtils.ThreadCount];

            while (cont)
            {
                Console.WriteLine("Podaj scierzke do slownika: ");
                string input = Console.ReadLine();
                if (i != 0)
                    mainUtils._abort_threadList(ref threadList);

                mainUtils._set_Dic(input);
                mainUtils._set_Pwd();
                mainUtils._init_threadList(ref threadList);
                i++;
                Thread.Sleep(1000);
                //break;
            }
        }
    }
}


/*
//---------------------------------------To Do-----------------------------------------------------------
//---------------------------------------Main-----------------------------------------------------------
X   1) wczytanie do pamieci listy hasel do pamieci globalnej
X   2) pobranie od usera pliku slownika
X   3) wczytanie listy ze slownika do pamieci globalnej
X   4) zabicie watkow ktore chodza
X   5) odpalenie watkow
//--------------SPRAWDZACZ--------------Watek czekajacy na sygnal ze zlamano haslo-----------------------------------------------------------
X   1) oczekuj na sygnal o info ze haslo zostalo zlamane i jest w pamieci               <--- sing_Zlamano_Haslo <---
X    2) po otrzymaniu sygnalu ze zlamano haslo: punkt 1.4.4 z 39 : oznacz wprowadzone haslo w pamieci
---2.0) naluz mutex na liste hasel w pamieci                              mutex_lista +                       //mozna wywalic            //to spowoduje dodatkowo wstrzymanie watkow ktore nie beda moglu pobierac nowych hasel
X   2.1) zmien wartosc zlamanego hasla w liscie <--> HASLA <--> w pamieci
X   2.2) wprowadx haslo do pliku
---2.2) zwolnij mutex                                                     mutex_lista -                       //mozna wywalic 
X   3) wyslij sygnal ze skonczono grzebac w danych krytycznych                          ---> sign_KoniecGrzebaniaWPamieci --->
//---------------------------------------Watek lamiacy proste hasla-----------------------------------------------------------
1) dla kazdego hasla:
1.1) nałuż sekcje krytyczną na liste haseł                              mutex_lista +                       //mozna wywalic 
1.2) pobierz hasło i bool czy_zlamane do zmiennej pomocniczej
1.3) zwolnij mutek                                                      mutex_lista -                       //mozna wywalic 
1.4) jeśli niezłamane -> sprobuj zlamac na swoj dedykowany sposob
1.4.1) w momencie zlamania hasla:
1.4.2) naluz sekcje krytyczna na (globalna zmienna) ZlamaneHaslo        mutex_haslo +       // !obowiazkowe
1.4.3) zmien wartosc na haslo ktore zostalo zlamane
1.4.4) wyslij sygnal do sprawdzacza: ze zlamano haslo                               ---> sing_Zlamano_Haslo --->
1.4.5) czekaj na sygnal ze skonczono grzebac w pamieci                              <--- sign_KoniecGrzebaniaWPamieci <---
1.4.6) zwolnij mutex                                                    mutex_haslo -       // !obowiazkowe
1.4.7 wczytaj nowe haslo
//---------------------------------------Kolejne watki-----------------------------------------------------------
jak wyzej
//--------------------------------------- M D 5 -----------------------------------------------------------
//--------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------



class Record{
    string haslo
    bool czy_zlamano
}
*/