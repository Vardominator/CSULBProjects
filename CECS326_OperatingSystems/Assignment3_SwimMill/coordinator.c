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

static FILE *eatenIdsFile;

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

    eatenIdsFile = fopen("eatenPellets.txt", "w");
    fclose(eatenIdsFile);

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
        _exit(0);
    }

    // Initialize array
    initializeSwimMill(row, width, height);

    int ret1;
    ret1 = pthread_create(&pelletThread, NULL, createPellets, NULL);

    // print the swim mill
    while(keepRunning && totalTime != 61)
    {
        signal(SIGINT, intHandler);    
        showSwimMill(row, width, height);    
        //sleep(1);
        usleep(1000 * 500);
        totalTime += 1;
    }

    pthread_cancel(pelletThread);

    // TODO: MANUALLY CANCEL FISH AND PELLET

    // Destroy shared memory segment
    shmctl(shmid, IPC_RMID, NULL);


    // Read pellet ids that were eaten
    printf("\n\n\nThe following pellets were eaten:\n\n");

    FILE * pelletIdsFile = fopen("eatenPellets.txt", "r");
    char *line = NULL;
    ssize_t read;
    size_t len = 0;

    while((read = getline(&line, &len, pelletIdsFile)) != -1)
    {
        printf("%s", line);
    }

    fclose(pelletIdsFile);


    return 0;
}

void intHandler(int dummy)
{
    keepRunning = 0;
}

void *createPellets()
{

    int time = 0;
    int width = 10, height = 10;

    char scanLine[1024];

    while(1)
    {
        
        if(time % 2 == 0)
        {

            int returnIDPellet = fork();
            char randWidthBuffer[20];
            char randHeightBuffer[20];
            snprintf(randWidthBuffer, 20, "%d", rand() % width + 1);     // random width
            snprintf(randHeightBuffer, 20, "%d", rand() % height + 1);   // random height
            char *argsToPellet[4];
            argsToPellet[0] = "coordinator";
            argsToPellet[1] = randWidthBuffer;
            argsToPellet[2] = randHeightBuffer;
            argsToPellet[3] = NULL;

            if(returnIDPellet == 0)
            {
                execv("pelletreader", argsToPellet);
                _exit(0);
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
    printf("\nTime elapsed: %d\n", (int)(totalTime/2));

}