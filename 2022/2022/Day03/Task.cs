using System.Collections.Generic;
using System.Linq;

namespace _2022.Day03
{
    internal class Task : BaseTask<int>
    {
        private readonly Dictionary<char, int> priorities;
        public Task() : base(157, 70)
        {
            priorities = new Dictionary<char, int>();
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            alphabet.ToLower().ToCharArray().ToList().ForEach((p, index) => priorities.Add(p, index + 1));
            alphabet.ToUpper().ToCharArray().ToList().ForEach((p, index) => priorities.Add(p, index + 27));
        }

        public override int SolvePart1(List<string> input)
        {
            return input.Select(p =>
            {
                var compartment1 = p.Substring(0, p.Length / 2).ToCharArray();
                var compartment2 = p.Substring(p.Length / 2, p.Length / 2).ToCharArray();
                var sharedItem = compartment1.Join(compartment2, p => p, p => p, (p, _) => p).First();
                return priorities[sharedItem];
            }).Sum();

        }

        public override int SolvePart2(List<string> input)
        {
            var sumOfPriorities = 0;
            for (int i = 0; i < input.Count; i += 3)
            {
                var bag1 = input[i];
                var bag2 = input[i + 1];
                var bag3 = input[i + 2];

                var sharedItem = bag1
                    .Join(bag2, p => p, p => p, (p, _) => p)
                    .Join(bag3, p => p, p => p, (p, _) => p)
                    .First();
                sumOfPriorities += priorities[sharedItem];
            }
            return sumOfPriorities;
        }
    }
}
