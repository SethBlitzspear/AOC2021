using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021_Day20
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] Puzzleinput = File.ReadAllLines("Input.txt");

            char[,] Pixels = new char[Puzzleinput.Length - 2, Puzzleinput[2].Length];

            string ImageEnhancer = Puzzleinput[0];

            for (int rowCount = 2; rowCount < Puzzleinput.Length; rowCount++)
            {
                string line = Puzzleinput[rowCount];
                for (int colCount = 0; colCount < line.Length; colCount++)
                {
                    Pixels[rowCount - 2, colCount] = line[colCount];
                }
            }
            char voidSpace = '.';
            Console.SetWindowSize(86  , 86);
            DisplayImage(Pixels);
            Console.WriteLine(CountPixels(Pixels));
            
            for (int enhanceCount = 0; enhanceCount < 50; enhanceCount++)
            {
                char[,] ImageOutput = new char[Pixels.GetLength(0) + 2, Pixels.GetLength(1) + 2];

                for (int rowCount = 0; rowCount < ImageOutput.GetLength(0); rowCount++)
                {
                    for (int colCount = 0; colCount < ImageOutput.GetLength(1); colCount++)
                    {
                        string focus = "";
                        for (int focusRow = -1; focusRow < 2; focusRow++)
                        {
                            for (int focusCol = -1; focusCol < 2; focusCol++)
                            {
                                try
                                {
                                    focus += Pixels[rowCount + focusRow - 1, colCount + focusCol - 1];
                                }
                                catch (Exception e)
                                {
                                    focus += voidSpace;
                                }

                            }
                        }
                        focus = focus.Replace('#', '1');
                        focus = focus.Replace('.', '0');
                        int pixelOffSet = Convert.ToInt32(focus, 2);
                        ImageOutput[rowCount, colCount] = ImageEnhancer[pixelOffSet];
                    }
                }
                Pixels = ImageOutput;

                if (voidSpace == '.')
                {
                    voidSpace = '#';
                }
                else
                {
                    voidSpace = '.';
                }

                DisplayImage(Pixels); 
                Console.WriteLine(CountPixels(Pixels));
            }
            

            
            Console.ReadLine();
        }

        private static int CountPixels(char[,] Pixels)
        {
            int pixelCount = 0;
            for (int rowCount = 0; rowCount < Pixels.GetLength(0); rowCount++)
            {
                for (int colCount = 0; colCount < Pixels.GetLength(1); colCount++)
                {
                    if (Pixels[rowCount, colCount] == '#')
                    {
                        pixelCount++;
                    }
                }
            }

            return pixelCount;
        }

        private static void DisplayImage(char[,] Pixels)
        {
            /*
            for (int rowCount = 0; rowCount < Pixels.GetLength(0); rowCount++)
            {
                string display = "";
                for (int colCount = 0; colCount < Pixels.GetLength(1); colCount++)
                {
                    display += Pixels[rowCount, colCount];
                }
                Console.WriteLine(display);
            }
            Console.WriteLine();
            */
        }
    }
}
