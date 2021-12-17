using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020_Day15
{

    class Point
    {
        int xPos;
        int yPos;
        int threat;
        int cost;

        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public int Threat { get => threat; set => threat = value; }
        public int Cost { get => cost; set => cost = value; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] Puzzleinput = File.ReadAllLines("Input.txt");
            List<Point> WorkList = new List<Point>();
            List<Point> SortedList = new List<Point>();

            int dim = Puzzleinput.Length * 5;

            //int[,] Grid = new int[Puzzleinput.Length * 5, Puzzleinput.Length];

            for (int xCount = 0; xCount < dim; xCount++)
            {
                for (int yCount = 0; yCount < dim; yCount++)
                {
                    int xpos = xCount % Puzzleinput.Length;
                    int ypos = yCount % Puzzleinput.Length;
                    /*Grid[xCount, yCount]*/ int val  = Convert.ToInt32(Convert.ToString(Puzzleinput[ypos][xpos]));
                    val += xCount / Puzzleinput.Length + yCount / Puzzleinput.Length;
                    if (val > 9)
                    {
                        val -= 9;
                    }
                    Point newPoint = new Point() { XPos = xCount, YPos = yCount, Threat = val, Cost = int.MaxValue };
                    
                    if(xCount == 0 && yCount == 0)
                    {
                        newPoint.Cost = 0;
                    }
                    WorkList.Add(newPoint);
                }
            }
            bool Found = false;
            while (!Found)
            {
                if(WorkList.Count % 1000 == 0)
                {
                    Console.WriteLine(SortedList.Count + " completed " + WorkList.Count + " to go");
                }
                WorkList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                Point currentPoint = WorkList[0];
                WorkList.RemoveAt(0);
                if (currentPoint.XPos == dim - 1 && currentPoint.YPos == dim - 1)
                {
                    Found = true;
                }
                else
                {
                    Point ProcessPoint = null;
                    if (currentPoint.XPos > 0)
                    {
                        ProcessPoint = getPoint(currentPoint.XPos - 1, currentPoint.YPos, WorkList);
                        if (ProcessPoint != null)
                        {
                            if (ProcessPoint.Cost == int.MaxValue || currentPoint.Cost + ProcessPoint.Threat < ProcessPoint.Cost)
                            {
                                ProcessPoint.Cost = currentPoint.Cost + ProcessPoint.Threat;
                            }
                        }
                    }

                    if (currentPoint.YPos > 0)
                    {
                        ProcessPoint = getPoint(currentPoint.XPos, currentPoint.YPos - 1, WorkList);
                        if (ProcessPoint != null)
                        {
                            if (ProcessPoint.Cost == int.MaxValue || currentPoint.Cost + ProcessPoint.Threat < ProcessPoint.Cost)
                            {
                                ProcessPoint.Cost = currentPoint.Cost + ProcessPoint.Threat;
                            }
                        }
                    }


                    if (currentPoint.XPos < dim - 1)
                    {
                        ProcessPoint = getPoint(currentPoint.XPos + 1, currentPoint.YPos, WorkList);
                        if (ProcessPoint != null)
                        {
                            if (ProcessPoint.Cost == int.MaxValue || currentPoint.Cost + ProcessPoint.Threat < ProcessPoint.Cost)
                            {
                                ProcessPoint.Cost = currentPoint.Cost + ProcessPoint.Threat;
                            }
                        }
                    }

                    if (currentPoint.YPos < dim - 1)
                    {
                        ProcessPoint = getPoint(currentPoint.XPos, currentPoint.YPos + 1, WorkList);
                        if (ProcessPoint != null)
                        {
                            if (ProcessPoint.Cost == int.MaxValue || currentPoint.Cost + ProcessPoint.Threat < ProcessPoint.Cost)
                            {
                                ProcessPoint.Cost = currentPoint.Cost + ProcessPoint.Threat;
                            }
                        }
                    }
                    
                }
               /* foreach (Point point in WorkList)
                {
                    
                    if (point.Cost == int.MaxValue)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(point.XPos * 2, point.YPos);
                        Console.Write(point.Threat);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(point.XPos * 2, point.YPos);
                        Console.Write(point.Cost);
                    }
                }
                foreach (Point point in SortedList)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(point.XPos * 2, point.YPos);
                    Console.Write(point.Cost);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(currentPoint.XPos * 2, currentPoint.YPos);
                Console.Write(currentPoint.Cost);*/
                SortedList.Add(currentPoint);
               //Console.ReadLine();
            }

            Point FinalPoint = getPoint(dim - 1, dim - 1, SortedList);
            Console.WriteLine(FinalPoint.Cost);
        }

        private static Point getPoint(int xPos, int yPos, List<Point> findList)
        {
            return findList.Find((x) => x.XPos == xPos && x.YPos == yPos);
        }
    }
}
