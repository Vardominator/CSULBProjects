#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>


// size of the shared memory
#define SHSIZE 100

int main (int argc, char *argv[])
{

    // shared memory id
    int shmid;
    key_t key;
    char *shm;
    char *s;

    key = 9876;

    shmid = shmget(key, SHSIZE, IPC_CREAT | 0666);

    if(shmid < 0)
    {

        perror("shmget");
        exit(1);

    }

    // set up shared memory
    shm = shmat(shmid, NULL, 0);

    if(shm == (char *) - 1)
    {
        perror("shmat");
        exit(1);
    }

    // write string to shared memory
    memcpy(shm, "Hello world", 11);


    s = shm;
    s += 11;

    // add 0 at the end of the string
    *s = 0;

    while(*shm != '*')
    {
        sleep(1);
    }


    return 0;
}