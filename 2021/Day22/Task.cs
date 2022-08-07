using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day22
{
    class Task : BaseTask<long>
    {
        public override long ExpectedPart1Test { get; set; } = 474140;

        public override long ExpectedPart2Test { get; set; } = 2758514936282225;

        public override long SolvePart1(IEnumerable<string> input)
        {
            var rebootSteps = input.Select(p => new Cuboid(p.Split(' ')))
                .ToList();

            var reactor = new Dictionary<(int,int,int),bool>();
            foreach (var rebootStep in rebootSteps)
            {
                foreach (var coords in rebootStep.GetAllCoords())
                {
                    reactor[coords] = rebootStep.On;
                }
            }

            return reactor.Values.Count(p => p);
        }

        public override long SolvePart2(IEnumerable<string> input)
        {
            var cuboids = input.Select(p => new Cuboid(p.Split(' ')))
                .ToList();

            var intersectedCubes = new List<Cuboid>();
            foreach (var cube in cuboids)
            {
                intersectedCubes.AddRange(
                    intersectedCubes.Select(p => Cuboid.Intersect(cube, p))
                    .Where(p => p != null).ToList()
                    );
                if (cube.On)
                    intersectedCubes.Add(cube);
            }

            return intersectedCubes.Sum(b => b.GetSquare() * (b.On ? 1 : -1));
        }

        private class Cuboid
        {
            private Cuboid() { }

            public Cuboid(string[] input)
            {
                On = input[0] == "on";
                var coords = input[1].Split(',').Select(p => p.Split('=')[1].Split("..")).ToList();
                X1 = int.Parse(coords[0][0]);
                X2 = int.Parse(coords[0][1]);
                Y1 = int.Parse(coords[1][0]);
                Y2 = int.Parse(coords[1][1]);
                Z1 = int.Parse(coords[2][0]);
                Z2 = int.Parse(coords[2][1]);
            }

            public IEnumerable<(int, int, int)> GetAllCoords(int size1 = -50, int size2 = 50)
            {
                for (int x = Math.Max(X1, size1); x <= Math.Min(X2, size2); x++)
                    for (int y = Math.Max(Y1, size1); y <= Math.Min(Y2, size2); y++)
                        for (int z = Math.Max(Z1, size1); z <= Math.Min(Z2, size2); z++)
                            yield return (x,y,z);
            }

            public long GetSquare()
            {
                // don't forget x=10..10,y=10..10,z=10..10 is 1 cube
                return ((long)X2 - X1 + 1) * (Y2 - Y1 + 1) * (Z2 - Z1 + 1);
            }

            public static Cuboid Intersect(Cuboid a, Cuboid b)
            {
                int x1 = Math.Max(a.X1, b.X1);
                int x2 = Math.Min(a.X2, b.X2);
                int y1 = Math.Max(a.Y1, b.Y1);
                int y2 = Math.Min(a.Y2, b.Y2);
                int z1 = Math.Max(a.Z1, b.Z1);
                int z2 = Math.Min(a.Z2, b.Z2);
                if(x1 < x2 && y1 < y2 && z1 < z2)
                {
                    return new Cuboid {
                        X1 = x1,
                        X2 = x2,
                        Y1 = y1,
                        Y2 = y2,
                        Z1 = z1,
                        Z2 = z2,
                        On = !b.On
                    };
                }
                else
                {
                    return null;
                }
            }

            public bool On { get; set; }
            public int X1 { get; set; }
            public int X2 { get; set; }
            public int Y1 { get; set; }
            public int Y2 { get; set; }
            public int Z1 { get; set; }
            public int Z2 { get; set; }
        }
    }
}
