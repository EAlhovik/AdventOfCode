using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _2022.Day14
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(24, 93) { }

        public override int SolvePart1(List<string> input)
        {
            var sourceCoords = new Point(500, 0);
            var map = new Dictionary<Point, string>
            {
                { sourceCoords, "+" }
            };

            AddLines(map, input);
            var bottomY = map.Keys.Select(p => p.Y).Max();
            var units = 0;
            while (TryAddSand(map, sourceCoords, bottomY))
            {
                units++;
            }
            return units;
        }

        public override int SolvePart2(List<string> input)
        {
            var sourceCoords = new Point(500, 0);
            var map = new Dictionary<Point, string>
            {
                { sourceCoords, "+" }
            };

            AddLines(map, input);
            var bottomY = map.Keys.Select(p => p.Y).Max() + 2;

            var minX = map.Keys.Select(p => p.X).Min() - 500;
            var maxX = map.Keys.Select(p => p.X).Max() + 500;
            AddLine(map, new Point(minX, bottomY), new Point(maxX, bottomY));
            var units = 0;
            while (TryAddSand(map, sourceCoords, bottomY))
            {
                units++;
            }
            return units + 1;
        }

        private void AddLines(Dictionary<Point, string> map, List<string> input)
        {
            input.ForEach((row =>
            {
                var points = row.Split(" -> ").Select(p => p.Split(",").Select(int.Parse).ToList()).Select(p => new Point(p[0], p[1])).ToList();
                for (int i = 1; i < points.Count; i++)
                {
                    AddLine(map, points[i - 1], points[i]);
                }
            }));
        }

        private void AddLine(Dictionary<Point, string> map, Point p1, Point p2)
        {
            if (p1.Y == p2.Y)
            {
                for (int x = Math.Min(p1.X, p2.X); x <= Math.Max(p1.X, p2.X); x++)
                {
                    map.TryAdd(new Point(x, p1.Y), "#");
                }
            }
            else
            {
                for (int y = Math.Min(p1.Y, p2.Y); y <= Math.Max(p1.Y, p2.Y); y++)
                {
                    map.TryAdd(new Point(p1.X, y), "#");
                }
            }
        }

        private bool TryAddSand(Dictionary<Point, string> map, Point sandCoords, int bottomY)
        {
            if (sandCoords.Y > bottomY)
            {
                return false;
            }

            var down = new Point(sandCoords.X, sandCoords.Y + 1);
            var downLeft = new Point(sandCoords.X - 1, sandCoords.Y + 1);
            var downRight = new Point(sandCoords.X + 1, sandCoords.Y + 1);

            if (!map.ContainsKey(down))
            {
                return TryAddSand(map, down, bottomY);
            }
            else if (!map.ContainsKey(downLeft))
            {
                return TryAddSand(map, downLeft, bottomY);
            }
            else if (!map.ContainsKey(downRight))
            {
                return TryAddSand(map, downRight, bottomY);
            }
            else
            {
                return map.TryAdd(sandCoords, "o");
            }
        }
    }
}
