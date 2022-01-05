#ifndef opwatki
#define opwatki

#include <ctype.h>
#include "includy.h"
#include "operacje_io.h"

void *lamacz_prostych_hasel(void *nazwa_pliku);
void *lamacz_prostych_hasel_f_upper(void *nazwa_pliku);
void *lamacz_prostych_hasel_all_upper(void *nazwa_pliku);

void *lamacz_prostych_hasel_liczba(void *nazwa_pliku);
void *lamacz_prostych_hasel_f_upper_liczba(void *nazwa_pliku);
void *lamacz_prostych_hasel_all_upper_liczba(void *nazwa_pliku);

void *liczba_lamacz_prostych_hasel(void *nazwa_pliku);
void *liczba_lamacz_prostych_hasel_f_upper(void *nazwa_pliku);
void *liczba_lamacz_prostych_hasel_all_upper(void *nazwa_pliku);

void *liczba_lamacz_prostych_hasel_liczba(void *nazwa_pliku);
void *liczba_lamacz_prostych_hasel_f_upper_liczba(void *nazwa_pliku);
void *liczba_lamacz_prostych_hasel_all_upper_liczba(void *nazwa_pliku);

void *lamacz_prostych_hasel_znak(void *nazwa_pliku);
void *lamacz_prostych_hasel_f_upper_znak(void *nazwa_pliku);
void *lamacz_prostych_hasel_all_upper_znak(void *nazwa_pliku);

void *znak_lamacz_prostych_hasel(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_f_upper(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_all_upper(void *nazwa_pliku);

void *znak_lamacz_prostych_hasel_znak(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_f_upper_znak(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_all_upper_znak(void *nazwa_pliku);

void *lamacz_prostych_hasel_znak2(void *nazwa_pliku);
void *lamacz_prostych_hasel_f_upper_znak2(void *nazwa_pliku);
void *lamacz_prostych_hasel_all_upper_znak2(void *nazwa_pliku);

void *znak_lamacz_prostych_hasel2(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_f_upper2(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_all_upper2(void *nazwa_pliku);

void *znak_lamacz_prostych_hasel_znak2(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_f_upper_znak2(void *nazwa_pliku);
void *znak_lamacz_prostych_hasel_all_upper_znak2(void *nazwa_pliku);



void * konsument_sprawdzacz_lapacz(void *nazwa_pliku);
int podmien_haslo_w_pamieci_i_daj_sygnal(char* haslo, int index);

#endif 