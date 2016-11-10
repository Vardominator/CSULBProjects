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

// Shared memory size. 100 is not necessary but gives a nice cushion
#define SHSIZE 100

// Define boolean type because C is so outdated
typedef int bool;
#define true 1
#define false 0

// Initializes the swim mill by setting all values in the array to 0
void initializeSwimMill(int *row, int width, int height);
// Print the swim mill every 500 milliseconds
void showSwimMill(int *row, int width, int height);
// Initialize the array that holds the pellet ids
void initializePelletIds();
// Loop through the pellet id array to kill all lingering pellet processes
void killPellets();

// An indicator that changes to 0 if ^C is pressed
static volatile int keepRunning = 1;
// Method that triggers keepRunning if ^C is pressed
void intHandler(int dummy);

// Total time that the swim mill runs
static double totalTime = 0;

// Function for pellet creation thread
void *createPellets();

// Array to hold the pellet ids that were return in the pellet creation thread
static int pelletIds[50];
// Used to increment the position in the above array everytime a pellet is created
static int pelletCount = 0;


static FILE *eatenIdsFile;


int main (char argc, char *argv[])
{

    // Set random seed
    srand(time(NULL));

    int shmid;
    int width = 10, height = 10;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    // Pellet creator thread
    pthread_t pelletThread;

    // Create memory segment
    key_t key = 9876;
    shmid = shmget(key, SHSIZE, IPC_CREAT | 0666);

    // Create txt file to log pellet that were eaten by fish
    eatenIdsFile = fopen("eatenPellets.txt", "w");
    // Close that file for now; pellet processes will write to it
    fclose(eatenIdsFile);

    if(shmid < 0)
    {
        perror("shmget");
        return 1;
    }

    // Map memory array to memory segment
    int *row = shmat(shmid, NULL, 0);
    if(row < (int *) NULL)
    {
        perror("shmat");
        return 1;
    }

    // Launch the fish process
    int returnIDFish = fork();

    if(returnIDFish == 0)
    {
        execv("fish", argv);
        _exit(0);
    }

    // Initialize array
    initializeSwimMill(row, width, height);

    // Launch pellet creator thread
    int ret1;
    ret1 = pthread_create(&pelletThread, NULL, createPellets, NULL);

    // Print the swim milland check for cancellation input every 500 milliseconds up to 30 seconds
    while(keepRunning && totalTime != 61)
    {
        signal(SIGINT, intHandler);    
        showSwimMill(row, width, height);    
        usleep(1000 * 500);
        totalTime += 1;
    }

    // Kill all pellet processes
    killPellets();
    printf("\nPellet processes cleaned...\n");

    // Cancel the thread that trigers the pellet processes
    pthread_cancel(pelletThread);
    printf("\nPellet creator thread cancelled...\n");

    // Kill the fish process
    kill(returnIDFish, SIGKILL);
    printf("\nFish process cleaned...\n");

    // Destroy shared memory segment
    shmctl(shmid, IPC_RMID, NULL);
    printf("\nShared memory segment cleared...\n\n");

    // Read pellet ids that were eaten
    printf("\nThe following pellets were eaten:\n");

    FILE * pelletIdsFile = fopen("eatenPellets.txt", "r");
    char *line = NULL;
    ssize_t read;
    size_t len = 0;

    // Read the pellets ID file line by line and print results
    while((read = getline(&line, &len, pelletIdsFile)) != -1)
    {
        printf("%s", line);
    }

    // Close the pellets ID file
    fclose(pelletIdsFile);
    printf("\nFile closed...\n\n");

    // Success (Borat voice)
    printf("EXIT SUCCES\n\n");

    return 0;
}

void intHandler(int dummy)
{
    keepRunning = 0;
}

void initializePelletIds()
{
    int i = 0;

    for(i = 0; i < 50; i++)
    {
        pelletIds[i] = -1;
    }
}

void killPellets()
{
    int i = 0;
    for(i = 0; i < pelletCount; i++)
    {
        kill(pelletIds[i], SIGKILL);
    }
}

void *createPellets()
{

    int time = 0;
    int width = 10, height = 10;

    while(1)
    {
        
        // Create pellet every second
        if(time % 1 == 0)
        {

            // Launch a pellet process
            int returnIDPellet = fork();

            // Create messages buffers for random initial pellet position
            char randWidthBuffer[20];
            char randHeightBuffer[20];

            // Place random values in the those buffers
            snprintf(randWidthBuffer, 20, "%d", rand() % width + 1);     // random width
            snprintf(randHeightBuffer, 20, "%d", rand() % height + 1);   // random height

            // Argument buffer to pass to pellet process
            char *argsToPellet[4];
            argsToPellet[0] = "coordinator";
            argsToPellet[1] = randWidthBuffer;
            argsToPellet[2] = randHeightBuffer;
            argsToPellet[3] = NULL;

            if(returnIDPellet == 0)
            {
                execv("pellet", argsToPellet);
                _exit(0);
            }

            // Add pellet id to pellet id array
            pelletIds[pelletCount] = returnIDPellet;
            // Increment pellet count
            pelletCount++;

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

    // Clear screen and move cursor to top left position of terminal
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