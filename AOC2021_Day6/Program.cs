using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day6
{

    class LanternFish
    {
        private int age;

        public int Age { get => age; set => age = value; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string Puzzleinput = "1,1,1,1,1,1,1,4,1,2,1,1,4,1,1,1,5,1,1,1,1,1,1,1,1,1,1,1,1,5,1,1,1,1,3,1,1,2,1,2,1,3,3,4,1,4,1,1,3,1,1,5,1,1,1,1,4,1,1,5,1,1,1,4,1,5,1,1,1,3,1,1,5,3,1,1,1,1,1,4,1,1,1,1,1,2,4,1,1,1,1,4,1,2,2,1,1,1,3,1,2,5,1,4,1,1,1,3,1,1,4,1,1,1,1,1,1,1,4,1,1,4,1,1,1,1,1,1,1,2,1,1,5,1,1,1,4,1,1,5,1,1,5,3,3,5,3,1,1,1,4,1,1,1,1,1,1,5,3,1,2,1,1,1,4,1,3,1,5,1,1,2,1,1,1,1,1,5,1,1,1,1,1,2,1,1,1,1,4,3,2,1,2,4,1,3,1,5,1,2,1,4,1,1,1,1,1,3,1,4,1,1,1,1,3,1,3,3,1,4,3,4,1,1,1,1,5,1,3,3,2,5,3,1,1,3,1,3,1,1,1,1,4,1,1,1,1,3,1,5,1,1,1,4,4,1,1,5,5,2,4,5,1,1,1,1,5,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,5,1,1,1,1,1,1,3,1,1,2,1,1";

            UInt64[] FishDays = new UInt64[9];

            string[] startingAges = Puzzleinput.Split(",");
            foreach (string startingFish in startingAges)
            {

                FishDays[Convert.ToInt32(startingFish)]++;

            }
            for (int dayCount = 0; dayCount < 256; dayCount++)
            {
                UInt64 newFishes = FishDays[0];
                FishDays[0] = FishDays[1];
                FishDays[1] = FishDays[2];
                FishDays[2] = FishDays[3];
                FishDays[3] = FishDays[4];
                FishDays[4] = FishDays[5];
                FishDays[5] = FishDays[6];
                FishDays[6] = FishDays[7];
                FishDays[7] = FishDays[8];

                FishDays[8] = newFishes;
                FishDays[6] += newFishes;


            }
            Console.WriteLine(FishDays[0] + FishDays[1] + FishDays[2] + FishDays[3] + FishDays[4] + FishDays[5] + FishDays[6] + FishDays[7] + FishDays[8]);

            //string Puzzleinput = "3,4,3,1,2";
            /*
            List<LanternFish> LanterFishies = new List<LanternFish>();
            string[] startingAges = Puzzleinput.Split(",");
            foreach (string startingFish in startingAges)
            {

                    LanterFishies.Add(new LanternFish() { Age = Convert.ToInt32(startingFish) });

            }

            for (int dayCount = 0; dayCount < 256; dayCount++)
            {
                List<LanternFish> newFish = new List<LanternFish>();
                foreach (LanternFish Fish in LanterFishies)
                {
                    Fish.Age--;
                    if(Fish.Age < 0)
                    {
                        newFish.Add(new LanternFish { Age = 8 });
                        Fish.Age = 6;
                    }
                }
                LanterFishies.AddRange(newFish);

               Console.WriteLine("After " + dayCount + "days ");
                /*    foreach (LanternFish Fish in LanterFishies)
                   {
                       Console.Write(Fish.Age + ",");
                   }
                   Console.WriteLine();
            }

            Console.WriteLine(LanterFishies.Count); */
        }
    }
}
