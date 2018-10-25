#include <iostream>
#include <cstdlib>

void printMatrix(int *matrix, int row, int col);


void matmul(int* mat1, int* mat2, int* result, int r1, int c1, int c2){
    for(int i = 0; i < r1; i++)
    {
        for(int j = 0; j < c2; j++)
        {
            int sum = 0;
            for(int k = 0; k < c2; k++)
            {
                sum = sum + mat1[i * c1 + k] * mat2[k * c2 + j];
            }
            result[i * c2 + j] = sum;
        }
    }
}

int main(int argc, char *argv[]){
    int r1 = atoi(argv[1]);
    int c1 = atoi(argv[2]);
    int r2 = atoi(argv[3]);
    int c2 = atoi(argv[4]);

    if (c1 != r2)
    {
        std::cout << "Cannot perform multiplication. The column size of matrix 1 must equal the row size of matrix 2." << std::endl;
        return 0;
    }

    int *mat1 = new int[r1 * c1];
    int *mat2 = new int[r2 * c2];
    int *result = new int[r1 * c2];

    // INITIALIZE
    for(int i = 0; i < r1 * c1; i++)
    {
        mat1[i] = i + 1;
    }
    for(int j = 0; j < r2 * c2; j++){
        mat2[j] = r2 * c2 - j;
    }
    std::cout << "Matrix 1" << std::endl;
    printMatrix(mat1, r1, c1);
    std::cout << "Matrix 2" << std::endl;
    printMatrix(mat2, r2, c2);

    // PERFORM MULTIPLICATION
    matmul(mat1, mat2, result, r1, c1, c2);
    // for(int i = 0; i < r1; i++)
    // {
    //     for(int j = 0; j < c2; j++)
    //     {
    //         int sum = 0;
    //         for(int k = 0; k < c2; k++)
    //         {
    //             sum = sum + mat1[i * c1 + k] * mat2[k * c2 + j];
    //         }
    //         result[i * c2 + j] = sum;
    //     }
    // }

    std::cout << "Matrix 1 * Matrix 2" << std::endl;
    printMatrix(result, r1, c2);

    return 0;
}

void printMatrix(int *matrix, int row, int col)
{
    for(int i = 0; i < row*col; i++){
        std::cout << matrix[i] << " ";
        if((i + 1) % row == 0)
        {
            std::cout << std::endl;
        }
    }
}