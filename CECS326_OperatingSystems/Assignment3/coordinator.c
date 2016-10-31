// fork() makes a duplicate of the current process
// execv() replaces the duplicated parent process with a new process

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <pthread.h>
#include <time.h>
#include <unistd.h>

#define SHSIZE 100

typedef int bool;
#define true 1
#define false 0

void initializeSwimMill(int *row, int width, int height);
void showSwimMill(int *row, int width, int height);

int main (char argc, char *argv[])
{

    srand(time(NULL));

    int shmid;
    int width = 10, height = 10;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    // random position for the pellet
    int r = rand() % width;
    char *myArg[10];
    char sprintf_buffer[10];
    sprintf(sprintf_buffer, "%d", r);
    myArg[1] = sprintf_buffer;

    // create memory segment
    key_t key = 9876;
    shmid = shmget(key, SHSIZE, IPC_CREAT | 0666);
    if(shmid < 0)
    {
        perror("shmget");
        return 1;
    }

    // map memory array to memory segment
    int *row = shmat(shmid, NULL, 0);
    if(row < (int *) NULL)
    {
        perror("shmat");
        return 1;
    }

    // Initialize array
    initializeSwimMill(row, width, height);

    int returnIDFish = fork();

    if(returnIDFish == 0)
    {
        execv("fishreader", argv);
    }

    // Launch pellet process with random position
    

    // TODO: Run this loop in a separate thread
    for(int i = 0; i < 5; i++)
    {

        int returnIDPellet = fork();
        char numberArgBuffer[20];
        snprintf(numberArgBuffer, 20, "%d", rand() % width);
        char *argsToPellet[3];
        argsToPellet[0] = "coordinator";
        argsToPellet[1] = numberArgBuffer;
        argsToPellet[2] = NULL;

        if(returnIDPellet == 0)
        {
            execv("pelletreader", argsToPellet);
        }

        sleep(2);

    }

    sleep(1);

    // print the swim mill
    showSwimMill(row, width, height);

    shmdt(row);

    return 0;
}

void initializeSwimMill(int * row, int width, int height)
{
    int i = 0, j = 0;

    // initialize mill
    for(i = 0; i < width; i++)
    {
        for(j = 0; j < height; j++)
        {
            row[i * width + j] = 0;
        }
    }
}

void showSwimMill(int * row, int width, int height)
{

    int i = 0, j = 0;

    while(1)
    {
        sleep(1);
        printf("\033[2J");
        printf("\033[1;1H");

        for(i = 0; i < width; i++)
        {
            for(j = 0; j < height; j++)
            {
                printf("%d  ", row[i * width + j]);
            }
            printf("\n");
        }

    }
}