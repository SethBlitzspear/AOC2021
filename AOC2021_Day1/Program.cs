using System;
using System.IO;

namespace AOC2021_Day1
{
    class Program
    {
       
        static void Main(string[] args)
        {
            int increaseTotal = 0;
            int threeMeasureTotal = 0;
            string[] measuresString = File.ReadAllLines("Input.txt");
            int[] measures = new int[measuresString.Length];
            int[] threemeasure = new int[measuresString.Length - 2];
            for (int measureCount = 0; measureCount < measuresString.Length; measureCount++)
            {
                measures[measureCount] = Convert.ToInt32(measuresString[measureCount]);
          
                if(measureCount> 0)
                {
                    if(measures[measureCount] > measures[measureCount-1])
                    {
                        increaseTotal++;
                    }
                }
            }

            for (int measureCount = 0; measureCount < threemeasure.Length; measureCount++)
            {
              
                    threemeasure[measureCount] = measures[measureCount] + measures[measureCount + 1] + measures[measureCount + 2];
                if (measureCount > 0)
                {
                    if (threemeasure[measureCount] > threemeasure[measureCount - 1])
                    {
                        threeMeasureTotal++;
                    }
                }

            }
            
            Console.WriteLine(increaseTotal);
            Console.WriteLine(threeMeasureTotal);
        }
    }
}
