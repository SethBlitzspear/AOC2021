using System;

namespace AOC2021_Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            //string PuzzleInput = "target area: x=20..30, y=-10..-5";
            string PuzzleInput = "target area: x=201..230, y=-99..-65";
            string coords = PuzzleInput.Split("x=")[1];
            string xcoords = coords.Split(", y=")[0];
            string ycoords = coords.Split(", y=")[1];
            int xMin = Convert.ToInt32(xcoords.Split("..")[0]);
            int xMax = Convert.ToInt32(xcoords.Split("..")[1]);
            int yMin = Convert.ToInt32(ycoords.Split("..")[0]);
            int yMax = Convert.ToInt32(ycoords.Split("..")[1]);

           
            int top = 0;
            int hitCount = 0;

            for (int x = 0; x < 2000; x++)
            {
                for (int y = yMin; y < 2000; y++)
                {
                    //Console.WriteLine("trying " + x + ", " + y);
                    int xVel = x, yVel = y;
                    int xPos = 0, yPos = 0;
                    int step = 0;
                    int tempTop = 0;
                    bool notHit = true;
                    while(xPos < xMax && yPos > yMin && notHit)
                    {
                        notHit = true;
                        step++;
                        yPos += yVel;
                        yVel -= 1;
                        xPos += xVel;
                        if (xVel > 0)
                        {
                            xVel -= 1;
                        }
                        else if (xVel < 0)
                        {
                            xVel += 1;
                        }
                        if (yPos > tempTop)
                        {
                            tempTop = yPos;
                        }
                        if (xPos >= xMin && xPos <= xMax)
                        {
                            if(yPos >= yMin && yPos <= yMax)
                            {
                                Console.WriteLine( x + ", " + y);
                                hitCount++;
                                notHit = false;
                                if (tempTop > top)
                                {
                                    top = tempTop;
                                }
                            }
                        }
                    }
                   // Console.WriteLine("Finishing after step count hit" + step);
                }
            }
            Console.WriteLine(top);
            Console.WriteLine(hitCount);
        }
    }
}
