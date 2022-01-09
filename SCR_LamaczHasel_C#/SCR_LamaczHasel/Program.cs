using SCR_LamaczHasel.Pwd;
using SCR_LamaczHasel.ThreadsOperations;
using System;
using System.IO;
using System.Threading;

namespace SCR_LamaczHasel
{
    public class Program
    {
        private const int ThreadCount = 2;
        public static long DiedThreads = 0;
        public static bool TimeToDie = false;

        private const string _path_dic = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\slownik.txt";
        private const string _path_pwd = @"C:\Users\pkubo\Desktop\Politechnika\Password-Breaker\SCR_LamaczHasel_C#\hasla.txt";

        public static PwdModel[] passwords;
        public static string[] dictionary;

        public static EventWaitHandle ewh;
        public static EventWaitHandle clearCount = new EventWaitHandle(false, EventResetMode.AutoReset);

        public static string BreakedPassword = "";

        public static void _set_Pwd()
        {
            string[] tmp = File.ReadAllLines(_path_pwd);
            passwords = new PwdModel[tmp.Length];

            for (int i = 0; i < tmp.Length; i++)
                passwords[i] = new PwdModel() { Pwd = tmp[i], Breaked = false };
        }
        public static void _set_Dic(string input)
        {
            if (input == "-")
                dictionary = File.ReadAllLines(_path_dic);
            else
                dictionary = File.ReadAllLines(input);
        }
        public static void _init_threadList(ref Thread[] threadList)
        {
            threadList[0] = new Thread(() => new ThreadDictionaryChecker().MarkBreakedPwds(""));        //wywolanie pojedynczego watku - pojedynczej funkcji - jak sie wykona do konca, watek sie konczy
            threadList[1] = new Thread(() => new ThreadDictionaryRecordBasic().DoSth(""));        


            foreach (Thread thread in threadList)
            {
                if (thread != null)
                    thread.Start();
            }
        }
        public static void _abort_threadList(ref Thread[] threadList)
        {
            TimeToDie = true;
            WaitHandle.SignalAndWait(Program.ewh, Program.clearCount);          //wyslane w celu zatrzymania Checkera

            while (Interlocked.Read(ref DiedThreads) < ThreadCount)
            {
                Console.WriteLine("abort -- wait");
                Thread.Sleep(500);
            }
            while (Interlocked.Read(ref DiedThreads) > 0)
            {
                Interlocked.Decrement(ref DiedThreads);
            }
            TimeToDie = false;
            Console.WriteLine("abort -- finish");
        }

        public static void Main(string[] args)
        {
            int i = 0;
            bool cont = true;
            _set_Pwd();
            ewh = new EventWaitHandle(false, EventResetMode.AutoReset);

            Thread[] threadList = new Thread[ThreadCount];

            Console.WriteLine("--MAIN-- start");

            while (cont)
            {
                Console.WriteLine("Podaj scierzke do slownika: ");
                _set_Dic(Console.ReadLine());
                if (i!=0)
                    _abort_threadList(ref threadList);
                _init_threadList(ref threadList);
                i++;
                Thread.Sleep(1000);
                //break;
            }
            Console.WriteLine("--MAIN----END--\n");
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
1) oczekuj na sygnal o info ze haslo zostalo zlamane i jest w pamieci               <--- sing_Zlamano_Haslo <---
2) po otrzymaniu sygnalu ze zlamano haslo: punkt 1.4.4 z 39 : oznacz wprowadzone haslo w pamieci
2.0) naluz mutex na liste hasel w pamieci                              mutex_lista +                       //mozna wywalic - wedlug senpai            //to spowoduje dodatkowo wstrzymanie watkow ktore nie beda moglu pobierac nowych hasel
2.1) zmien wartosc zlamanego hasla w liscie <--> HASLA <--> w pamieci
2.2) wprowadx haslo do pliku
2.2) zwolnij mutex                                                     mutex_lista -                       //mozna wywalic - wedlug senpai
3) wyslij sygnal ze skonczono grzebac w danych krytycznych                          ---> sign_KoniecGrzebaniaWPamieci --->
//---------------------------------------Watek lamiacy proste hasla-----------------------------------------------------------
1) dla kazdego hasla:
1.1) nałuż sekcje krytyczną na liste haseł                              mutex_lista +                       //mozna wywalic - wedlug senpai
1.2) pobierz hasło i bool czy_zlamane do zmiennej pomocniczej
1.3) zwolnij mutek                                                      mutex_lista -                       //mozna wywalic - wedlug senpai
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