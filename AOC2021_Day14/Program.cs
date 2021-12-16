using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day14
{

    public class PolymerNode
    {
        static int depth = 0;
        string code;
        PolymerNode left, right;
        string newElement;
        Dictionary<int, Dictionary<char, UInt64>> completedValues = new Dictionary<int, Dictionary<char, ulong>>();
        

        public PolymerNode Left { get => left; set => left = value; }
        public PolymerNode Right { get => right; set => right = value; }
        public string NewElement { get => newElement; set => newElement = value; }
        public string Code { get => code; set => code = value; }

        public PolymerNode(string code, string newElement)
        {
            Code = code;
            NewElement = newElement;
            TryNewLeft();
            TryNewRight();
        }

        private void TryNewRight()
        {
            string rightTry = NewElement + Code[1];
            if (Program.PolymerPairs.ContainsKey(rightTry))
            {
                Right = Program.PolymerPairs[rightTry];
            }
        }

        private void TryNewLeft()
        {
            string leftTry = Code[0] + NewElement;
            if (Program.PolymerPairs.ContainsKey(leftTry))
            {
                Left = Program.PolymerPairs[leftTry];
            }
        }

        public Dictionary<char, ulong> Expand(int limit)
        {
            depth++;
            if (completedValues.ContainsKey(depth))
            {
                foreach (char element in completedValues[depth].Keys)
                {
                    Program.elementCounter[element] += completedValues[depth][element];
                }
            }
            else
            {
                completedValues[depth] = new Dictionary<char, ulong>();
                Program.AddElement(NewElement[0]);
                AddElement(NewElement[0], depth);
                if (Right == null)
                {
                    TryNewRight();
                }
                if (right != null)
                {
                    if (depth < limit)
                    {
                        
                        updateCounts(depth, Right.Expand(limit));
                    }

                }

                if (Left == null)
                {
                    TryNewLeft();
                }
                if (Left != null)
                {
                    if (depth < limit)
                    {
                        updateCounts(depth, Left.Expand(limit));
                    }
                }
            }
            depth--;
            return completedValues[depth + 1];
        }

        private void updateCounts(int depth, Dictionary<char, ulong> elementCounts)
        {
            foreach (char element in elementCounts.Keys)
            {
                AddElement(element, depth, elementCounts[element]);
            }
        }
        public void AddElement(char element, int depth)
        {
            AddElement(element, depth, 1);
        }

        public void AddElement(char element, int depth, UInt64 amount )
        {
           
                if (completedValues[depth].ContainsKey(element))
                {
                    completedValues[depth][element] += amount;
                }
                else
                {
                    completedValues[depth].Add(element, 1);
                }
           
        }
    }
    class Program
    {
        public static Dictionary<char, UInt64> elementCounter = new Dictionary<char, UInt64>();
        public static Dictionary<string, PolymerNode> PolymerPairs = new Dictionary<string, PolymerNode>();
        static void Main(string[] args)
        {
            
            
            
            string[] Puzzleinput = File.ReadAllLines("Input.txt");

            string BigChain = Puzzleinput[0];

            foreach (char element in BigChain)
            {
                AddElement(element);
            }

            for (int inputCount = 2; inputCount < Puzzleinput.Length; inputCount++)
            {
                PolymerPairs.Add(Puzzleinput[inputCount].Split(" -> ")[0], new PolymerNode(Puzzleinput[inputCount].Split(" -> ")[0], Puzzleinput[inputCount].Split(" -> ")[1]));
            }

            for (int chainCount = 0; chainCount < BigChain.Length - 1; chainCount++)
            {
                PolymerPairs[BigChain.Substring(chainCount, 2)].Expand(2);
            }

            /*
            for (int inputCount = 2; inputCount < Puzzleinput.Length; inputCount++)
            {
                PolymerPairs.Add(Puzzleinput[inputCount].Split(" -> ")[0], Puzzleinput[inputCount].Split(" -> ")[1]);
            }


            
           Queue<string> WorkList = new Queue<string>();

           foreach (char element in BigChain)
           {
               AddElement( element);
           }

           for (int chainCount = 0; chainCount < BigChain.Length - 1; chainCount++)
           {
               WorkList.Enqueue(BigChain.Substring(chainCount, 2) + ":1");
           }

           while (WorkList.Count > 0)
           {
               string pairEntry = WorkList.Dequeue();
               string pair = pairEntry.Split(":")[0];
               int level = Convert.ToInt32(pairEntry.Split(":")[1]);
               if(PolymerPairs.ContainsKey(pair))
               {
                   AddElement(PolymerPairs[pair][0]);
                   if (level < 40)
                   {
                       WorkList.Enqueue(pair[0] + PolymerPairs[pair] + ":" + (level + 1));
                       WorkList.Enqueue(PolymerPairs[pair] + pair[1] + ":" + (level + 1));
                   }
               }
           }
           */

            /*
            string Chain = "";

            for (int chainCount = 0; chainCount < BigChain.Length - 1; chainCount++)
            {
                Chain = BigChain.Substring(chainCount, 2);

                for (int stepCount = 0; stepCount < 40; stepCount++)
                {
                    Console.WriteLine("Step " + stepCount);
                    string newChain = Convert.ToString(Chain[0]);
                    for (int elementCount = 0; elementCount < Chain.Length - 1; elementCount++)
                    {
                        if (PolymerPairs.ContainsKey(Chain.Substring(elementCount, 2)))
                        {
                            newChain += PolymerPairs[Chain.Substring(elementCount, 2)];
                            newChain += Chain[elementCount + 1];
                        }
                    }
                    Chain = newChain;
                }

                foreach (char element in Chain)
                {
                    if (elementCounter.ContainsKey(element))
                    {
                        elementCounter[element]++;
                    }
                    else
                    {
                        elementCounter.Add(element, 1);
                    }
                }
            }
           */

            UInt64 min = 0;
            UInt64 max = 0;

            foreach (char element in elementCounter.Keys)
            {
                if(min == 0)
                {
                    min = elementCounter[element];
                }
                else
                {
                    if (min > elementCounter[element])
                    {
                        min = elementCounter[element];
                    }
                }
                if (max == 0)
                {
                    max = elementCounter[element];
                }
                else
                {
                    if (max < elementCounter[element])
                    {
                        max = elementCounter[element];
                    }
                }
            }
            Console.WriteLine(max - min);

        }

        public static void AddElement(char element)
        {
            if (elementCounter.ContainsKey(element))
            {
                elementCounter[element]++;
            }
            else
            {
                elementCounter.Add(element, 1);
            }
        }
    }
}
