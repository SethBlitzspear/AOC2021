using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day12
{
    public class Node
    {
        private string name;
        private List<Node> links = new List<Node>();
        private bool lower;

        public string Name
        {
            get => name;
            set
            {
                name = value;
             
                    
                    if (name.Equals(name.ToLower()))
                    {
                        lower = true;
                    }
                    else
                    {
                        lower = false;
                    }
                }
            
        }
        public List<Node> Links { get => links; set => links = value; }
        public bool Lower { get => lower; }
    }
   
    class Program
    {
        static Dictionary<string, Node> Graph;
        static List<Node> Route = new List<Node>();
        static int routeCount = 0;
        static Node twice = null;
        static void Main(string[] args)
        {
            Graph = new Dictionary<string, Node>();
            string[] Puzzleinput = File.ReadAllLines("Input.txt");
            foreach (string line in Puzzleinput)
            {
                string Start = line.Split("-")[0];
                string End = line.Split("-")[1];
                if(!Graph.ContainsKey(Start))
                {
                    Node newNode = new Node() { Name = Start };
                    Graph.Add(Start, newNode);
                }
                if (!Graph.ContainsKey(End))
                {
                    Node newNode = new Node() { Name = End };
                    Graph.Add(End, newNode);
                }
                Graph[Start].Links.Add(Graph[End]); 
                Graph[End].Links.Add(Graph[Start]);
            }

            FindRoute(Graph["start"]);
            Console.WriteLine(routeCount);
            
        }

        private static void FindRoute(Node From)
        {


            if (From.Name == "end")
            {
                routeCount++;
               /* foreach (Node node in Route)
                {
                    Console.Write(node.Name + "-");
                }
                Console.Write("end");
                Console.WriteLine();*/
            }

            else
            {
                Route.Add(From);
                foreach (Node searchNode in From.Links)
                {
                    if (!searchNode.Lower || !Route.Contains(searchNode))
                    {
                        FindRoute(searchNode);
                    }
                    else if (searchNode.Lower && twice == null)
                    {
                        if (!searchNode.Name.Equals("start") && !searchNode.Equals("end"))
                        {
                            twice = searchNode;
                            FindRoute(searchNode);
                        }
                    }
                }

                Route.Remove(From);
                if (From == twice)
                {
                    twice = null;
                }

            }
            
        }
    }
}
