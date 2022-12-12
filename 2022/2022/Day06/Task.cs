using System;
using System.Collections.Generic;
using System.Linq;

namespace _2022.Day06
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(10, 29) { }

        public override int SolvePart1(List<string> input)
        {
            var buffer = input.Single();
            var messageSize = 4;
            var singnals = Enumerable.Range(0, buffer.Length - messageSize - 1)
                .Select(p => buffer.Substring(p, messageSize))
                .Select(p => p.ToArray().Distinct().Count())
                .ToList();
            var firstSignalIndex = singnals.FindIndex(p => p == messageSize);

            return firstSignalIndex + messageSize;
        }

        public override int SolvePart2(List<string> input)
        {
            var buffer = input.Single();
            var messageSize = 14;
            var singnals = Enumerable.Range(0, buffer.Length - messageSize - 1)
                .Select(p => buffer.Substring(p, messageSize))
                .Select(p => p.ToArray().Distinct().Count())
                .ToList();
            var firstSignalIndex = singnals.FindIndex(p => p == messageSize);

            return firstSignalIndex + messageSize;
        }
    }
}
