using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> winningBoard = new List<int>();
            string[] PuzzleLines = File.ReadAllLines("Input.txt");
            int[,,] Boards = new int[(PuzzleLines.Length - 1) / 6, 5, 5];

            for (int bingoCount = 2; bingoCount < PuzzleLines.Length; bingoCount++)
            {
                if((bingoCount - 1) % 6 != 0)
                {
                    string[] Numbers = PuzzleLines[bingoCount].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (int numbersCount = 0; numbersCount < Numbers.Length; numbersCount++)
                    {
                        Boards[(bingoCount - 1) / 6, ((bingoCount - 1) % 6) - 1, numbersCount] = Convert.ToInt32(Numbers[numbersCount]);
                    }
                }
            }

            string[] BingoCalls = PuzzleLines[0].Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int callCount = 0; callCount< BingoCalls.Length; callCount++)
            {

                int Number = Convert.ToInt32(BingoCalls[callCount]);
                for (int boardCount = 0; boardCount < Boards.GetLength(0); boardCount++)
                {
                    for (int xCount = 0; xCount < 5; xCount++)
                    {
                        for (int yCount = 0; yCount < 5; yCount++)
                        {
                            if (Boards[boardCount, xCount, yCount] == Number)
                            {
                                Boards[boardCount, xCount, yCount] = -1;
                            }
                        }
                    }
                }

                //Check Winner
                for (int boardCount = 0; boardCount < Boards.GetLength(0); boardCount++)
                {
                    if (!winningBoard.Contains(boardCount))
                    {
                        for (int xCount = 0; xCount < 5; xCount++)
                        {
                            bool xWin = true;
                            bool yWin = true;
                            for (int yCount = 0; yCount < 5; yCount++)
                            {
                                if (Boards[boardCount, xCount, yCount] != -1)
                                {
                                    yWin = false;
                                }
                                if (Boards[boardCount, yCount, xCount] != -1)
                                {
                                    xWin = false;
                                }
                            }
                            if (xWin || yWin)
                            {
                                int total = 0;
                                for (int xWinCount = 0; xWinCount < 5; xWinCount++)
                                {
                                    for (int yWinCount = 0; yWinCount < 5; yWinCount++)
                                    {
                                        if (Boards[boardCount, xWinCount, yWinCount] != -1)
                                        {
                                            total += Boards[boardCount, xWinCount, yWinCount];
                                        }
                                    }
                                }
                                Console.WriteLine("Board " + boardCount + " : " + total * Number);
                                
                                winningBoard.Add(boardCount);
                                //Console.ReadLine();
                            }
                        }
                    }
                }
            }
        }
    }
}
