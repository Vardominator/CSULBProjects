using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string s = "1 2 3 4 5 6 7 8 9";
            string t = "2   4   5 6";

            string[] result = missingWords(s, t);

            string[] subs = buildSubsequences("abc");

        }

        public static string[] missingWords(string s, string t)
        {


            string[] allWords = s.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string[] nonMissingWords = t.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> missingWords = new List<string>();

            int allWordsCounter = 0;
            int nonMissingCounter = 0;

            while(nonMissingCounter < nonMissingWords.Length)
            {
                if(nonMissingWords[nonMissingCounter] != allWords[allWordsCounter])
                {
                    missingWords.Add(allWords[allWordsCounter]);
                    allWordsCounter++;
                }
                else
                {
                    nonMissingCounter++;
                    allWordsCounter++;
                }

           }

            for (int i = allWordsCounter; i < allWords.Length; i++)
            {
                missingWords.Add(allWords[i]);
            }

            return missingWords.ToArray();

        }


        public static string[] buildSubsequences(string s)
        {

            List<string> substrings = new List<string>();

            string currentString = s;

            for (int k = 0; k < s.Length; k++)
            {

                
                for (int i = 0; i <= currentString.Length; i++)
                {
                    for (int j = i + 1; j <= currentString.Length; j++)
                    {
                        substrings.Add(currentString.Substring(i, j - i));
                    }
                }

                char lastChar = currentString[s.Length - 1];

                currentString = lastChar + currentString.Substring(1, currentString.Length - 1);

            }

            return substrings.ToArray();

        }


    }
}
