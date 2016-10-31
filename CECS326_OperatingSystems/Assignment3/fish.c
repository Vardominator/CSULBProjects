#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <pthread.h>


#define SHSIZE 100

typedef int bool;
#define true 1
#define false 0


// thread method to move fish left and right
void *moveFish(int * fishArray);

int main(char argc, char *argv[])
{

    int shmid;
    int count = 5;
    int i = 0;
    int *fishPosition;
    int SizeMem;

    key_t key = 9876;


    // Create shared memory segment
    shmid = shmget(key, SHSIZE, IPC_CREAT | 0666);

    if(shmid < 0)
    {
        perror("shmget");
        return 1;
    }


    // map the segment into the address space
    fishPosition = shmat(shmid, NULL, 0);

    if(fishPosition < (int *) NULL)
    {
        perror("shmat");
        return 1;
    }

    for(i = 0; i < count; i++)
    {
        fishPosition[i] = 0;
    }

    for(i = 0; i < count; i++)
    {
        printf("\n%d---\n", fishPosition[i]);
    }


    printf("\nWriting to memory successful--\n");
    printf("\033[2J");
    printf("\033[1;1H");


    // move fish left and right
    bool right = true;
    int currentposition = count/2;
    int lastPosition = currentposition - 1;

    while(1)
    {
        
        printf("Waiting to be read...\n");
        sleep(1);
        printf("\033[2J");
        printf("\033[1;1H");

        currentposition += right ? 1: -1;

        
        fishPosition[currentposition] = 1;
        
        if(right == true)
        {
            fishPosition[currentposition - 1] = 0;
        }
        else
        {
            fishPosition[currentposition + 1] = 0;
        }

        if(currentposition == count - 1 && right == true)
        {
            right = false;
        }
        else if(currentposition == 0 && right == false)
        {
            right = true;
        }

        lastPosition = currentposition;

        printf("\n");

    }

    // detach from memory segment
    shmdt(fishPosition);


    return 0;
}

