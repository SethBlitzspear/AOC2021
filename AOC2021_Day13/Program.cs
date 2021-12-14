using System;
using System.IO;

namespace AOC2021_Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] Grid;
            string[] Puzzleinput = File.ReadAllLines("Input.txt");

            int max = 0;
            foreach (string line in Puzzleinput)
            {
                if (line != "" && line[0] != 'f')
                {
                    if (max < Convert.ToInt32(line.Split(",")[0]))
                    {
                        max = Convert.ToInt32(line.Split(",")[0]);
                    }
                    if (max < Convert.ToInt32(line.Split(",")[1]))
                    {
                        max = Convert.ToInt32(line.Split(",")[1]);
                    }
                }
            }
            max++;

            Grid = new char[max, max];
            foreach (string line in Puzzleinput)
            {
                if (line != "" && line[0] != 'f')
                {
                    int x = Convert.ToInt32(line.Split(",")[0]);
                    int y = Convert.ToInt32(line.Split(",")[1]);
                    Grid[x, y] = '*';
                }
                else if (line == "")
                {

                }
                else
                {
                    int foldLine = Convert.ToInt32(line.Split("=")[1]);
                  
                        Fold(Grid, line[11], foldLine);
                    
                    int dotCount = 0;
                    for (int xCount = 0; xCount < max; xCount++)
                    {
                        for (int yCount = 0; yCount < max; yCount++)
                        {
                            if(Grid[xCount, yCount] == '*')
                            {
                                dotCount++;
                            }
                        }
                    }
                    Console.WriteLine(dotCount);
                }
            }

            int xmin = -1, ymin = -1, xmax = max, ymax = max;

            int xpos = 0;
            while (xmin == -1)
            {
                for (int ycount = 0; ycount < max; ycount++)
                {
                    if(Grid[xpos, ycount] == '*')
                    {
                        xmin = xpos;
                    }
                }
                xpos++;
            }
            int ypos = 0;
            while (ymin == -1)
            {
                for (int xcount = 0; xcount < max; xcount++)
                {
                    if (Grid[xcount, ypos] == '*')
                    {
                        ymin = ypos;
                    }
                }
                ypos++;
            }
            xpos = max - 1;
            while (xmax == max)
            {
                for (int ycount = 0; ycount < max; ycount++)
                {
                    if (Grid[xpos, ycount] == '*')
                    {
                        xmax = xpos;
                    }
                }
                xpos--;
            }
            ypos = max - 1;
            while (ymax == max)
            {
                for (int xcount = 0; xcount < max; xcount++)
                {
                    if (Grid[xcount, ypos] == '*')
                    {
                        ymax = ypos;
                    }
                }
                ypos--;
            }

            for (int xCount = xmin; xCount <= xmax; xCount++)
            {
                for (int yCount = xmin; yCount <= ymax; yCount++)
                {
                    if (Grid[xCount, yCount] == '*')
                    {
                        Console.Write(Grid[xCount, yCount]);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Fold(char[,] Grid, char dim, int foldLine)
        {
            int max = Grid.GetLength(0);

            for (int xCount = dim == 'x' ? foldLine + 1 : 0; xCount < max; xCount++)
            {
                for (int yCount = dim == 'y'? foldLine + 1:0; yCount < max; yCount++)
                {
                    if (Grid[xCount, yCount] == '*')
                    {
                        Grid[dim == 'x' ? foldLine - (xCount - foldLine) : xCount, dim == 'y' ? foldLine - (yCount - foldLine) : yCount] = '*';
                        Grid[xCount, yCount] = ' ';
                    }
                }
            }
        }
    }
}
