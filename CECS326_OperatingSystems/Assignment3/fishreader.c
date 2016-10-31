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

    
    // move fish left and right
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
    
    shmdt(row);

    return 0;
}