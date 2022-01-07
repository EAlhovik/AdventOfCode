using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day11
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 1656;
        public override int ExpectedPart2Test { get; set; } = 195;

        private class Octopus
        {
            public int i { get; set; }
            public int j { get; set; }
            public int Energy { get; set; }
            public bool Flashed { get; set; }
        }

        private static int FlashOctopus(Octopus[,] octopuses, Octopus octopus)
        {
            int flashes = 1;
            Func<int, int, List<Octopus>> getAdjacent = (int i, int j) =>
            {
                var points = new List<Tuple<int, int>>
                {
                    Tuple.Create(i + 1, j),
                    Tuple.Create(i + 1, j + 1),
                    Tuple.Create(i, j + 1),
                    Tuple.Create(i - 1, j + 1),
                    Tuple.Create(i - 1, j),
                    Tuple.Create(i - 1, j - 1),
                    Tuple.Create(i, j - 1),
                    Tuple.Create(i + 1, j - 1),
                };
                return points
                    .Where(p => p.Item1 >= 0)
                    .Where(p => p.Item2 >= 0)
                    .Where(p => p.Item1 < octopuses.GetLength(0))
                    .Where(p => p.Item2 < octopuses.GetLength(0))
                    .Select(p => octopuses[p.Item1, p.Item2])
                    .ToList();
            };

            octopus.Flashed = true;
            octopus.Energy = 0;
            var adjacent = getAdjacent(octopus.i, octopus.j).ToList();
            adjacent.Where(p => !p.Flashed).ToList().ForEach(p => p.Energy += 1);
            if (adjacent.Any(p => p.Energy > 9))
            {
                foreach (var item in adjacent)
                {
                    if (item.Energy > 9)
                    {
                        flashes += FlashOctopus(octopuses, item);
                    }
                }
            }

            return flashes;
        }
        public override int SolvePart1(IEnumerable<string> input)
        {
            var gridSize = input.Count();
            var octopuses = new Octopus[gridSize, gridSize];
            var inputList = input.ToList();
            for (int i = 0; i < inputList.Count; i++)
                for (int j = 0; j < inputList[i].Length; j++)
                    octopuses[i, j] = new Octopus() { Energy = int.Parse(inputList[i][j].ToString()), i = i, j = j };
            var flashes = 0;

            for (int step = 0; step < 100; step++)
            {
                foreach (var octopus in octopuses)
                {
                    octopus.Energy += 1;
                    octopus.Flashed = false;
                }

                foreach (var octopus in octopuses)
                {
                    if (octopus.Energy > 9)
                    {
                        flashes += FlashOctopus(octopuses, octopus);
                    }
                }
            }
            return flashes;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var gridSize = input.Count();
            var octopuses = new Octopus[gridSize, gridSize];
            var inputList = input.ToList();
            for (int i = 0; i < inputList.Count; i++)
                for (int j = 0; j < inputList[i].Length; j++)
                    octopuses[i, j] = new Octopus() { Energy = int.Parse(inputList[i][j].ToString()), i = i, j = j };
            var flashes = 0;
            var step = 0;
            while(true)
            {
                foreach (var octopus in octopuses)
                {
                    octopus.Energy += 1;
                    octopus.Flashed = false;
                }

                foreach (var octopus in octopuses)
                {
                    if (octopus.Energy > 9)
                    {
                        flashes = FlashOctopus(octopuses, octopus);
                    }
                }
                if(flashes == octopuses.Length)
                {
                    return step + 1;
                }
                step++;
                //Print(octopuses, $"After step {step + 1} we have {flashes} flashes");
            }
        }

        private void Print(Octopus[,] octopuses, string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);

            for (int i = 0; i < octopuses.GetLength(0); i++)
            {
                for (int j = 0; j < octopuses.GetLength(1); j++)
                {
                    if (octopuses[i, j].Flashed)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(octopuses[i, j].Energy);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
