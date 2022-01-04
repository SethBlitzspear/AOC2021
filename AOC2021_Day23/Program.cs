using System;
using System.Collections.Generic;

namespace AOC2021_Day23
{
    public class Space
    {
        Space up, down, left, right;
        string name;
        bool stop;
        bool occupied;
        int x, y;

        public Space Up { get => up; set => up = value; }
        public Space Down { get => down; set => down = value; }
        public Space Left { get => left; set => left = value; }
        public Space Right { get => right; set => right = value; }
        public string Home { get => name.Substring(0, 1); }
        public bool Stop { get => stop; set => stop = value; }
        public bool Occupied { get => occupied; set => occupied = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public string Name { get => name; set => name = value; }
    }

    public class Move
    {
        Space destination;
        int cost;
       

        public Space Destination { get => destination; set => destination = value; }
        public int Cost { get => cost; set => cost = value; }
        
    }

    public class Amphipod
    {
        Space location;
        string type;
        string name;
        public Space Location { get => location; set => location = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public bool CanMove()
        {
            if (location.Up != null && !location.Up.Occupied && location.Up.Stop)
            {
                return true;
            }
            if (location.Down != null && !location.Down.Occupied && location.Down.Stop)
            {
                return true;
            }
            if (location.Right != null && !location.Right.Occupied && location.Right.Stop)
            {
                return true;
            }
            if (location.Left != null && !location.Left.Occupied && location.Left.Stop)
            {
                return true;
            }
            return false;
        }
    }
    internal class Program
    {
        static Stack<string> moveStack = new Stack<string>();
        static Space H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, H11, A1, A2, B1, B2, C1, C2, D1, D2;
        static Amphipod AA1, AA2, AB1, AB2, AC1, AC2, AD1, AD2;
        static int moveDepth;
        static int energy;
        static int lowestEnery = int.MaxValue;
        static List<Amphipod> Amphiphods = new List<Amphipod>();
        static void Main(string[] args)
        {
            H1 = new Space() { Right = H2, Stop = true, X = 1, Y = 1, Name = "H1" };
            H2 = new Space() { Right = H3, Left = H1, Stop = true, X = 2, Y = 1, Name = "H2" };
            H3 = new Space() { Right = H4, Left = H2, Down = A1, Stop = false, X = 3, Y = 1, Name = "H3" };
            H4 = new Space() { Right = H5, Left = H3, Stop = true, X = 4, Y = 1, Name = "H4" };
            H5 = new Space() { Right = H6, Left = H4, Down = B1, Stop = false, X = 5, Y = 1, Name = "H5" };
            H6 = new Space() { Right = H7, Left = H5, Stop = true, X = 6, Y = 1, Name = "H6" };
            H7 = new Space() { Right = H8, Left = H6, Down = C1, Stop = false, X = 7, Y = 1, Name = "H7" };
            H8 = new Space() { Right = H9, Left = H7, Stop = true, X = 8, Y = 1, Name = "H8" };
            H9 = new Space() { Right = H10, Left = H8, Down = D1, Stop = false, X = 9, Y = 1, Name = "H9" };
            H10 = new Space() { Right = H11, Left = H9, Stop = true, X = 10, Y = 1, Name = "H10" };
            H11 = new Space() { Left = H10, Stop = true, X = 11, Y = 1, Name = "H10" };
            A1 = new Space() { Up = H3, Down = A2, Stop = true, X = 3, Y = 2, Name = "A1" };
            A2 = new Space() { Up = A1, Stop = true, X = 3, Y = 3, Name = "A2" };
            B1 = new Space() { Up = H5, Down = B2, Stop = true, X = 5, Y = 2, Name = "B1" };
            B2 = new Space() { Up = B1, Stop = true, X = 5, Y = 3, Name = "B2" };
            C1 = new Space() { Up = H7, Down = C2, Stop = true, X = 7, Y = 2, Name = "C1" };
            C2 = new Space() { Up = C1, Stop = true, X = 7, Y = 3, Name = "C2" };
            D1 = new Space() { Up = H9, Down = D2, Stop = true, X = 9, Y = 2, Name = "D1" };
            D2 = new Space() { Up = D1, Stop = true, X = 9, Y = 3, Name = "D2" };



            AA1 = new Amphipod() { Location = A2, Type = "A", Name = "AA1" }; A2.Occupied = true;
            AA2 = new Amphipod() { Location = D2, Type = "A", Name = "AA2" }; D2.Occupied = true;
            AB1 = new Amphipod() { Location = A1, Type = "B", Name = "AB1" }; A1.Occupied = true;
            AB2 = new Amphipod() { Location = C1, Type = "B", Name = "AB2" }; C1.Occupied = true;
            AC1 = new Amphipod() { Location = B1, Type = "C", Name = "AC1" }; B1.Occupied = true;
            AC2 = new Amphipod() { Location = C2, Type = "C", Name = "AC2" }; C2.Occupied = true;
            AD1 = new Amphipod() { Location = B2, Type = "D", Name = "AD1" }; B2.Occupied = true;
            AD2 = new Amphipod() { Location = D1, Type = "D", Name = "AD2" }; D1.Occupied = true;

            Amphiphods.Add(AA1);
            Amphiphods.Add(AA2);
            Amphiphods.Add(AB1);
            Amphiphods.Add(AB2);
            Amphiphods.Add(AC1);
            Amphiphods.Add(AC2);
            Amphiphods.Add(AD1);
            Amphiphods.Add(AD2);


                

            moveDepth = energy = 0;

            DoMove();
            Console.WriteLine(lowestEnery);

        }

        private static void DoMove()
        {
            moveDepth++;
 //           DisplayBoard();
            
           if(moveDepth == 10)
            {
                moveDepth--;
                return;
            }
            if (AllHome())
            {
                if (energy < lowestEnery)
                {
                    lowestEnery = energy;
                }
                moveDepth--; 
                return;
            }
            else 
            {
                

                foreach (Amphipod amphipod in Amphiphods)
                {
                    if (moveDepth == 1)
                    {
                        int stop = 1;
                    }
                    List<Move> ValidMoves = new List<Move>();
                    Space exploreSpace = amphipod.Location;
                    MoveTo(exploreSpace, ValidMoves, 0);
                    foreach (Move move in ValidMoves)
                    {
                        
                        if (move.Destination.Stop)
                        {
                            // Implement their silly rules.
                            Space oldLocation = amphipod.Location;
                            int energyForMove = GetEnergyUsed(amphipod, move);
                            moveStack.Push(amphipod.Name + ": " + amphipod.Location.Name + " => " + move.Destination.Name);
                            energy += energyForMove;
                            amphipod.Location.Occupied = false;
                            amphipod.Location = move.Destination;
                            amphipod.Location.Occupied = true;
                            DoMove();
                            amphipod.Location.Occupied = false;
                            amphipod.Location = oldLocation;
                            amphipod.Location.Occupied = true;
                            moveStack.Pop();
                            energy -= energyForMove;
                        }

                    }
                }
                moveDepth--;
            }
        }

        private static void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("#############");
            Console.WriteLine("#...........#");
            Console.WriteLine("###.#.#.#.###");
            Console.WriteLine("  #.#.#.#.#  ");
            Console.WriteLine("  #########  ");
            Console.WriteLine("Depth = " + moveDepth);
            Console.WriteLine("Energy = " + energy);
            Console.WriteLine("Lowest = " + lowestEnery);

            foreach (Amphipod amphipod in Amphiphods)
            {
                Console.SetCursorPosition(amphipod.Location.X, amphipod.Location.Y);
                Console.WriteLine(amphipod.Type);
            }

        }

        private static int GetEnergyUsed(Amphipod amphipod, Move move)
        {
            switch (amphipod.Type)
            {
                case "A":
                    return move.Cost;
                case "B":
                    return move.Cost * 10;
                case "C":
                    return move.Cost * 100;
                case "D":
                    return move.Cost * 1000;
            }
            return 0;
        }

        private static void MoveTo(Space exploreSpace, List<Move> validMoves, int cost)
        {
            cost++;
            if (exploreSpace.Up != null && !exploreSpace.Up.Occupied  && validMoves.Find(e => e.Destination == exploreSpace.Up) == null)
            {
                validMoves.Add(new Move() { Destination = exploreSpace.Up, Cost = cost });
                MoveTo(exploreSpace.Up, validMoves, cost);
            }
            if (exploreSpace.Down != null && !exploreSpace.Down.Occupied  && validMoves.Find(e => e.Destination == exploreSpace.Down) == null)
            {
                validMoves.Add(new Move() { Destination = exploreSpace.Down, Cost = cost });
                MoveTo(exploreSpace.Down, validMoves, cost);
            }
            if (exploreSpace.Left != null && !exploreSpace.Left.Occupied  && validMoves.Find(e => e.Destination == exploreSpace.Left) == null)
            {
                validMoves.Add(new Move() { Destination = exploreSpace.Left, Cost = cost });
                MoveTo(exploreSpace.Left, validMoves, cost);
            }
            if (exploreSpace.Right != null && !exploreSpace.Right.Occupied  && validMoves.Find(e => e.Destination == exploreSpace.Right) == null)
            {
                validMoves.Add(new Move() { Destination = exploreSpace.Right, Cost = cost });
                MoveTo(exploreSpace.Right, validMoves, cost);
            }
        }

        private static bool AllHome()
        {
            foreach (Amphipod amphipod in Amphiphods)
            {
                if (amphipod.Type == amphipod.Location.Home)
                {
                    return false;
                }
            }
            foreach (string move in moveStack)
            {
                Console.Write(move + ", ");
            }
            Console.WriteLine(" Energy = " + energy);
            return true;
        }
    }
}
