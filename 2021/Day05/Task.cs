using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day05
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 5;
        public override int ExpectedPart2Test { get; set; } = 12;

        private class Line
        {
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 { get; set; }

            public IEnumerable<Line> GetLineCoords(bool diagonal = false)
            {
                if (X1 == X2)
                {
                    return Enumerable
                        .Range(Math.Min(Y1, Y2), Math.Abs(Y1 - Y2) + 1)
                        .Select(p => new Line { X1 = X1, Y1 = p })
                        .ToList();
                }
                else if (Y1 == Y2)
                {
                    return Enumerable
                        .Range(Math.Min(X1, X2), Math.Abs(X1 - X2) + 1)
                        .Select(p => new Line { X1 = p, Y1 = Y1 })
                        .ToList();
                }
                else if (!diagonal)
                {
                    return Enumerable.Empty<Line>();
                }
                else
                {
                    var xIncreace = X1 < X2;
                    var yIncreace = Y1 < Y2;
                    return Enumerable
                        .Range(0, Math.Abs(X1 - X2) + 1)
                        .Select(p => new Line { 
                            X1 = X1 + (xIncreace ? p : (-1) * p), 
                            Y1 = Y1 + (yIncreace ? p : (-1) * p)
                        });
                }
            }
        }
        public override int SolvePart1(IEnumerable<string> input)
        {
            var lines = input.Select(line =>
            {
                var cor1 = line.Split(" -> ")[0].Split(',').Select(int.Parse).ToList();
                var cor2 = line.Split(" -> ")[1].Split(',').Select(int.Parse).ToList();
                return new Line
                {
                    X1 = cor1[0],
                    Y1 = cor1[1],
                    X2 = cor2[0],
                    Y2 = cor2[1],
                };
            })
                .ToList();
            var mapSize = lines.Max(p => new List<int> { p.X1, p.Y1, p.X2, p.Y2 }.Max());

            var map = new int[mapSize + 1, mapSize + 1];

            foreach (var line in lines)
            {
                foreach (var dot in line.GetLineCoords(false))
                {
                    map[dot.X1, dot.Y1] += 1;
                }
            }

            var overlaps = (from int dot in map
                           where dot > 1
                           select dot).ToList();

            return overlaps.Count();
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var lines = input.Select(line =>
            {
                var cor1 = line.Split(" -> ")[0].Split(',').Select(int.Parse).ToList();
                var cor2 = line.Split(" -> ")[1].Split(',').Select(int.Parse).ToList();
                return new Line
                {
                    X1 = cor1[0],
                    Y1 = cor1[1],
                    X2 = cor2[0],
                    Y2 = cor2[1],
                };
            })
                .ToList();
            var mapSize = lines.Max(p => new List<int> { p.X1, p.Y1, p.X2, p.Y2 }.Max());

            var map = new int[mapSize + 1, mapSize + 1];

            foreach (var line in lines)
            {
                foreach (var dot in line.GetLineCoords(true))
                {
                    map[dot.X1, dot.Y1] += 1;
                }
            }

            var overlaps = (from int dot in map
                            where dot > 1
                            select dot).ToList();

            return overlaps.Count();
        }
    }
}
