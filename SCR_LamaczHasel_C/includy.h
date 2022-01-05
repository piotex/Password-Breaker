#ifndef includy
#define includy


#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>


pthread_mutex_t mutex_na_wpisywanie_hasla;
pthread_cond_t cond_na_wpisywanie_hasla;

pthread_mutex_t mutex_na_zapisywanie_do_pliku;
pthread_cond_t cond_na_zapisywanie_do_pliku;

char* zlamaneHaslo;
int zlamaneIndex;

char** hasla;
char** slownik;
int ilosc_hasel;
int ilosc_slow;
char* nazwa_slownika;
char* nazwa_hasel ;

#endif