#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <pthread.h>


#define SHSIZE 100

typedef int bool;
#define true 1
#define false 0

int main(char argc, char *argv[])
{

    int shmid;
    int width = 5, height = 5;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    key_t key = 9876;

    shmid = shmget(key, SHSIZE, IPC_CREAT | 0666);

    if(shmid < 0)
    {
        perror("shmget");
        return 1;
    }

    int *row = shmat(shmid, NULL, 0);
    if(row < (int *) NULL)
    {
        perror("shmat");
        return 1;
    }

    // initialize array
    for(i = 0; i < width; i++)
    {
        for(j = 0; j < height; j++)
        {
            // map 2d array to 1d
            row[i * width + j] = i * width + j;
        }
    }


    while(1)
    {

        printf("Waiting to be read--\n");
        sleep(1);
        printf("\033[2J");
        printf("\033[1;1H");

        for(i = 0; i < width; i++)
        {   
            for(j = 0; j < height; j++)
            {
                // map 2d array to 1d
                row[i * width + j] += 1;
            }
        }


    }

    shmdt(row);

    return 0;

}
