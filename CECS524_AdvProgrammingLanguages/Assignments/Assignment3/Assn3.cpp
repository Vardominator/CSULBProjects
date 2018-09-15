#include <iostream>
#include <fstream>
#include <algorithm>
#include <string>
#include <regex>

using namespace std;

int main(int argc, char *argv[])
{
    regex hexDecPattern ("0[xX](([0-9a-fA-F])*)(\\.([0-9a-fA-F])*)?(p|P)(\\+|-|$)(\\d\\d*)((f|F)|(l|L)|$)");
    ifstream file(argv[1]);
    string str;
    while (getline(file, str))
    {
        str.erase(remove(str.begin(), str.end(), '\r'), str.end());
        if(regex_match(str, hexDecPattern))
        {
            cout << "Matched: " << str << endl;
        }
        else
        {
            cout << "Not Matched: " << str << endl;
        }
    }
    return 0;
}