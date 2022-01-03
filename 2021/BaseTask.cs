using System;
using System.Collections.Generic;

namespace _2021
{
    abstract class BaseTask<T> where T: struct
    {
        public abstract T ExpectedPart1Test { get; set; }

        public abstract T ExpectedPart2Test { get; set; }

        public void Part1(string day)
        {
            var testResult = SolvePart1(Utils.GetTestLines(day));
            var result = SolvePart1(Utils.GetInputLines(day));

            Console.WriteLine(
                "Day {0} Part 1 = {1} with result {2}",
                day,
                testResult.Equals(ExpectedPart1Test) ? "Passed" : "FAILED",
                result);
        }

        public void Part2(string day)
        {
            var testResult = SolvePart2(Utils.GetTestLines(day));
            var result = SolvePart2(Utils.GetInputLines(day));

            Console.WriteLine(
                "Day {0} Part 2 = {1} with result {2}",
                day,
                testResult.Equals(ExpectedPart2Test) ? "Passed" : "FAILED",
                result);
        }

        public abstract T SolvePart1(IEnumerable<string> input);

        public abstract T SolvePart2(IEnumerable<string> input);
    }
}
