using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day7
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 37;
        public override int ExpectedPart2Test { get; set; } = 168;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var numbers = input.ElementAt(0).Split(',').Select(int.Parse).ToList();
            return numbers
                .GroupBy(p => p)
                .Select(p => p.First())
                .ToList()
                .Select(n => numbers.Select(p => Math.Abs(p - n)).Sum())
                .Min();
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var numbers = input.ElementAt(0).Split(',').Select(int.Parse).ToList();

            return Enumerable.Range(numbers.Min(), numbers.Max() - numbers.Min())
                .Select(n => numbers.Select(p => (1 + Math.Abs(p - n)) * Math.Abs(p - n) / 2).Sum())
                .Min();
        }
    }
}
