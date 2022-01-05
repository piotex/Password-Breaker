#ifndef operacje_io
#define operacje_io

#include <openssl/md5.h>
#include "includy.h"
#define MAX_BUFF 70

int wczytajTXT(char* nazwa, char*** slownik, int* wymiar);
int printTXT(char* nazwa, char** _slownik, int _wymiar, char* zlamaneHaslo);
int czy_stringi_takie_same(char* str1, char* str2);
int czy_haslo_wystepuje_w_bazie(char* haslo_ze_slownika, char** baza, int dlugosc_bazy);
void str2md5(char *string, char* outString) ;

#endif