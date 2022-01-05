#include "operacje_io.h"

int czy_stringi_takie_same(char* str1, char* str2){
    if (str1 == NULL || str2 == NULL)
    {
        return 0;
    }
    char tmp1[50] = "";
    char tmp2[50] = "";
    for (int i = 0; i < 20; i++)
    {
        tmp1[i] = str1[i];
        tmp2[i] = str2[i];
    }
    str2md5(tmp1,tmp1);
    str2md5(tmp2,tmp2);

    if (strlen(tmp1) == strlen(tmp2))
    {       
        for(int i=0; i< strlen(tmp1); i++){
            if(tmp1[i] != tmp2[i]){
                return 0;
            }
        }   
        return 1;
    }
    /*
    if (strlen(str1) == strlen(str2))
    {       
        for(int i=0; i< strlen(str1); i++){
            if(str1[i] != str2[i]){
                return 0;
            }
        }   
        return 1;
    }
    */
    return 0;
}

int czy_haslo_wystepuje_w_bazie(char* haslo_ze_slownika, char** baza, int dlugosc_bazy){
    if (haslo_ze_slownika[0] == '#')
    {
        return 0;
    }
    
    for(int i=0; i< dlugosc_bazy; i++){
        if(czy_stringi_takie_same(haslo_ze_slownika, baza[i])){
            return 1;
        }
    }
    return 0;
}

void str2md5(char *string, char* outString) {
    unsigned char digest[16];
    //const char* string = "Hello World";
    MD5_CTX context;
    MD5_Init(&context);
    MD5_Update(&context, string, strlen(string));
    MD5_Final(digest, &context);
    int i;
    for (i=0; i<16; i++) {
        //printf("%02x", digest[i]);
        outString[i] = digest[i];
    }
    //printf("\n");
    return 0;
}

int wczytajTXT(char* nazwa, char*** _slownik, int* _wymiar){

    FILE *plik;
    
    plik = fopen(nazwa, "r");

    fscanf(plik, "%d\n", (_wymiar));

    (*_slownik) = malloc((*_wymiar) * sizeof(char*));
    for (int i = 0; i < (*_wymiar); i++){
        (*_slownik)[i] = malloc((MAX_BUFF+1) * sizeof(char)); 
        for(int j = 0; j < MAX_BUFF; j++){
            char ccc;
            fscanf(plik, "%c", &ccc);

            //printf("i: %d ",i);
            //printf("j: %d\n",j);
            //printf("---------------------------  %c   :\n",ccc);

            if(ccc == '\n'){
                break;
            }
            (*_slownik)[i][j] = ccc;
        }
    }

    fclose(plik);
    return 1;
}



int printTXT(char* nazwa, char** _slownik, int _wymiar, char* zlamaneHaslo){
    FILE *plik;
    
    plik = fopen(nazwa, "w");

    for (int i = 0; i < _wymiar; i++){
        if (czy_stringi_takie_same(zlamaneHaslo,slownik[i])){
            printf("xDDDDDDDDDDDD\r\n");
            fprintf(plik, "#%s\r\n", slownik[i]);
        }
        else{
            fprintf(plik, "%s\r\n", slownik[i]);
        }
        

        //for(int j = 0; j < strlen(_slownik[i]); j++){
        //}
        //printf("\r\n");
    }
    return 1;
}