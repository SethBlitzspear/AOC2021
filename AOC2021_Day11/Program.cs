using System;
using System.IO;

namespace AOC2021_Day11
{
    class Program
    {
        static int[,] Grid;
        static int BoomCount = 0;
        static int dim;
        static bool allFlash = false;
        static void Main(string[] args)
        {
            string[] Puzzleinput = File.ReadAllLines("Input.txt");
            dim = Puzzleinput.Length;
            Grid = new int[dim, dim];

            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    Grid[x, y] = Convert.ToInt32(Convert.ToString(Puzzleinput[x][y]));
                }
            }


            int stepCount = 0;
            //for (int stepCount = 0; stepCount < 100; stepCount++)
            while(!allFlash)
            {
                if(stepCount == 194)
                {
                    int stop = 1;
                }
                allFlash = true;
                for (int x = 0; x < dim; x++)
                {
                    for (int y = 0; y < dim; y++)
                    {
                        AddOne(x, y);
                    }
                }

                for (int x = 0; x < dim; x++)
                {
                    for (int y = 0; y < dim; y++)
                    {
                        if (Grid[x, y] == -1)
                        {
                            Grid[x, y] = 0;
                        }
                        else
                        {
                            allFlash = false;
                        }
                    }
                }
                stepCount++;
                
                for (int x = 0; x < dim; x++)
                {
                    for (int y = 0; y < dim; y++)
                    {

                        if (Grid[x, y] == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(Grid[x, y]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                

            }

            Console.WriteLine(BoomCount);
            Console.WriteLine(stepCount);
           

        }

        private static void AddOne(int x, int y)
        {
            if (x >= 0 && x < dim && y >= 0 && y < dim)
            {
                if (Grid[x, y] != -1)
                {
                    Grid[x, y]++;
                    if (Grid[x, y] == 10)
                    {
                        BoomCount++;
                        Grid[x, y] = -1;
                        AddOne(x - 1, y - 1);
                        AddOne(x - 1, y);
                        AddOne(x - 1, y + 1);
                        AddOne(x, y - 1);
                        AddOne(x, y + 1);
                        AddOne(x + 1, y - 1);
                        AddOne(x + 1, y);
                        AddOne(x + 1, y + 1);
                    }

                }
            }
        }
    }
}
