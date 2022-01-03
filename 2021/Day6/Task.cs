using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace _2021.Day6
{
    class Task : BaseTask<BigInteger>
    {
        public override BigInteger ExpectedPart1Test { get; set; } = 5934;
        public override BigInteger ExpectedPart2Test { get; set; } = 26984457539;

        private class Lanternfish
        {
            public int Timer { get; set; }
            public BigInteger Count { get; set; }
        }
        public override BigInteger SolvePart1(IEnumerable<string> input)
        {
            var fish = input.ElementAt(0).Split(',').Select(p => new Lanternfish { Timer = int.Parse(p) }).ToList();

            for (int day = 0; day < 80; day++)
            {
                var numberOfFish = fish.Count();
                for (int i = 0; i < numberOfFish; i++)
                {
                    fish[i].Timer -= 1;
                    if (fish[i].Timer < 0)
                    {
                        fish[i].Timer = 6;
                        fish.Add(new Lanternfish() { Timer = 8 });
                    }
                }
            }

            return fish.Count;
        }

        public override BigInteger SolvePart2(IEnumerable<string> input)
        {
            var fish = Enumerable.Range(0, 9).Select(p => new Lanternfish() { Timer = p, Count = 0 }).ToList();

            input.ElementAt(0).Split(',').Select(int.Parse).ToList().ForEach(p =>
            {
                fish.First(f => f.Timer == p).Count += 1;
            });

            for (int day = 0; day < 256; day++)
            {
                foreach (var fishItem in fish)
                {
                    fishItem.Timer -= 1;
                }
                var nextGen = fish.First(p => p.Timer < 0);
                nextGen.Timer = 8;
                var sixGen = fish.First(p => p.Timer == 6);
                sixGen.Count += nextGen.Count;
            }

            BigInteger result = 0;
            fish.ForEach(p => result += p.Count);

            return result;
        }
    }
}
