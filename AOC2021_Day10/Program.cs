using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day10
{
    class Program
    {
        static string[] Puzzleinput;
        static int currentPos = 0;
        static string validOpen = "({[<";
        static string validClose = ">}])";
        static int SyntaxErrorScore = 0;
        static UInt64 AutoCompleteScore = 0;
        static List<UInt64> AutoCompleteScores = new List<UInt64>();
        static string currentLine;

        static void Main(string[] args)
        {
            Puzzleinput = File.ReadAllLines("Input.txt");

            foreach (string line in Puzzleinput)
            {
                AutoCompleteScore = 0;
                currentLine = line;
                currentPos = 0;
                while (currentPos < currentLine.Length && OpenChunk(currentLine[currentPos++]))
                {
                    
                }
                if (AutoCompleteScore != 0)
                {
                    Console.WriteLine("Adding Autocomplete " + AutoCompleteScore);
                    AutoCompleteScores.Add(AutoCompleteScore);
                }
            }
            Console.WriteLine(SyntaxErrorScore);
            AutoCompleteScores.Sort();

            Console.WriteLine(AutoCompleteScores[Convert.ToInt32(Math.Floor(Convert.ToDecimal(AutoCompleteScores.Count/2)))]);
        }

        private static bool OpenChunk(char v)
        {
            char open = v;
            while (currentPos < currentLine.Length && validOpen.Contains(currentLine[currentPos]))
            {
                char newOpenChar = currentLine[currentPos++];
                if (validOpen.Contains(newOpenChar))
                {
                    if (!OpenChunk(newOpenChar))
                    {
                        return false;
                    }
                }
            }
            if (currentPos < currentLine.Length)
            {
                char newCloseChar = currentLine[currentPos++];
                if (MatchChunk(open, newCloseChar))
                {
                    return true;
                }
                else
                {
                    SyntaxErrorScore += GetSyntaxErrorScore(newCloseChar);
                    return false;
                }
            }
            else
            {
                AutoCompleteScore *= 5;
                AutoCompleteScore += getAutoCompleteScore(open);
                //Console.WriteLine("Autocomplete Score now " + AutoCompleteScore);
                return true; // incomplete
            }
        }

        private static UInt64 getAutoCompleteScore(char open)
        {
            switch (open)
            {
                case '(':
                    return 1;
                case '[':
                    return 2;
                case '{':
                    return 3;
                case '<':
                    return 4;
                default:
                    return 0;
            }
        }

        private static int GetSyntaxErrorScore(char newChar)
        {
            switch (newChar)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    return 0;

            }
        }

        private static bool MatchChunk(char open, char close)
        {
            if (open == '<' && close == '>')
            {
                return true;
            }
            if (open == '(' && close == ')')
            {
                return true;
            }
            if (open == '[' && close == ']')
            {
                return true;
            }
            if (open == '{' && close == '}')
            {
                return true;
            }

            return false;
        }
    }
}
