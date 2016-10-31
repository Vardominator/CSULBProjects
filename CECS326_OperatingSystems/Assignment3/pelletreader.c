#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>

#define SHSIZE 100

void dropPellet(int *row, int width, int height, int randomColumn);

int main(char argc, char * argv[])
{

    int shmid;
    int width = 10, height = 10;
    int total = width * height;
    int i = 0, j = 0;
    int position = 0;

    key_t key = 9876;

    shmid = shmget(key, SHSIZE, 0666);

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

    // while(1)
    // {
    //     sleep(1);
    //     printf("\033[2J");
    //     printf("\033[1;1H");

    //     for(i = 0; i < width; i++)
    //     {
    //         for(j = 0; j < height; j++)
    //         {
    //             // map 2d array to 1d
                
    //             printf("%d  ", row[i * width + j]);
    //         }
    //         printf("\n");
    //     }

    // }

    int column = atoi(argv[1]);

    dropPellet(row, width, height, column);
    shmdt(row);

    return 0;
}

void dropPellet(int *row, int width, int height, int randomColumn)
{

    int i = 0, j = 0;

    for(j = 0; j < height; j++)
    {
        
        sleep(1);
        // map 2d array to 1d
        row[height * j + randomColumn] = 2;

        if(j > 0)
        {
            row[height * (j - 1) + randomColumn] = 0;
        }

    }

    

    sleep(1);

}