using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Puzzleinput = File.ReadAllLines("Input.txt");
            int grandTotal = 0;
            int uniqueCount = 0;
            foreach (string puzzleLine in Puzzleinput)
            {
                char T = ' ', TR = ' ', TL = ' ', M = ' ', BR = ' ', BL = ' ', B = ' ';
                string two = "", three = "", four = "", seven = "";
                List<string> fives = new List<string>(), six = new List<string>();
                string check = puzzleLine.Split(" | ")[0];
                string[] segmentCheck = check.Split();
                foreach (string segment in segmentCheck)
                {

                    if (segment.Length == 2 || segment.Length == 3 || segment.Length == 4 || segment.Length == 7)
                    {
                        uniqueCount++;
                    }

                    if (segment.Length == 2)
                    {
                        two = segment;
                    }
                    if (segment.Length == 3)
                    {
                        three = segment;
                    }
                    if (segment.Length == 4)
                    {
                        four = segment;
                    }
                    if (segment.Length == 7)
                    {
                        seven = segment;
                    }
                    if (segment.Length == 5)
                    {
                        fives.Add(segment);
                    }
                    if (segment.Length == 6)
                    {
                        six.Add(segment);
                    }
                }

                foreach (char letter in three)
                {
                    if (!two.Contains(letter))
                    {
                        T = letter;
                    }
                }

                string lets = "abcdefg";

                // Remove top
                lets = lets.Replace(T, ' ');

                // Now remove anything not in a five segment
                for (int letCount = 0; letCount < 7; letCount++)
                {
                    foreach (string five in fives)
                    {
                        if (!five.Contains(lets[letCount]))
                        {
                            lets = lets.Replace(lets[letCount], ' ');
                        }
                    }
                }
                

                char[] MB = new char[2];
                int MBindex = 0;

                foreach (char let in lets)
                {
                    if(let != ' ')
                    {
                        MB[MBindex++] = let;
                    }
                }

                if (four.Contains(MB[0]))
                {
                    M = MB[0];
                    B = MB[1];
                }
                else
                {
                    B = MB[0];
                    M = MB[1];
                }

                string findTL = four;
                findTL = findTL.Remove(findTL.IndexOf(M), 1);
                findTL = findTL.Remove(findTL.IndexOf(two[0]), 1);
                findTL = findTL.Remove(findTL.IndexOf(two[1]), 1);

                TL = findTL[0];

                foreach (string five in fives)
                {
                    if (five.Contains(TL))
                    {
                        string findBR = five;
                        findBR = findBR.Remove(findBR.IndexOf(TL), 1);
                        findBR = findBR.Remove(findBR.IndexOf(T), 1);
                        findBR = findBR.Remove(findBR.IndexOf(M), 1);
                        findBR = findBR.Remove(findBR.IndexOf(B), 1);

                        BR = findBR[0];

                    }
                }

                foreach (string five in fives)
                {
                    if (five.Contains(BR) && !five.Contains(TL))
                    {
                        string findTR = five;

                        findTR = findTR.Remove(findTR.IndexOf(BR), 1);
                        findTR = findTR.Remove(findTR.IndexOf(T), 1);
                        findTR = findTR.Remove(findTR.IndexOf(M), 1);
                        findTR = findTR.Remove(findTR.IndexOf(B), 1);

                        TR = findTR[0];

                    }
                }

                //Last one
                string findBL = "abcdefg";
                findBL = findBL.Remove(findBL.IndexOf(BR), 1);
                findBL = findBL.Remove(findBL.IndexOf(TR), 1);
                findBL = findBL.Remove(findBL.IndexOf(TL), 1);
                findBL = findBL.Remove(findBL.IndexOf(T), 1);
                findBL = findBL.Remove(findBL.IndexOf(M), 1);
                findBL = findBL.Remove(findBL.IndexOf(B), 1);

                BL = findBL[0];


                string output = puzzleLine.Split(" | ")[1];
                string[] segmentOutput = output.Split();

                string totalStr = "";

                totalStr += findSegVal(segmentOutput[0], T, M, B, TR, TL, BR, BL);
                totalStr += findSegVal(segmentOutput[1], T, M, B, TR, TL, BR, BL);
                totalStr += findSegVal(segmentOutput[2], T, M, B, TR, TL, BR, BL);
                totalStr += findSegVal(segmentOutput[3], T, M, B, TR, TL, BR, BL);

                int total = Convert.ToInt32(totalStr);

                grandTotal += total;
                Console.WriteLine(total);
            }

            Console.WriteLine(uniqueCount);
            Console.WriteLine(grandTotal);
        }

        static string findSegVal(string find, char T, char M, char B, char TR, char TL, char BR, char BL)
        {
            if(find.Contains(T) && !find.Contains(M) && find.Contains(B) && find.Contains(TR) && find.Contains(TL) && find.Contains(BR) && find.Contains(BL))
            {
                return "0";
            }
            if (!find.Contains(T) && !find.Contains(M) && !find.Contains(B) && find.Contains(TR) && !find.Contains(TL) && find.Contains(BR) && !find.Contains(BL))
            {
                return "1";
            }
            if (find.Contains(T) && find.Contains(M) && find.Contains(B) && find.Contains(TR) && !find.Contains(TL) && !find.Contains(BR) && find.Contains(BL))
            {
                return "2";
            }
            if (find.Contains(T) && find.Contains(M) && find.Contains(B) && find.Contains(TR) && !find.Contains(TL) && find.Contains(BR) && !find.Contains(BL))
            {
                return "3";
            }
            if (!find.Contains(T) && find.Contains(M) && !find.Contains(B) && find.Contains(TR) && find.Contains(TL) && find.Contains(BR) && !find.Contains(BL))
            {
                return "4";
            }
            if (find.Contains(T) && find.Contains(M) && find.Contains(B) && !find.Contains(TR) && find.Contains(TL) && find.Contains(BR) && !find.Contains(BL))
            {
                return "5";
            }
            if (find.Contains(T) && find.Contains(M) && find.Contains(B) && !find.Contains(TR) && find.Contains(TL) && find.Contains(BR) && find.Contains(BL))
            {
                return "6";
            }
            if (find.Contains(T) && !find.Contains(M) && !find.Contains(B) && find.Contains(TR) && !find.Contains(TL) && find.Contains(BR) && !find.Contains(BL))
            {
                return "7";
            }
            if (find.Contains(T) && find.Contains(M) && find.Contains(B) && find.Contains(TR) && find.Contains(TL) && find.Contains(BR) && find.Contains(BL))
            {
                return "8";
            }
            if (find.Contains(T) && find.Contains(M) && find.Contains(B) && find.Contains(TR) && find.Contains(TL) && find.Contains(BR) && !find.Contains(BL))
            {
                return "9";
            }
            return " ";

        }
    }
}
