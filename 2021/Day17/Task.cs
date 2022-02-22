using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day17
{
    class Task : BaseTask<int>
    {
        private class TargetArea
        {
            public int X1 { get; set; }
            public int X2 { get; set; }
            public int Y1 { get; set; }
            public int Y2 { get; set; }
        }
        public override int ExpectedPart1Test { get; set; } = 45;
        public override int ExpectedPart2Test { get; set; } = 112;
        public override int SolvePart1(IEnumerable<string> input)
        {
            var targetArea = input.Select(d => d.Split(": ")[1].Split(", ").Select(p => p.Split("=")[1].Split("..").Select(int.Parse).ToList()))
                .Select(p => new TargetArea
                {
                    X1 = p.ElementAt(0)[0],
                    X2 = p.ElementAt(0)[1],
                    Y1 = p.ElementAt(1)[0],
                    Y2 = p.ElementAt(1)[1]
                }).First();

            var velocities = BruteForceVelocities(targetArea);

            return velocities.Select(p =>
            {
                var yVelocity = p.Item2;
                var y = 0;
                var yMax = y;
                for (int i = 0; i < p.Item3; i++)
                {
                    y += yVelocity;
                    yVelocity -= 1;
                    yMax = yMax < y ? y : yMax;
                }
                return (p, yMax);
            }).Select(p => p.yMax).Max();
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var targetArea = input.Select(d => d.Split(": ")[1].Split(", ").Select(p => p.Split("=")[1].Split("..").Select(int.Parse).ToList()))
                .Select(p => new TargetArea
                {
                    X1 = p.ElementAt(0)[0],
                    X2 = p.ElementAt(0)[1],
                    Y1 = p.ElementAt(1)[0],
                    Y2 = p.ElementAt(1)[1]
                }).First();

            return BruteForceVelocities(targetArea).Select(p => (p.Item1, p.Item2)).Distinct().Count();
        }

        private List<(int, int, int)> BruteForceVelocities(TargetArea targetArea)
        {
            var xVelocities = new Dictionary<int, List<int>>();
            for (int x = 0; x < targetArea.X2 + 2; x++)
            {
                var xVelocity = x;
                xVelocities.Add(x, new List<int>() { xVelocity });
                var prevX = xVelocity;
                var d = -1;
                for (int n = 1; n < targetArea.X2; n++)
                {
                    if (xVelocity == 0)
                    {
                        d = 0;
                    }
                    xVelocity += d;
                    xVelocities[x].Add(prevX += xVelocity);
                }
            }
            xVelocities = xVelocities
                .Where(p => p.Value.Any(x => targetArea.X1 <= x && x <= targetArea.X2))
                .ToDictionary(p => p.Key, p => p.Value);
            var result = new List<(int, int, int)>();
            for (int yVelocity = targetArea.Y1; yVelocity < targetArea.Y1 + 100000; yVelocity++)
            {

                for (int y = targetArea.Y1; y <= targetArea.Y2; y++)
                {
                    var a = 1;
                    var b = 1 + 2 * yVelocity;
                    var c = 2 * y;
                    var D = b * b - 4 * a * c;
                    if (D < 0) continue;
                    var n1 = (-b + Math.Sqrt(D)) / 2 * a;
                    var n2 = (-b - Math.Sqrt(D)) / 2 * a;
                    var possibleNs = new List<int?> {
                    TestY(yVelocity, (int)Math.Abs(n1), y),
                    TestY(yVelocity, (int)Math.Abs(n2), y)
                }
                    .Where(p => p != null)
                    .ToList();
                    if (possibleNs.Any())
                    {
                        foreach (var n in possibleNs)
                        {
                            foreach (var xVelocity in xVelocities)
                            {
                                if (xVelocity.Value.Count >= n.Value)
                                {
                                    var x = xVelocity.Value[n.Value - 1];
                                    if (targetArea.X1 <= x && x <= targetArea.X2)
                                    {
                                        result.Add((xVelocity.Key, yVelocity, n.Value));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private int? TestY(int yVelocity, int n, int expectedY)
        {
            return (2 * yVelocity + (-1) * (n - 1)) * n / 2 == expectedY
                ? n
                : (int?)null;
        }
    }
}
