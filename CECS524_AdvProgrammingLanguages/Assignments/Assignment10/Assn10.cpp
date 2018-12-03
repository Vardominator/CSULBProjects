/*

CECS 524 Assignment 10

Author: Varderes Barsegyan

Date of Completion: 12/3/2018

*/

#include <iostream>
#include <string>
#include <vector>

using namespace std;

void substring(vector<vector<string>> &combinedPartitions, string &numStr, int numStrLength, vector<string> &placeHolder, int leftCursor)
{
    // FIND ALL PARTITIONS OF A STRING RECURSIVELY
    vector<string> currPlaceHolder = placeHolder;
    string combString;

    if(leftCursor == 0)
    {
        placeHolder.clear();
    }
    for (int i = leftCursor; i < numStrLength; i++)
    {
        combString = combString + numStr[i];
        placeHolder.push_back(combString);
        if (i + 1 < numStrLength)
        {
            substring(combinedPartitions, numStr, numStrLength, placeHolder, i + 1);
        }
        else
        {
            combinedPartitions.push_back(placeHolder);
        }
        placeHolder = currPlaceHolder;
    }
}

void summations(int sum, int correctSum, int position, vector<string> partitions, vector<vector<string>> &correctPartitions, vector<char> plusMinus, vector<vector<char>> &correctPlusMinus)
{   
    // PERFORM A RECURSIVE SUMMATION GOING LEFT TO ADD AND RIGHT TO SUBTRACT
    if(position == partitions.size())
    {
        if(sum == correctSum)
        {
            correctPartitions.push_back(partitions);
            correctPlusMinus.push_back(plusMinus);
        }
        return;
    }

    int left = sum + stoi(partitions[position]);
    int right = sum - stoi(partitions[position]);
    plusMinus.push_back('+');
    summations(left, correctSum, position + 1, partitions, correctPartitions, plusMinus, correctPlusMinus);
    plusMinus.pop_back();
    plusMinus.push_back('-');
    summations(right, correctSum, position + 1, partitions, correctPartitions, plusMinus, correctPlusMinus);
}

int main(int argc, char *argv[])
{
    int num = stoi(argv[1]);
    int correctSum = stoi(argv[2]);
    std::string numStr = std::to_string(num);
    int strLength = numStr.length();

    // FIND ALL PARTITIONS
    vector<vector<string>> combinedPartitions;
    vector<string> placeholder;
    substring(combinedPartitions, numStr, strLength, placeholder, 0);

    vector<vector<string>> correctPartitions;
    vector<vector<char>> correctPlusMinus;

    for (int i = 0; i < combinedPartitions.size(); i++)
    {
        int sum = 0;
        vector<char> leftPlusMinus;
        leftPlusMinus.push_back('+');
        summations(stoi(combinedPartitions[i][0]), correctSum, 1, combinedPartitions[i], correctPartitions, leftPlusMinus, correctPlusMinus);
        vector<char> rightPlusMinus;
        rightPlusMinus.push_back('-');        
        summations(-1*stoi(combinedPartitions[i][0]), correctSum, 1, combinedPartitions[i], correctPartitions, rightPlusMinus, correctPlusMinus);
    }

    int count = 1;
    for (int i = 0; i < correctPartitions.size(); i++)
    {
        cout << count << " : " << correctPartitions[i][0];
        for (int j = 1; j < correctPartitions[i].size(); j++)
        {
            cout << correctPlusMinus[i][j];
            cout << correctPartitions[i][j];
        }
        cout << "=" << correctSum << endl;
        count += 1;
    }

    return 0;
}