#include "opwatki.h"

int max_int_pwd = 200;
int max_buffor = 70;


void * konsument_sprawdzacz_lapacz(void *nazwa_pliku){
  while(1>0){
    pthread_mutex_lock(&mutex_na_wpisywanie_hasla);                                 //zablokuj mozliwosc wpisywania hasel do pamieci                 
    pthread_cond_wait(&cond_na_wpisywanie_hasla, &mutex_na_wpisywanie_hasla);       //odblokuj mozliwosc w_h_d_p i czekaj na warunek poprawnego hasla w pamieci ....

///*                            przez to ze wycinamy slowo ze slownika, alternatywne wersje hasla juz nie sa sprawdzane
    //update pliku
    char tmp[50] = ""; 
    char buff[50] = "#"; 
    strcpy(tmp, slownik[zlamaneIndex]);
    strcat(buff, tmp);
    strcpy(slownik[zlamaneIndex], buff);
    printf("---ZLAMANE HASLO: %s               Oryginal: %s\n", zlamaneHaslo, slownik[zlamaneIndex]);
//*/  
//printf("|--ZLAMANE HASLO:   %s\n", zlamaneHaslo);

    //char* tmp = "tmp.txt";
    //printTXT(tmp, slownik, ilosc_slow, zlamaneHaslo);

    pthread_cond_signal(&cond_na_zapisywanie_do_pliku);                          //puszcza sygnal do wszystkich watkow ze skonczyl zapisywac -> to odblokuje blokade w watku zapisujacym haslo do pamieci
    pthread_mutex_unlock(&mutex_na_wpisywanie_hasla);                               //zdejmuje blokade na wpisywania hasel do pamieci   
  }
}
//-------------------------------------------------------------------
char* _to_all_Upper(char* tmp, char* slownik){
    strcpy(tmp, slownik);
    char *s = tmp;
    while (*s) {
      *s = toupper((unsigned char) *s);
      s++;
    }
    return tmp;
}
char* _to_sing_Upper(char* tmp, char* slownik){
    strcpy(tmp, slownik);
    tmp[0] = toupper(tmp[0]);
    return tmp;
}
//-------------------------------------------------------------------
void *lamacz_prostych_hasel(void *nazwa_pliku){
  for(int i=0; i< ilosc_slow; i++){
    if(czy_haslo_wystepuje_w_bazie(slownik[i], hasla, ilosc_hasel)){
	    podmien_haslo_w_pamieci_i_daj_sygnal(slownik[i],i); 
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_f_upper(void *nazwa_pliku){
  for(int i=0; i< ilosc_slow; i++){
    char tmp[70];
    _to_sing_Upper(tmp,slownik[i]);
    if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
	    podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_all_upper(void *nazwa_pliku){
  for(int i=0; i< ilosc_slow; i++){
    char tmp[max_buffor];
    _to_all_Upper(tmp,slownik[i]);
    if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
	    podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
    }
  }
  return 0;
}
//-------------------------------------------------------------------
//-----------------------liczby---------------------------
//-------------------------------------------------------------------
void *lamacz_prostych_hasel_liczba(void *nazwa_pliku){
  for (int k = 0; k < max_int_pwd; k++)
  {
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      char buff[70];
      strcpy(tmp, slownik[i]);
      sprintf(buff, "%d", k);
      strcat(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_f_upper_liczba(void *nazwa_pliku){
  for (int k = 0; k < max_int_pwd; k++)
  {
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      char buff[70];
      strcpy(tmp, slownik[i]);
      tmp[0] = toupper(tmp[0]);
      sprintf(buff, "%d", k);
      strcat(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_all_upper_liczba(void *nazwa_pliku){
  for (int k = 0; k < max_int_pwd; k++)
  {
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      char buff[70];
      strcpy(tmp, slownik[i]);
      char *s = tmp;
      while (*s) {
        *s = toupper((unsigned char) *s);
        s++;
      }
      tmp[0] = toupper(tmp[0]);
      sprintf(buff, "%d", k);
      strcat(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
void *liczba_lamacz_prostych_hasel(void *nazwa_pliku){
  for (int k = 0; k < max_int_pwd; k++)
  {
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      char buff[70];
      strcpy(tmp, slownik[i]);
      sprintf(buff, "%d", k);
      strcat(buff, tmp);
      strcpy(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *liczba_lamacz_prostych_hasel_f_upper(void *nazwa_pliku){
  for (int k = 0; k < max_int_pwd; k++)
  {
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      char buff[70];
      strcpy(tmp, slownik[i]);
      tmp[0] = toupper(tmp[0]);
      sprintf(buff, "%d", k);
      strcat(buff, tmp);
      strcpy(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *liczba_lamacz_prostych_hasel_all_upper(void *nazwa_pliku){
  for (int k = 0; k < max_int_pwd; k++)
  {
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      char buff[70];
      strcpy(tmp, slownik[i]);
      char *s = tmp;
      while (*s) {
        *s = toupper((unsigned char) *s);
        s++;
      }
      tmp[0] = toupper(tmp[0]);
      sprintf(buff, "%d", k);
      strcat(buff, tmp);
      strcpy(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
void *liczba_lamacz_prostych_hasel_liczba(void *nazwa_pliku){
  for (int l = 0; l < max_int_pwd; l++)
  {
    for (int k = 0; k < max_int_pwd; k++)
    {
      for(int i=0; i< ilosc_slow; i++){
        char tmp[max_buffor];
        char lll[max_buffor];
        char ppp[max_buffor];

        strcpy(tmp, slownik[i]);
        sprintf(lll, "%d", k);
        sprintf(ppp, "%d", l);
        strcat(lll, tmp);
        strcat(lll, ppp);
        strcpy(tmp, lll);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *liczba_lamacz_prostych_hasel_f_upper_liczba(void *nazwa_pliku){
  for (int l = 0; l < max_int_pwd; l++)
  {
    for (int k = 0; k < max_int_pwd; k++)
    {
      for(int i=0; i< ilosc_slow; i++){
        char tmp[max_buffor];
        char lll[max_buffor];
        char ppp[max_buffor];

        strcpy(tmp, slownik[i]);
        tmp[0] = toupper(tmp[0]);

        sprintf(lll, "%d", k);
        sprintf(ppp, "%d", l);
        strcat(lll, tmp);
        strcat(lll, ppp);
        strcpy(tmp, lll);

        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *liczba_lamacz_prostych_hasel_all_upper_liczba(void *nazwa_pliku){
  for (int l = 0; l < max_int_pwd; l++)
  {
    for (int k = 0; k < max_int_pwd; k++)
    {
      for(int i=0; i< ilosc_slow; i++){
        char tmp[max_buffor];
        char lll[max_buffor];
        char ppp[max_buffor];

        strcpy(tmp, slownik[i]);
        char *s = tmp;
        while (*s) {
          *s = toupper((unsigned char) *s);
          s++;
        }

        sprintf(lll, "%d", k);
        sprintf(ppp, "%d", l);
        strcat(lll, tmp);
        strcat(lll, ppp);
        strcpy(tmp, lll);

        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
//-----------------------znaki---------------------------
//-------------------------------------------------------------------
char* znaki = "!@#$^&*()_+=-[]}{|;:?><,./";
int ilosc_znakow = 25;

void *lamacz_prostych_hasel_znak(void *nazwa_pliku){
  for (int k = 0; k < ilosc_znakow; k++){
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      strcpy(tmp, slownik[i]);
      strncat(tmp, &znaki[k], 1);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_f_upper_znak(void *nazwa_pliku){
  for (int k = 0; k < ilosc_znakow; k++){
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70] = "";
      strcpy(tmp, slownik[i]);
      tmp[0] = toupper(tmp[0]);
      strncat(tmp, &znaki[k], 1);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_all_upper_znak(void *nazwa_pliku){
  for (int k = 0; k < ilosc_znakow; k++){
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70];
      char buff[70];
      strcpy(tmp, slownik[i]);
      char *s = tmp;
      while (*s) {
        *s = toupper((unsigned char) *s);
        s++;
      }
      strncat(tmp, &znaki[k], 1);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
void *znak_lamacz_prostych_hasel(void *nazwa_pliku){
  for (int k = 0; k < ilosc_znakow; k++){
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70] = "";
      char buff[70] = "";
      strcpy(tmp, slownik[i]);
      strncat(buff, &znaki[k], 1);
      strcat(buff, tmp);
      strcpy(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_f_upper(void *nazwa_pliku){
  for (int k = 0; k < ilosc_znakow; k++){
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70] = "";
      char buff[70] = "";
      strcpy(tmp, slownik[i]);
      tmp[0] = toupper(tmp[0]);
      strncat(buff, &znaki[k], 1);
      strcat(buff, tmp);
      strcpy(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_all_upper(void *nazwa_pliku){
  for (int k = 0; k < ilosc_znakow; k++){
    for(int i=0; i< ilosc_slow; i++){
      char tmp[70] = "";
      char buff[70] = "";
      strcpy(tmp, slownik[i]);
      char *s = tmp;
      while (*s) {
        *s = toupper((unsigned char) *s);
        s++;
      }
      tmp[0] = toupper(tmp[0]);
      strncat(buff, &znaki[k], 1);
      strcat(buff, tmp);
      strcpy(tmp, buff);
      if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
        podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
void *znak_lamacz_prostych_hasel_znak(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow; l++){
    for (int k = 0; k < ilosc_znakow; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70]="";
        char lll[70]="";
        char ppp[70]="";

        strcpy(tmp, slownik[i]);
        strncat(lll, &znaki[k], 1);
        strncat(ppp, &znaki[l], 1);
        strcat(lll, tmp);
        strcat(lll, ppp);
        strcpy(tmp, lll);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_f_upper_znak(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow; l++){
    for (int k = 0; k < ilosc_znakow; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70]="";
        char lll[70]="";
        char ppp[70]="";

        strcpy(tmp, slownik[i]);
        tmp[0] = toupper(tmp[0]);

        strncat(lll, &znaki[k], 1);
        strncat(ppp, &znaki[l], 1);
        strcat(lll, tmp);
        strcat(lll, ppp);
        strcpy(tmp, lll);

        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_all_upper_znak(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow; l++){
    for (int k = 0; k < ilosc_znakow; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70]="";
        char lll[70]="";
        char ppp[70]="";

        strcpy(tmp, slownik[i]);
        char *s = tmp;
        while (*s) {
          *s = toupper((unsigned char) *s);
          s++;
        }

        strncat(lll, &znaki[k], 1);
        strncat(ppp, &znaki[l], 1);
        strcat(lll, tmp);
        strcat(lll, ppp);
        strcpy(tmp, lll);

        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
//-----------------------znaki i liczby - kombinacja---------------------------
//-------------------------------------------------------------------
char* znaki2 = "!@#$^&*()_+=-[]}{|;:?><,./0123456789";
int ilosc_znakow2 = 35;

void *lamacz_prostych_hasel_znak2(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow2; l++){
    for (int k = 0; k < ilosc_znakow2; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70];
        strcpy(tmp, slownik[i]);
        strncat(tmp, &znaki2[k], 1);
        strncat(tmp, &znaki2[l], 1);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_f_upper_znak2(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow2; l++){
    for (int k = 0; k < ilosc_znakow2; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70] = "";
        strcpy(tmp, slownik[i]);
        tmp[0] = toupper(tmp[0]);
        strncat(tmp, &znaki2[k], 1);
        strncat(tmp, &znaki2[l], 1);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *lamacz_prostych_hasel_all_upper_znak2(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow2; l++){
    for (int k = 0; k < ilosc_znakow2; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70];
        char buff[70];
        strcpy(tmp, slownik[i]);
        char *s = tmp;
        while (*s) {
          *s = toupper((unsigned char) *s);
          s++;
        }
        strncat(tmp, &znaki2[k], 1);
        strncat(tmp, &znaki2[l], 1);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
void *znak_lamacz_prostych_hasel2(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow2; l++){
    for (int k = 0; k < ilosc_znakow2; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70] = "";
        char buff[70] = "";
        strcpy(tmp, slownik[i]);
        strncat(buff, &znaki2[k], 1);
        strncat(buff, &znaki2[l], 1);
        strcat(buff, tmp);
        strcpy(tmp, buff);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_f_upper2(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow2; l++){
    for (int k = 0; k < ilosc_znakow2; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70] = "";
        char buff[70] = "";
        strcpy(tmp, slownik[i]);
        tmp[0] = toupper(tmp[0]);
        strncat(buff, &znaki2[k], 1);
        strncat(buff, &znaki2[l], 1);
        strcat(buff, tmp);
        strcpy(tmp, buff);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_all_upper2(void *nazwa_pliku){
  for (int l = 0; l < ilosc_znakow2; l++){
    for (int k = 0; k < ilosc_znakow2; k++){
      for(int i=0; i< ilosc_slow; i++){
        char tmp[70] = "";
        char buff[70] = "";
        strcpy(tmp, slownik[i]);
        char *s = tmp;
        while (*s) {
          *s = toupper((unsigned char) *s);
          s++;
        }
        tmp[0] = toupper(tmp[0]);
        strncat(buff, &znaki2[k], 1);
        strncat(buff, &znaki2[l], 1);
        strcat(buff, tmp);
        strcpy(tmp, buff);
        if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
          podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
        }
      }
    }
  }
  return 0;
}
//-------------------------------------------------------------------
void *znak_lamacz_prostych_hasel_znak2(void *nazwa_pliku){
  for (int n = 0; n < ilosc_znakow2; n++){
    for (int m = 0; m < ilosc_znakow2; m++){
      for (int l = 0; l < ilosc_znakow2; l++){
        for (int k = 0; k < ilosc_znakow2; k++){
          for(int i=0; i< ilosc_slow; i++){
            char tmp[70]="";
            char lll[70]="";
            char ppp[70]="";

            strcpy(tmp, slownik[i]);
            strncat(lll, &znaki[k], 1);
            strncat(lll, &znaki[l], 1);
            strncat(ppp, &znaki[m], 1);
            strncat(ppp, &znaki[n], 1);
            strcat(lll, tmp);
            strcat(lll, ppp);
            strcpy(tmp, lll);
            if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
              podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
            }
          }
        }
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_f_upper_znak2(void *nazwa_pliku){
  for (int n = 0; n < ilosc_znakow2; n++){
    for (int m = 0; m < ilosc_znakow2; m++){
      for (int l = 0; l < ilosc_znakow2; l++){
        for (int k = 0; k < ilosc_znakow2; k++){
          for(int i=0; i< ilosc_slow; i++){
            char tmp[70]="";
            char lll[70]="";
            char ppp[70]="";

            strcpy(tmp, slownik[i]);
            tmp[0] = toupper(tmp[0]);

            strncat(lll, &znaki[k], 1);
            strncat(lll, &znaki[l], 1);
            strncat(ppp, &znaki[m], 1);
            strncat(ppp, &znaki[n], 1);
            strcat(lll, tmp);
            strcat(lll, ppp);
            strcpy(tmp, lll);

            if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
              podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
            }
          }
        }
      }
    }
  }
  return 0;
}
void *znak_lamacz_prostych_hasel_all_upper_znak2(void *nazwa_pliku){
  for (int n = 0; n < ilosc_znakow2; n++){
    for (int m = 0; m < ilosc_znakow2; m++){
      for (int l = 0; l < ilosc_znakow2; l++){
        for (int k = 0; k < ilosc_znakow2; k++){
          for(int i=0; i< ilosc_slow; i++){
            char tmp[70]="";
            char lll[70]="";
            char ppp[70]="";

            strcpy(tmp, slownik[i]);
            char *s = tmp;
            while (*s) {
              *s = toupper((unsigned char) *s);
              s++;
            }

            strncat(lll, &znaki[k], 1);
            strncat(lll, &znaki[l], 1);
            strncat(ppp, &znaki[m], 1);
            strncat(ppp, &znaki[n], 1);
            strcat(lll, tmp);
            strcat(lll, ppp);
            strcpy(tmp, lll);
            if(czy_haslo_wystepuje_w_bazie(tmp, hasla, ilosc_hasel)){
              podmien_haslo_w_pamieci_i_daj_sygnal(tmp,i); 
            }
          }
        }
      }
    }
  }
  return 0;
}





int podmien_haslo_w_pamieci_i_daj_sygnal(char* haslo, int index){
  pthread_mutex_lock(&mutex_na_zapisywanie_do_pliku);                              //zablokuj mozliwosc dzialania az do otrzymania sygnalu ze zupdatowano plik            pthread_mutex_lock(&mutex_na_wpisywanie_hasla);                                  //zablokuj mozliwosc wpisywania hasel do pamieci
	pthread_mutex_lock(&mutex_na_wpisywanie_hasla); 
	zlamaneHaslo = haslo;  
  zlamaneIndex = index;                                                    //wpisz haslo do pamieci
	pthread_cond_signal(&cond_na_wpisywanie_hasla);                                 //wyslij sygnal ze haslo jest ok w pamieci -> to odblokowuje blokade w konsumencie, ktory moze updatowac plik
	pthread_cond_wait(&cond_na_zapisywanie_do_pliku, &mutex_na_wpisywanie_hasla);   //zwolnij blokade w
	pthread_mutex_unlock(&mutex_na_wpisywanie_hasla); 
	pthread_mutex_unlock(&mutex_na_zapisywanie_do_pliku); 
  return 0;
}
