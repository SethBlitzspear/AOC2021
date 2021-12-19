using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day18
{
    public class SnailFishNumber
    {
        int value = -1;
        string number;
        SnailFishNumber left, right, parent;

        public SnailFishNumber()
        {
        }

        public SnailFishNumber(string line, SnailFishNumber newParent) : this (line)
        {
            Parent = newParent;
        }

        public SnailFishNumber(string line)
        {
            Number = line;
            if(!int.TryParse(line, out value))
            {
                line = line.Substring(1, line.Length - 2);
                int commaFinder = 0;
                int bracketDepth = 0;
                while(line[commaFinder] != ',' || bracketDepth != 0)
                {
                    if (line[commaFinder] == '[')
                    {
                        bracketDepth++;
                    }
                    if (line[commaFinder++] == ']')
                    {
                        bracketDepth--;
                    }
                }
                string leftNumber = line.Substring(0, commaFinder);
                string rightNumber = line.Substring(commaFinder + 1, line.Length - (commaFinder + 1));
                Left = new SnailFishNumber(leftNumber, this);
                Right = new SnailFishNumber(rightNumber, this);
                Value = -1;
            }
        }

        public int Value { get => value; set => this.value = value; }
        public SnailFishNumber Left { get => left; set => left = value; }
        public SnailFishNumber Right { get => right; set => right = value; }
        public string Number { get => number; set => number = value; }
        public SnailFishNumber Parent { get => parent; set => parent = value; }

        public void AddExplodedValue(int value, bool AddRight, SnailFishNumber sender)
        {
            if (AddRight)
            {
                if (sender == Right)
                {
                    if(Parent != null)
                    {
                        Parent.AddExplodedValue(value, AddRight, this);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (sender == Left)
                {
                    if (Right.Value != -1)
                    {
                        Right.Value += value;
                    }
                    else
                    {
                        Right.AddExplodedValue(value, AddRight, this);
                    }
                }
                else
                {
                    if (Left.Value != -1)
                    {
                        Left.Value += value;
                    }
                    else
                    {
                        Left.AddExplodedValue(value, AddRight, this);
                    }
                }
            }
            else
            {
                if (sender == Left)
                {
                    if (Parent != null)
                    {
                        Parent.AddExplodedValue(value, AddRight, this);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (sender == Right)
                {
                    if (Left.Value != -1)
                    {
                        Left.Value += value;
                    }
                    else
                    {
                        Left.AddExplodedValue(value, AddRight, this);
                    }
                }
                else
                {
                    if (Right.Value != -1)
                    {
                        Right.Value += value;
                    }
                    else
                    {
                        Right.AddExplodedValue(value, AddRight, this);
                    }
                }
            }
        }
        public bool Explode(int level)
        {
            if (level == 4 && Value == -1)
            {

                Parent.AddExplodedValue(Left.Value, false, this);
                Parent.AddExplodedValue(Right.Value, true, this);
                Right = Left = null;
                Value = 0;
                return false;

            }
            
            else if (left != null)
            {
                if (Left.Explode(level + 1))
                {
                    if (Right != null)
                    {
                        return Right.Explode(level + 1);
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (Right != null)
            {
                return Right.Explode(level + 1);
            }
            else
            {
                return true;
            }
            return true;

        }

        public bool Split()
        {
           
            if (Value > 9)
            {
                int leftVal, rightVal;
                if (Value % 2 == 0)
                {
                    leftVal = rightVal = Value / 2;
                }
                else
                {
                    leftVal = Value / 2;
                    rightVal = Value / 2 + 1;
                }
                Value = -1;
                Left = new SnailFishNumber(Convert.ToString(leftVal), this);
                Right = new SnailFishNumber(Convert.ToString(rightVal), this);

                return false;
            }
            else if (left != null)
            {
                if (Left.Split())
                {
                    if (Right != null)
                    {
                        return Right.Split();
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (Right != null)
            {
                return Right.Split();
            }
            else
            {
                return true;
            }
            return true;

        }
        public string Display()
        {
            if (Value == -1)
            {
                string retVal = "[";
                retVal += Left.Display();
                retVal += ",";
                retVal += Right.Display();
                retVal += "]";
                return retVal;

            }
            else
            {
                return Convert.ToString(Value);
            }
        }

        public int GetMagnitude()
        {
            if (Value == -1)
            {
                return Left.GetMagnitude() * 3 + Right.GetMagnitude() * 2;
            }
            else
            {
                return value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //SnailFishNumber test = new SnailFishNumber("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");
            //while (!test.Reduce(0)) ;
            //Console.WriteLine(test.Display());

            string[] Puzzleinput = File.ReadAllLines("Input.txt");
            List<SnailFishNumber> InputNumbers = new List<SnailFishNumber>();

            foreach (string line in Puzzleinput)
            {
                InputNumbers.Add(new SnailFishNumber(line));
            }

            SnailFishNumber Accumulator = null;

            foreach (SnailFishNumber num in InputNumbers)
            {
                if (Accumulator == null)
                {
                    Accumulator = num;
                }
                else
                {
                    Accumulator = new SnailFishNumber() { Left = Accumulator, Right = num };
                    Accumulator.Right.Parent = Accumulator;
                    Accumulator.Left.Parent = Accumulator;
                    Accumulator.Number = Accumulator.Display();
                    //Console.WriteLine(Accumulator.Display());
                    Reduce(Accumulator);
                }
            }
            Console.WriteLine(Accumulator.Display());
           Console.WriteLine(Accumulator.GetMagnitude());

            int max = 0;
            for (int leftCount = 0; leftCount < Puzzleinput.Length; leftCount++)
            {
                for (int rightCount = 0; rightCount < Puzzleinput.Length; rightCount++)
                {
                    if (leftCount != rightCount)
                    {
                        SnailFishNumber LeftNum = new SnailFishNumber(Puzzleinput[leftCount]);
                        SnailFishNumber RightNum = new SnailFishNumber(Puzzleinput[rightCount]);

                        SnailFishNumber XY = new SnailFishNumber { Left = LeftNum, Right = RightNum };
                        XY.Right.Parent = XY;
                        XY.Left.Parent = XY;
                        XY.Number = XY.Display();
                        Reduce(XY);
                        // Console.WriteLine(XY.Display() + " " + XY.GetMagnitude());
                        if (XY.GetMagnitude() > max)
                        {
                            max = XY.GetMagnitude();
                        }
                    }
                }
            }
            Console.WriteLine(max);
        }

        private static void Reduce(SnailFishNumber Accumulator)
        {
            bool ReduceComplete = false;
            while (!ReduceComplete)
            {
                while (!Accumulator.Explode(0))
                {
                    //  Console.WriteLine("Exploded: " + Accumulator.Display());
                }
                ReduceComplete = true;
                if (!Accumulator.Split())
                {
                    // Console.WriteLine("Splitted: " + Accumulator.Display());
                    ReduceComplete = false;
                }
            }
            // Console.WriteLine(Accumulator.Display());
        }
    }
}
