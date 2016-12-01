#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// malloc() and free() are used for allocating memory and releasing the
// allocated memory

int main(int argt, char *argv[])
{

    // variable that holds the address of the allocated memory
    char *ptr;

    // allocate memory
    ptr = (char *)malloc(24);

    if(ptr == NULL)
    {
        printf("Failed to get or allocate memory!\n");
        exit(1);
    }

    strcpy(ptr, "hello there!");

    printf("ptr: %s\n", ptr);

    // release memory
    free(ptr);

    return 0;

}