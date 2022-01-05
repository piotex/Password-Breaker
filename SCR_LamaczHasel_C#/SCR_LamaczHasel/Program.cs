using System;

namespace SCR_LamaczHasel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}


/*
//---------------------------------------To Do-----------------------------------------------------------
//---------------------------------------Main-----------------------------------------------------------
1) wczytanie do pamieci listy hasel do pamieci globalnej
2) pobranie od usera pliku slownika
3) wczytanie listy ze slownika do pamieci globalnej
4) zabicie watkow ktore chodza
5) odpalenie watkow
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
/*


class Record{
    string haslo
    bool czy_zlamano
}