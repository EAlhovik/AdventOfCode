using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day24
{
    class Task : BaseTask<long>
    {
        public override long ExpectedPart1Test { get; set; } = 94992992796199;

        public override long ExpectedPart2Test { get; set; } = 11931881141161;

        public override long SolvePart1(IEnumerable<string> input)
        {
            var simplifiedInput = input.Chunk(18).SelectMany(c => new[] { c[4], c[5], c[15] })
                .Select(p => int.Parse(p.Substring(6)))
                .Chunk(3).ToList();

            var allResults = Compute(simplifiedInput, new int[0], 0, 0).ToList();
            return allResults.Max();
        }

        // simplified reddit solution
        private IEnumerable<long> Compute(List<List<int>> input, int[] digits, int index, int z)
        {
            //div z input[index][0]
            //add x input[index][1]
            //add y input[index][2]
            if (digits.Length == input.Count)
            {
                return z == 0
                    ? new[] { long.Parse(string.Join("", digits)) }
                    : new long[0];
            }

            var numbers = Enumerable.Range(1, 9).ToList();
            if (input[index][0] == 26)
            {
                return ComputeNext(
                    input,
                    digits,
                    index,
                    z,
                    numbers.Where(p => (z % 26 + input[index][1]) == p).ToList(),
                    p => z / 26
                    );
            }
            else
            {
                return ComputeNext(
                    input,
                    digits,
                    index,
                    z,
                    numbers,
                    p => z * 26 + p + input[index][2]
                    );
            }
        }

        private IEnumerable<long> ComputeNext(List<List<int>> input, int[] digits, int index, int z, IEnumerable<int> possibleNumbers, Func<int, int> zFunc)
        {
            return possibleNumbers.SelectMany(p => Compute(input, digits.Append(p).ToArray(), index + 1, zFunc(p)));
        }

        public override long SolvePart2(IEnumerable<string> input)
        {
            var simplifiedInput = input.Chunk(18).SelectMany(c => new[] { c[4], c[5], c[15] })
                .Select(p => int.Parse(p.Substring(6)))
                .Chunk(3).ToList();

            var allResults = Compute(simplifiedInput, new int[0], 0, 0).ToList();
            return allResults.Min();
        }
    }
}
