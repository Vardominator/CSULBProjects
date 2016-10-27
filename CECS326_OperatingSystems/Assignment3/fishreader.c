#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>

#define SHSIZE 100

int main(char argc, char *argv[])
{

    int shmid;
    int count = 5;
    key_t key = 9876;
    int *readArray;
    int i;

    shmid = shmget(key, SHSIZE, 0666);

    if(shmid < 0)
    {
        perror("shmget");
        return 1;
    }

    // attach to shared memory segment
    readArray = (int * )shmat(shmid, NULL, 0);

    if(readArray < (int *) NULL)
    {
        perror("shmat");
        return 1;
    }

    while(1)
    {

        sleep(1);
        printf("\033[2J");
        printf("\033[1;1H");

        for(i = 0; i < count; i++)
        {
            printf("%d---", readArray[i]);
        }

        printf("\n");

    }

    printf("\nRead from memory successful--\n");

    //*readArray = 99;

    shmdt(readArray);

    return 0;
}