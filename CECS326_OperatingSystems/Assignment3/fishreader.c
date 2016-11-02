#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>

#define SHSIZE 100

typedef int bool;
#define true 1
#define false 0

void swimBackAndForth(int *row, int width, int height);

int findPellet(int *row, int width, int height);

int main(char argc, char *argv[])
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

    // attach to shared memory segment
    int *row = shmat(shmid, NULL, 0);

    if(row < (int *) NULL)
    {
        perror("shmat");
        return 1;
    }


    // place fish in the middle
    int currentPosition = 0;
    row[height * (height - 1)] = 1;

    while(1)
    {

        sleep(1);

        // check if there is a closer pellet
        int pelletPosition = findPellet(row, width, height);
        int pelletColumn = pelletPosition % height;

        if(currentPosition < pelletColumn)
        {
            row[height * (height - 1) + currentPosition] = 0;
            currentPosition += 1;
        }
        else if (currentPosition > pelletColumn)
        {
            row[height * (height - 1) + currentPosition] = 0;
            currentPosition -= 1;
        }

        row[height * (height - 1) + currentPosition] = 1;

    }

    shmdt(row);

    return 0;
}


int findPellet(int *row, int width, int height)
{

    while(1)
    {

        int i = 0, j = 0;

        for(i = width - 1; i >= 0; i--)
        {
            for(j = height; j >= 0; j--)
            {
                if(row[i * width + j] == 2)
                {
                    return i * width + j;
                }
            }
        }
    }

}

void swimBackAndForth(int *row, int width, int height)
{
    // Testing only; fish just swims left and right

    bool right = true;
    int currentposition = width/2;
    int lastPosition = currentposition - 1;

    while(1)
    {

        sleep(1);

        currentposition += right ? 1: -1;
        
        row[height * (height - 1) + currentposition] = 1;
        
        if(right == true)
        {
            row[height * (height - 1) + (currentposition - 1)] = 0;
        }
        else
        {
            row[height * (height - 1) + (currentposition + 1)] = 0;
        }

        if(currentposition == width - 1 && right == true)
        {
            right = false;
        }
        else if(currentposition == 0 && right == false)
        {
            right = true;
        }

        lastPosition = currentposition;

    }

}