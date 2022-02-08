using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace _2021.Day15
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 40;
        public override int ExpectedPart2Test { get; set; } = 315;

        // https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
        public override int SolvePart1(IEnumerable<string> input)
        {
            var grid = input.Select(p => p.ToCharArray().Select(c => int.Parse(c.ToString())).ToList()).ToList();

            var graph = new Graph<int, string>();
            var gridSize = input.Count();
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    graph.AddNode(GetNodeName(i, j, gridSize));

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    foreach (var adjacent in GetAdjacent(i, j, gridSize))
                    {
                        var value = grid[adjacent.Item1][adjacent.Item2];
                        graph.Connect(
                            (uint)GetNodeName(i, j, gridSize),
                            (uint)GetNodeName(adjacent.Item1, adjacent.Item2, gridSize),
                            value,
                            $"{i}:{j}:{value}");
                    }
                }
            }

            ShortestPathResult result = graph.Dijkstra(1, (uint)(gridSize * gridSize));

            return result.Distance;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var grid = input.Select(p => p.ToCharArray().Select(c => int.Parse(c.ToString())).ToList()).ToList();
            grid = HorizontalScale(VerticalScale(grid, 5), 5);

            var graph = new Graph<int, string>();
            var gridSize = grid.Count();
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    graph.AddNode(GetNodeName(i, j, gridSize));

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    foreach (var adjacent in GetAdjacent(i, j, gridSize))
                    {
                        var value = grid[adjacent.Item1][adjacent.Item2];
                        graph.Connect(
                            (uint)GetNodeName(i, j, gridSize),
                            (uint)GetNodeName(adjacent.Item1, adjacent.Item2, gridSize),
                            value,
                            $"{i}:{j}:{value}");
                    }
                }
            }

            ShortestPathResult result = graph.Dijkstra(1, (uint)(gridSize * gridSize));
            return result.Distance;
        }

        private List<List<int>> HorizontalScale(List<List<int>> iterationGrid, int scale = 5)
        {
            var grid = new List<List<int>>();

            for (int i = 0; i < scale; i++)
            {
                for (int j = 0; j < iterationGrid.Count; j++)
                {
                    if (i == 0) grid.Add(new List<int>());
                    grid[j].AddRange(iterationGrid[j]);
                }
                Increment(iterationGrid);
            }
            return grid;
        }

        private List<List<int>> VerticalScale(List<List<int>> iterationGrid, int scale = 5)
        {
            var grid = new List<List<int>>();
            for (int i = 0; i < scale; i++)
            {
                foreach (var item in iterationGrid)
                {
                    grid.Add(JsonSerializer.Deserialize<List<int>>(JsonSerializer.Serialize(item)));
                }
                Increment(iterationGrid);
            }
            return grid;
        }

        private void Increment(List<List<int>> iterationGrid)
        {
            for (int i = 0; i < iterationGrid.Count; i++)
            {
                for (int j = 0; j < iterationGrid[i].Count; j++)
                {
                    var newValue = iterationGrid[i][j] + 1;
                    iterationGrid[i][j] = newValue > 9 ? 1 : newValue;
                }
            }
        }

        public int GetNodeName(int i, int j, int gridSize)
        {
            return j * gridSize + i + 1;
        }

        public List<(int, int)> GetAdjacent(int i, int j, int gridSize)
        {
            return new List<(int, int)>
            {
                { (i, j + 1) },
                { (i + 1, j) },
                { (i, j - 1) },
                { (i - 1, j) },
            }
            .Where(p => p.Item1 >= 0 && p.Item2 >= 0)
            .Where(p => p.Item1 < gridSize && p.Item2 < gridSize)
            .ToList();
        }
    }
}
