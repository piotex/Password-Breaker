#!/bin/bash
#gcc -o sooo s.c -lpthread

clear

cc -X -c main.c
cc -X -c operacje_io.c
cc -X -c opwatki.c
cc main.o operacje_io.o opwatki.o -lpthread  -lssl -lcrypto

#rm opwatki.o
#rm operacje_io.o
#rm main.o

./a.out #&

rm a.out

