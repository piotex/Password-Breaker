//https://github.com/danielmiessler/SecLists/blob/master/Passwords/Common-Credentials/10k-most-common.txt
//https://www.geeksforgeeks.org/difference-strlen-sizeof-string-c-reviewed/


//#include <openssl/md5.h>
#include "includy.h"
#include "operacje_io.h"
#include "opwatki.h"

void init(pthread_t* tid){
        pthread_create(&tid[1], NULL, lamacz_prostych_hasel, (void *)nazwa_slownika);

        pthread_create(&tid[2], NULL, lamacz_prostych_hasel_f_upper, (void *)nazwa_slownika);
        pthread_create(&tid[3], NULL, lamacz_prostych_hasel_all_upper, (void *)nazwa_slownika);

        pthread_create(&tid[4], NULL, lamacz_prostych_hasel_liczba, (void *)nazwa_slownika);
        pthread_create(&tid[5], NULL, lamacz_prostych_hasel_f_upper_liczba, (void *)nazwa_slownika);
        pthread_create(&tid[6], NULL, lamacz_prostych_hasel_all_upper_liczba, (void *)nazwa_slownika);

        pthread_create(&tid[7], NULL, liczba_lamacz_prostych_hasel, (void *)nazwa_slownika);
        pthread_create(&tid[8], NULL, liczba_lamacz_prostych_hasel_f_upper, (void *)nazwa_slownika);
        pthread_create(&tid[9], NULL, liczba_lamacz_prostych_hasel_all_upper, (void *)nazwa_slownika);

        pthread_create(&tid[10], NULL, liczba_lamacz_prostych_hasel_liczba, (void *)nazwa_slownika);
        pthread_create(&tid[11], NULL, liczba_lamacz_prostych_hasel_f_upper_liczba, (void *)nazwa_slownika);
        pthread_create(&tid[12], NULL, liczba_lamacz_prostych_hasel_all_upper_liczba, (void *)nazwa_slownika);


        pthread_create(&tid[13], NULL, lamacz_prostych_hasel_znak, (void *)nazwa_slownika);
        pthread_create(&tid[14], NULL, lamacz_prostych_hasel_f_upper_znak, (void *)nazwa_slownika);
        pthread_create(&tid[15], NULL, lamacz_prostych_hasel_all_upper_znak, (void *)nazwa_slownika);

        pthread_create(&tid[16], NULL, znak_lamacz_prostych_hasel, (void *)nazwa_slownika);
        pthread_create(&tid[17], NULL, znak_lamacz_prostych_hasel_f_upper, (void *)nazwa_slownika);
        pthread_create(&tid[18], NULL, znak_lamacz_prostych_hasel_all_upper, (void *)nazwa_slownika);

        pthread_create(&tid[19], NULL, znak_lamacz_prostych_hasel_znak, (void *)nazwa_slownika);
        pthread_create(&tid[20], NULL, znak_lamacz_prostych_hasel_f_upper_znak, (void *)nazwa_slownika);
        pthread_create(&tid[21], NULL, znak_lamacz_prostych_hasel_all_upper_znak, (void *)nazwa_slownika);

        pthread_create(&tid[22], NULL, lamacz_prostych_hasel_znak2, (void *)nazwa_slownika);
        pthread_create(&tid[23], NULL, lamacz_prostych_hasel_f_upper_znak2, (void *)nazwa_slownika);
        pthread_create(&tid[24], NULL, lamacz_prostych_hasel_all_upper_znak2, (void *)nazwa_slownika);

        pthread_create(&tid[25], NULL, znak_lamacz_prostych_hasel2, (void *)nazwa_slownika);
        pthread_create(&tid[26], NULL, znak_lamacz_prostych_hasel_f_upper2, (void *)nazwa_slownika);
        pthread_create(&tid[27], NULL, znak_lamacz_prostych_hasel_all_upper2, (void *)nazwa_slownika);

        pthread_create(&tid[28], NULL, znak_lamacz_prostych_hasel_znak2, (void *)nazwa_slownika);
        pthread_create(&tid[29], NULL, znak_lamacz_prostych_hasel_f_upper_znak2, (void *)nazwa_slownika);
        pthread_create(&tid[30], NULL, znak_lamacz_prostych_hasel_all_upper_znak2, (void *)nazwa_slownika);
}

int main(){
    char slownik_urzytkownika[70]; 
    char* nazwa_slownika = "slownik.txt";
    char* nazwa_hasel = "hasla.txt";
       
    int ntimes = 31;

    pthread_t tid[ntimes];

    pthread_mutex_init(&mutex_na_wpisywanie_hasla, NULL);
    pthread_mutex_init(&mutex_na_zapisywanie_do_pliku, NULL);
    pthread_cond_init(&cond_na_wpisywanie_hasla, NULL);
    pthread_cond_init(&cond_na_zapisywanie_do_pliku, NULL);
    
    wczytajTXT(nazwa_hasel,    &hasla,   &ilosc_hasel);
    wczytajTXT(nazwa_slownika, &slownik, &ilosc_slow);

    pthread_create(&tid[0], NULL, konsument_sprawdzacz_lapacz, (void *)nazwa_slownika);

    init(tid);

    while(1>0){
        printf("Podaj nazwe slownika:\n");
        scanf("%s",slownik_urzytkownika);

        for (int i = 1; i < 9; i++)
        {
            pthread_kill( tid[i], 9);                   //SIGKILL - oszczedzamy konsumenta
        }
        wczytajTXT(nazwa_hasel,    &hasla,   &ilosc_hasel);
        // wczytajTXT(slownik_urzytkownika, &slownik, &ilosc_slow);
        free(*slownik);
        free(slownik);
        slownik = NULL;
        ilosc_slow=0;
        wczytajTXT(slownik_urzytkownika, &slownik, &ilosc_slow);
        init(tid);
    }

    printf("END------main------\r\n");
    pthread_exit(NULL);
    printf("END------ALL------\r\n");   //to sie nie wypisze :<
    return 0;
}
