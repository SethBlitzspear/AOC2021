using System;
using System.IO;

namespace AOC2021_Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 1000;
            string[] Puzzleinput = File.ReadAllLines("Input.txt");

            int[,] VentGrid = new int[size, size];

            foreach (string vent in Puzzleinput)
            {
                string Coords1 = vent.Split(" -> ")[0];
                string Coords2 = vent.Split(" -> ")[1];

                int x1 = Convert.ToInt32(Coords1.Split(",")[0]);
                int x2 = Convert.ToInt32(Coords2.Split(",")[0]);
                int y1 = Convert.ToInt32(Coords1.Split(",")[1]);
                int y2 = Convert.ToInt32(Coords2.Split(",")[1]);

                if(y1 == y2)
                {
                    for (int xCount = Math.Min(x1, x2); xCount <= Math.Max(x1, x2); xCount++)
                    {
                        VentGrid[xCount, y1]++;
                    }
                }
                else if (x1 == x2)
                {
                    for (int yCount = Math.Min(y1, y2); yCount <= Math.Max(y1, y2); yCount++)
                    {
                        VentGrid[x1, yCount]++;
                    }
                }
                else
                {
                    
                    int offsetX = 0;
                    int offsetY = 0;
                    for (int xCount = Math.Min(x1, x2); xCount <= Math.Max(x1, x2); xCount++)
                    {
                        VentGrid[x1 + offsetX, y1 + offsetY]++;
                        if(x1 > x2)
                        {
                            offsetX--;
                        }
                        else
                        {
                            offsetX++;
                        }
                        if (y1 > y2)
                        {
                            offsetY--;
                        }
                        else
                        {
                            offsetY++;
                        }
                    }
                }

            }
            int total = 0;
            for (int xCount = 0; xCount < size; xCount++)
            {
                for (int yCount = 0; yCount < size; yCount++)
                {
                    if( VentGrid[xCount, yCount] > 1)
                    {
                        total++;
                    }
                }
            }
            Console.WriteLine(total);
            if (size == 10)
            {
                for (int xCount = 0; xCount < size; xCount++)
                {
                    for (int yCount = 0; yCount < size; yCount++)
                    {
                        Console.Write(VentGrid[xCount, yCount]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
