using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Puzzleinput = File.ReadAllLines("Input.txt");
            List<string> LowPoints = new List<string>();
            List<int> Basins = new List<int>();



            int[,] Heights = new int[Puzzleinput.Length, Puzzleinput[0].Length];
            for (int rowCount = 0; rowCount < Puzzleinput.Length; rowCount++)
            {
                for (int colCount = 0; colCount < Puzzleinput[rowCount].Length; colCount++)
                {
                    Heights[rowCount, colCount] = Convert.ToInt32(Convert.ToString(Puzzleinput[rowCount][colCount]));
                }
            }
            int totalRisk = 0;
            for (int rowCount = 0; rowCount < Puzzleinput.Length; rowCount++)
            {
                for (int colCount = 0; colCount < Puzzleinput[rowCount].Length; colCount++)
                {
                   if(rowCount == 0 || Heights[rowCount, colCount] < Heights[rowCount - 1, colCount])
                    {
                        if (rowCount == Puzzleinput.Length - 1 || Heights[rowCount, colCount] < Heights[rowCount + 1, colCount])
                        {
                            if (colCount == 0 || Heights[rowCount, colCount] < Heights[rowCount, colCount - 1])
                            {
                                if (colCount == Puzzleinput[0].Length - 1 || Heights[rowCount, colCount] < Heights[rowCount, colCount + 1])
                                {
                                    totalRisk += (Heights[rowCount, colCount] + 1);
                                    LowPoints.Add(Convert.ToString(rowCount) + "," + Convert.ToString(colCount));
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(totalRisk);

            foreach (string low in LowPoints)
            {
                
                Basins.Add(GetBasin(low, Heights));
            }

            Basins.Sort();
            Console.WriteLine(Basins[Basins.Count - 1] * Basins[Basins.Count - 2] * Basins[Basins.Count - 3]);
            
        }

        private static int GetBasin(string low, int[,] heights)
        {
            
            Queue<string> SearchList = new Queue<string>();
            List<string> BasinPoints = new List<string>();

            SearchList.Enqueue(low);
            BasinPoints.Add(low);

            while (SearchList.Count > 0)
            {
                string searchPoint = SearchList.Dequeue();
             

                int XPos = Convert.ToInt32(searchPoint.Split(",")[0]);
                int YPos = Convert.ToInt32(searchPoint.Split(",")[1]);

                if (XPos > 0 && heights[XPos - 1, YPos] != 9)
                {
                    string newPoint = Convert.ToString(XPos - 1) + "," + Convert.ToString(YPos);
                    if (!BasinPoints.Contains(newPoint))
                    {
                        SearchList.Enqueue(newPoint);
                        BasinPoints.Add(newPoint);
                    }
                }
                if (YPos > 0 && heights[XPos, YPos - 1] != 9)
                {
                    string newPoint = Convert.ToString(XPos) + "," + Convert.ToString(YPos - 1);
                    if (!BasinPoints.Contains(newPoint))
                    {
                        SearchList.Enqueue(newPoint);
                        BasinPoints.Add(newPoint);
                    }
                }
                if (XPos < heights.GetLength(0) - 1 && heights[XPos + 1, YPos] != 9)
                {
                    string newPoint = Convert.ToString(XPos + 1) + "," + Convert.ToString(YPos);
                    if (!BasinPoints.Contains(newPoint))
                    {
                        SearchList.Enqueue(newPoint);
                        BasinPoints.Add(newPoint);
                    }
                }
                if (YPos < heights.GetLength(1) - 1 && heights[XPos, YPos + 1] != 9)
                {
                    string newPoint = Convert.ToString(XPos) + "," + Convert.ToString(YPos + 1);
                    if (!BasinPoints.Contains(newPoint))
                    {
                        SearchList.Enqueue(newPoint);
                        BasinPoints.Add(newPoint);
                    }
                }

            }


            return BasinPoints.Count; ;
        }
    }
}
