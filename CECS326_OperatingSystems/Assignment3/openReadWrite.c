#include <stdio.h>

// required headers for file stuff
#include <sys/types.h>
#include <fcntl.h>
#include <unistd.h>

int main(int argc, char *argv[])
{

    int fd;

    //fd = open("myfile.txt", O_CREAT | O_WRONGLY, 0600);
    open("myfile.txt");

    if(fd == -1)
    {

        printf("Failed to create and open the file.\n");
        exit(1);
    }

    close(fd);

    return 0;
}
