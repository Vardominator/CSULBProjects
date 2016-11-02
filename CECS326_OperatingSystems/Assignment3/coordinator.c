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
#include <pthread.h>
#include <time.h>

#include <signal.h>

#define SHSIZE 100

typedef int bool;
#define true 1
#define false 0

void initializeSwimMill(int *row, int width, int height);
void showSwimMill(int *row, int width, int height);

static volatile int keepRunning = 1;
static double totalTime = 0;
void intHandler(int dummy);

// Function for pellet creation thread
void *createPellets();

int main (char argc, char *argv[])
{

    srand(time(NULL));

    int shmid;
    int width = 10, height = 10;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    pthread_t pelletThread;

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

    int returnIDFish = fork();

    if(returnIDFish == 0)
    {
        execv("fishreader", argv);
    }

    // Initialize array
    initializeSwimMill(row, width, height);

    int ret1;
    ret1 = pthread_create(&pelletThread, NULL, createPellets, NULL);

    // print the swim mill
    while(keepRunning && totalTime < 31)
    {
        signal(SIGINT, intHandler);    
        showSwimMill(row, width, height);    
        sleep(1);
        totalTime += 1;
    }

    pthread_cancel(pelletThread);

    //shmdt(row);

    shmctl(shmid, IPC_RMID, NULL);

    return 0;
}

void intHandler(int dummy)
{
    keepRunning = 0;
}

void *createPellets()
{

    int time = 0;
    int width = 10;

    char scanLine[1024];

    while(1)
    {
        
        if(time % 3 == 0)
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

        }

        
        time += 1;
        sleep(1);

    }

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
    printf("%f\n", totalTime);

}