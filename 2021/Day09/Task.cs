using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day09
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 15;
        public override int ExpectedPart2Test { get; set; } = 1134;

        private class CavePoint
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public bool IsLowest { get; set; }
            public bool IsBasin { get; set; }
        }

        public override int SolvePart1(IEnumerable<string> input)
        {
            var points = new CavePoint[input.Count(), input.ElementAt(0).Length];

            input.Select((p, i) => p.ToCharArray().Select((p, j) =>
            {
                var height = int.Parse(p.ToString());
                points[i, j] = new CavePoint() { Height = height };
                return height;
            }).ToList()).ToList();


            Func<int, int, List<Tuple<int, int>>> getAdjacentCoordinates = (int i, int j) =>
            {
                return new List<Tuple<int, int>>
                {
                    Tuple.Create(i + 1, j),
                    Tuple.Create(i, j + 1),
                    Tuple.Create(i - 1, j),
                    Tuple.Create(i, j - 1),
                }
                .Where(p => p.Item1 >= 0)
                .Where(p => p.Item2 >= 0)
                .Where(p => p.Item1 < input.Count())
                .Where(p => p.Item2 < input.ElementAt(0).Length)
                .ToList();
            };
            Func<int, int, IEnumerable<CavePoint>> getAdjacentPoints = (int i, int j) =>
            {
                return getAdjacentCoordinates(i, j).Select(c => points[c.Item1, c.Item2]).ToList();
            };

            for (int i = 0; i < input.Count(); i++)
            {
                for (int j = 0; j < input.ElementAt(0).Length; j++)
                {
                    var adjacentPoints = getAdjacentPoints(i, j)
                        .Select(p => p.Height).ToList();
                    if(adjacentPoints.All(p => p > points[i, j].Height))
                    {
                        points[i, j].IsLowest = true;
                    }
                }
            }
            return (from CavePoint point in points
             where point.IsLowest
             select point.Height).Select(p => p + 1).Sum();
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var points = new CavePoint[input.Count(), input.ElementAt(0).Length];

            input.Select((p, i) => p.ToCharArray().Select((p, j) =>
            {
                var height = int.Parse(p.ToString());
                points[i, j] = new CavePoint() { Height = height, X = i, Y = j };
                return height;
            }).ToList()).ToList();

            Func<int, int, IEnumerable<CavePoint>> getAdjacentPoints = (int i, int j) =>
            {
                var adjacentPoints = new List<Tuple<int, int>>
                {
                    Tuple.Create(i + 1, j),
                    Tuple.Create(i, j + 1),
                    Tuple.Create(i - 1, j),
                    Tuple.Create(i, j - 1),
                }
                .Where(p => p.Item1 >= 0)
                .Where(p => p.Item2 >= 0)
                .Where(p => p.Item1 < input.Count())
                .Where(p => p.Item2 < input.ElementAt(0).Length)
                .ToList();
                return adjacentPoints.Select(c => points[c.Item1, c.Item2]).ToList();
            };

            for (int i = 0; i < input.Count(); i++)
            {
                for (int j = 0; j < input.ElementAt(0).Length; j++)
                {
                    var adjacentPoints = getAdjacentPoints(i, j)
                        .Select(p => p.Height).ToList();
                    if (adjacentPoints.All(p => p > points[i, j].Height))
                    {
                        points[i, j].IsLowest = true;
                    }
                }
            }

            var basinSizes = new List<int>();
            for (int i = 0; i < input.Count(); i++)
            {
                for (int j = 0; j < input.ElementAt(0).Length; j++)
                {
                    if (points[i, j].IsLowest)
                    {
                        var basin = new List<CavePoint> { points[i, j] };
                        var basinCount = 1;
                        var isNotFullBasin = true;
                        while (isNotFullBasin)
                        {
                            basin = basin.Select(p => getAdjacentPoints(p.X, p.Y).Where(p => p.Height != 9))
                                .SelectMany(p => p)
                                .Union(basin)
                                .Distinct()
                                .ToList();
                            if(basinCount == basin.Count())
                            {
                                isNotFullBasin = false;
                            }
                            basinCount = basin.Count();
                        }
                        basin.ForEach(p => p.IsBasin = true);
                        basinSizes.Add(basin.Count);
                    }

                }
            }

            return basinSizes.OrderByDescending(p => p).Take(3).Aggregate(1, (x, y) => x * y);
        }
    }
}
