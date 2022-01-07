using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day12
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 226;
        public override int ExpectedPart2Test { get; set; } = 3509;

        public override int SolvePart1(IEnumerable<string> input)
        {
            CaveMap map = new CaveMap();
            input.Select(p => p.Split('-'))
                .ToList()
                .ForEach(p => map.addEdge(p[0], p[1]));

            map.PrintAllPaths("start", "end", map.GenerateIsVisitedList());

            return map.numberOfPath;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            CaveMap map = new CaveMap();
            input.Select(p => p.Split('-'))
                .ToList()
                .ForEach(p => map.addEdge(p[0], p[1]));

            return input.Select(p => p.Split('-'))
                .SelectMany(p => p)
                .Where(p => p.ToLower() == p)
                .Where(p => p != "start")
                .Where(p => p != "end")
                .Distinct()
                .Select(p =>
                {
                    var isVisited = map.GenerateIsVisitedList();
                    isVisited[p]++;
                    return map.PrintAllPaths("start", "end", isVisited);
                })
                .SelectMany(p => p)
                .Distinct()
                .Count();
        }

        public class CaveMap
        {
            private Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();
            public int numberOfPath = 0;
            public void addEdge(string a, string b)
            {
                if (!edges.ContainsKey(a))
                {
                    edges[a] = new List<string>();
                }
                if (!edges.ContainsKey(b))
                {
                    edges[b] = new List<string>();
                }

                edges[a].Add(b);
            }

            public Dictionary<string, int?> GenerateIsVisitedList()
            {
                return edges.Values.SelectMany(p => p).Union(edges.Keys).Distinct().ToDictionary(p => p, p => p.ToUpper() == p ? null : (int?)1);
            }

            public List<string> PrintAllPaths(string a, string b, Dictionary<string, int?> isVisited)
            {
                numberOfPath = 0;
                List<string> pathList = new List<string>() { a };
                isVisited[a] = 0;
                List<string> allPathList = new List<string>();
                PrintAllPathsUtil(a, b, isVisited, pathList, allPathList);

                return allPathList;
            }

            private void PrintAllPathsUtil(string a, string b, Dictionary<string, int?> isVisited, List<string> pathList, List<string> allPathList)
            {

                if (a.Equals(b))
                {
                    numberOfPath++;
                    var path = string.Join(" ", pathList);
                    allPathList.Add(path);
                    return;
                }

                var neighbours = edges[a].ToList().Union(edges.Where(p => p.Value.Contains(a)).Select(p => p.Key)).ToList();
                
                foreach (string i in neighbours)
                {
                    if (isVisited[i] > 0 || isVisited[i] == null)
                    {
                        pathList.Add(i);
                        if (isVisited[i] != null)
                        {
                            isVisited[i]--;
                        }

                        PrintAllPathsUtil(i, b, isVisited, pathList, allPathList);

                        pathList.RemoveAt(pathList.Count - 1);

                        if (isVisited[i] != null)
                        {
                            isVisited[i]++;
                        }
                    }
                }
            }
        }

        public class Graph
        {
            private int numberOfVertices;
            private List<int>[] adjacencyList;

            public Graph(int vertices)
            {
                this.numberOfVertices = vertices;
                InitialiseAdjacencyList();
            }

            private void InitialiseAdjacencyList()
            {
                adjacencyList = new List<int>[numberOfVertices];

                for (int i = 0; i < numberOfVertices; i++)
                {
                    adjacencyList[i] = new List<int>();
                }
            }

            public void addEdge(int u, int v)
            {
                // Add v to u's list.
                adjacencyList[u].Add(v);
            }

            // Prints all paths from
            // 's' to 'd'
            public void printAllPaths(int s, int d)
            {
                bool[] isVisited = new bool[numberOfVertices];
                List<int> pathList = new List<int>();

                // add source to path[]
                pathList.Add(s);

                // Call recursive utility
                printAllPathsUtil(s, d, isVisited, pathList);
            }

            // A recursive function to print
            // all paths from 'u' to 'd'.
            // isVisited[] keeps track of
            // vertices in current path.
            // localPathList<> stores actual
            // vertices in the current path
            private void printAllPathsUtil(int u, int d,
                                           bool[] isVisited,
                                           List<int> localPathList)
            {

                if (u.Equals(d))
                {
                    Console.WriteLine(string.Join(" ", localPathList));
                    // if match found then no need
                    // to traverse more till depth
                    return;
                }

                // Mark the current node
                isVisited[u] = true;

                // Recur for all the vertices
                // adjacent to current vertex
                foreach (int i in adjacencyList[u])
                {
                    if (!isVisited[i])
                    {
                        // store current node in path[]
                        localPathList.Add(i);
                        printAllPathsUtil(i, d, isVisited, localPathList);

                        // remove current node in path[]
                        localPathList.Remove(i);
                    }
                }

                // Mark the current node
                isVisited[u] = false;
            }
        }
    }
}
