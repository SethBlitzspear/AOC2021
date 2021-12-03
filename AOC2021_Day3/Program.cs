using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Binary = File.ReadAllLines("Bin.txt");

            List<string> OxyList = new List<string>(Binary);
            List<string> CO2List = new List<string>(Binary);

            string gamma = "";
            string epsilon = "";

            int gammaCount = 0;
            int epsilonCount = 0;

            for (int bitCount = 0; bitCount < Binary[0].Length; bitCount++)
            {
                gammaCount = 0;
                epsilonCount = 0;

                foreach (string binary in Binary)
                {
                    if(binary[bitCount] == '1')
                    {
                        gammaCount++;
                    }
                    else
                    {
                        epsilonCount++;
                    }
                }
                if (gammaCount > epsilonCount)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }

               

            }
            int gammaInt = Convert.ToInt32(gamma, 2);
            int epsilonInt = Convert.ToInt32(epsilon, 2);

            Console.WriteLine(gammaInt * epsilonInt);

            int CO2 = FindVal(CO2List, "0");
            int Oxy = FindVal(OxyList, "1");

            Console.WriteLine(CO2 * Oxy);

        }
        static private int FindVal(List<string> searchList, string searchBit)
        {
            int count1 = 0, count0 = 0;
            int bitCount = 0;
            while (searchList.Count > 1)
            {
                count1 = count0 = 0;
                foreach (string binary in searchList) 
                {
                    if(binary[bitCount] == '1')
                    {
                        count1++;
                    }
                    else
                    {
                        count0++;
                    }
                }

                List<string> newList = new List<string>();
                if(searchBit == "1")
                {
                    if (count1 >= count0)
                    {
                        foreach (string binary in searchList)
                        {
                            if(binary[bitCount] == '1')
                            {
                                newList.Add(binary);
                            }
                        }
                    }
                    else
                    {
                        foreach (string binary in searchList)
                        {
                            if (binary[bitCount] == '0')
                            {
                                newList.Add(binary);
                            }
                        }
                    }
                }
                else
                {
                    if(count0 <= count1)
                    {
                        foreach (string binary in searchList)
                        {
                            if (binary[bitCount] == '0')
                            {
                                newList.Add(binary);
                            }
                        }
                    }
                    else
                    {
                        foreach (string binary in searchList)
                        {
                            if (binary[bitCount] == '1')
                            {
                                newList.Add(binary);
                            }
                        }
                    }
                }

                bitCount++;
                searchList = newList;
            }
            return Convert.ToInt32(searchList[0], 2);
    
        }
    }

   
}
