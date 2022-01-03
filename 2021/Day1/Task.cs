using System.Collections.Generic;
using System.Linq;

namespace _2021.Day1
{

    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 7;
        public override int ExpectedPart2Test { get; set; } = 5;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var numbers = input.Select(int.Parse).ToList();
            return numbers.Skip(1).Where((p, index) => numbers[index] < p).Count();
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var numbers = input.Select(int.Parse).ToList();
            numbers = numbers.SkipLast(2).Select((p, index) => p + numbers[index + 1] + numbers[index + 2]).ToList();

            return numbers.Skip(1).Where((p, index) => numbers[index] < p).Count();
        }
    }
}
