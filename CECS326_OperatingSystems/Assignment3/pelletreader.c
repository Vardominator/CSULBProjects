#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>

#define SHSIZE 100

void dropPellet(int *row, int width, int height, int randomColumn, int randomRow);

static int PelletId = 0;

int main(char argc, char * argv[])
{

    int shmid;
    int width = 10, height = 10;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    key_t key = 9876;

    shmid = shmget(key, SHSIZE, 0666);
    PelletId = shmid;

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

    int column = atoi(argv[1]);
    int rowPel = atoi(argv[2]);

    dropPellet(row, width, height, column, rowPel);
    
    shmdt(row);

    return 0;
}

void dropPellet(int *row, int width, int height, int randomColumn, int randomRow)
{

    int i = 0, j = randomRow;

    for(j; j <= height; j++)
    {
        
        sleep(1);
        // map 2d array to 1d

        if(row[height * j + randomColumn] == 1)
        {
            printf("Pellet eaten. Pellet ID: %d\n", PelletId);
        }

        row[height * j + randomColumn] = 2;

        if(j > 0)
        {
            row[height * (j - 1) + randomColumn] = 0;
        }

    }


    
    sleep(1);

}