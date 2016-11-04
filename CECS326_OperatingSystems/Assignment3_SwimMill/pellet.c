#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <pthread.h>
#include <time.h>


#define SHSIZE 100

typedef int bool;
#define true 1
#define false 0

void initializePellet(int *row, int width, int height);
void dropPellet(int *row, int width, int height, int randomColumn);

int main(char argc, char *argv[])
{

    srand(time(NULL));

    int shmid;
    int width = 5, height = 5;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    int r = rand() % width;

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


    initializePellet(row, width, height);

    dropPellet(row, width, height, r);


    shmdt(row);

    

    return 0;

}


void initializePellet(int *row, int width, int height)
{
    int i = 0, j = 0;

    // initialize array
    for(i = 0; i < width; i++)
    {
        for(j = 0; j < height; j++)
        {
            // map 2d array to 1d
            row[i * width + j] = 0;
        }
    }

}

void dropPellet(int *row, int width, int height, int randomColumn)
{

    int i = 0, j = 0;

    printf("Waiting to be read--\n");
    printf("\033[2J");
    printf("\033[1;1H");

    for(j = 0; j < height; j++)
    {

        sleep(1);
        // map 2d array to 1d
        row[height * j + randomColumn] = 2;

        row[height * (j - 1) + randomColumn] = 0;

    }

    sleep(1);

    return NULL;

}