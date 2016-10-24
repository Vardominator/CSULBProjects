#include <stdio.h>
#include <unistd.h>

// The purpose of fork() is to create a new process, which becomes the child
//  process of the caller. After a new child process is created, both processes
//  will execute the next instruction following the fork() system call. 
//  Therefore we have to distinguish the parent from the child.

int main (int argc, char *argv[])
{

    int childpid;
    int count1 = 0;
    int count2 = 0;

    printf("Before it forks!\n");

    childpid = fork();

    // The parent process will get the process id information of the child
    // but the child will not get information about its process id.
    if(childpid == 0)
    {

        printf("This is a child process\n");

        while(count1 < 10)
        {
            printf("Child process: %d\n", count1);
            sleep(1);
            count1++;

        }

    }
    else
    {

        while(count2 < 20)
        {
            printf("Parent process: %d\n", count2);
            sleep(1);
            count2++;

        }

    }

    return 0;

}