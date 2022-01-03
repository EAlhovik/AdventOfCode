using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day3
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 198;
        public override int ExpectedPart2Test { get; set; } = 230;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var result = new Dictionary<int, List<int>>();
            input
                .Select(p => p.ToCharArray().Select((bit, index) => new { bit = int.Parse(bit.ToString()), index = index }).ToList())
                .ToList()
                .ForEach(p =>
                {
                    p.ForEach(line =>
                    {

                        if (!result.ContainsKey(line.index))
                        {
                            result[line.index] = new List<int>();
                        }
                        result[line.index].Add(line.bit);
                    });
                });

            var gammaRateList = result.Select(p => p.Value.Count(bit => bit == 0) > p.Value.Count(bit => bit == 1) ? 0 : 1);
            var epsilonRateList = gammaRateList.Select(p => p == 1 ? 0 : 1).ToList();

            var gammaRate = Convert.ToInt32(string.Join(string.Empty, gammaRateList), 2);
            var epsilonRate = Convert.ToInt32(string.Join(string.Empty, epsilonRateList), 2);
            return gammaRate * epsilonRate;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var oxygenGeneratorRatingList = input.Select(p => p.ToCharArray().Select(p => int.Parse(p.ToString())).ToList());
            foreach (var index in Enumerable.Range(0, input.First().Length))
            {
                if (oxygenGeneratorRatingList.Count() == 1)
                {
                    continue;
                }
                var count = oxygenGeneratorRatingList.Count();
                var countOfZeros = oxygenGeneratorRatingList.Count(p => p[index] == 0);
                var bitShouldStay = countOfZeros > (count - countOfZeros) ? 0 : 1;
                oxygenGeneratorRatingList = oxygenGeneratorRatingList.Where(p => p[index] == bitShouldStay);
            }

            var co2ScrubberRatingList = input.Select(p => p.ToCharArray().Select(p => int.Parse(p.ToString())).ToList());
            foreach (var index in Enumerable.Range(0, input.First().Length))
            {
                if(co2ScrubberRatingList.Count() == 1)
                {
                    continue;
                }
                var count = co2ScrubberRatingList.Count();
                var countOfOnes = co2ScrubberRatingList.Count(p => p[index] == 1);
                var bitShouldStay = countOfOnes < (count - countOfOnes) ? 1 : 0;
                co2ScrubberRatingList = co2ScrubberRatingList.Where(p => p[index] == bitShouldStay);
            }
            var oxygenGeneratorRating = Convert.ToInt32(string.Join(string.Empty, oxygenGeneratorRatingList.Single()), 2);
            var co2ScrubberRating = Convert.ToInt32(string.Join(string.Empty, co2ScrubberRatingList.Single()), 2);
            return oxygenGeneratorRating * co2ScrubberRating;
        }
    }
}
