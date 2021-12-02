using System;
using System.IO;

namespace AOC2021_Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Directions = File.ReadAllLines("Input.txt");
            int Horizontal = 0;
            
            int Depth = 0;
            int AimDepth = 0;
            foreach (string direction in Directions)
            {
                string command = direction.Split(' ')[0];
                int distance = Convert.ToInt32(direction.Split(' ')[1]);

                switch (command)
                {
                    case "forward":
                        Horizontal += distance;
                        AimDepth += distance * Depth;
                        break;

                    case "up":
                        Depth -= distance;
                        break;

                    case "down":
                        Depth += distance;
                        break;
                }


            }
            int total = Depth * Horizontal;
            Console.WriteLine(total);
            Console.WriteLine(AimDepth * Horizontal);
        }
    }
}
