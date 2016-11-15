#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <unistd.h>

#define SHSIZE 100

// Drops pellet until it reaches the bottom row of the array
void dropPellet(int *row, int width, int height, int randomColumn, int randomRow);

static int pelletId = 0;

// File pointer used to write pellet id to if pellet is eaten by fish
static FILE *eatenIdsFile;

int main(char argc, char * argv[])
{

    int shmid;
    int width = 10, height = 10;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    key_t key = 9876;

    shmid = shmget(key, SHSIZE, 0666);
    pelletId = getpid();
    eatenIdsFile = fopen("eatenPellets.txt", "a");

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

    // Read arguments from coordinator process (random initial position for pellet)
    int column = atoi(argv[1]);
    int rowPel = atoi(argv[2]);

    // Start dropping pellet
    dropPellet(row, width, height, column, rowPel);

    // Detach from memory
    shmdt(row);

    fclose(eatenIdsFile);

    return 0;
}

void dropPellet(int *row, int width, int height, int randomColumn, int randomRow)
{

    int i = 0, j = randomRow;

    for(j; j <= height; j++)
    {
        
        //sleep(1);
        usleep(1000 * 500);

        // map 2d array to 1d
        if(row[height * j + randomColumn] == 1)
        {
            fprintf(eatenIdsFile, "%d\n", pelletId);
        }

        row[height * j + randomColumn] = 2;

        if(j > 0)
        {
            row[height * (j - 1) + randomColumn] = 0;
        }

    }

    usleep(1000 * 500);

}