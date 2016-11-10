#include <stdio.h>
#include <unistd.h>


int main(int argc, char *argv[])
{

    // buffer
    char buf[10];

    int ret;

    
    while(1)
    {
        // 0 is the standard input from keyboard
        ret = read(0, buf, 10);

        // if number of bytes read is less than the buffer size
        if(ret < 10)
        {
            buf[ret] = '\0';
            printf("%s\n", buf);
            break;
        }

        printf("%s\n", buf);
    
    }

    return 0;

}