#include <pthread.h>
#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>


static void *child(void *ignored)
{

  while(1)
  {
    printf("child thread sleeping...\n");
    sleep(5);
  } 

  return NULL;
 
}

int main(int argc, char *argv[])
{
 
  pthread_t child_thread;

  if(pthread_create(&child_thread, NULL, child, NULL))
  {

    fprintf(stderr, "pthread_create failed with code\n");
    return 1;
   
  }
  
  while(1)
  {

    char c = getchar();
    if(c == '\n')
    {
      
      pthread_cancel(child_thread);
      printf("child thread cancelled\n");
      break;

    }
  }


  //sleep(2);
  printf("Parent is done sleeping.\n");
 
  return 0;

}