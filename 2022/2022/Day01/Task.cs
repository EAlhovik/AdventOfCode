using System.Collections.Generic;
using System.Linq;

namespace _2022.Day01
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(24000, 45000) { }

        public override int SolvePart1(List<string> input)
        {
            var inventory = GetInventory(input);
            return inventory.Max();
        }

        public override int SolvePart2(List<string> input)
        {
            var inventory = GetInventory(input);
            return inventory.OrderByDescending(p => p).Take(3).Sum();
        }

        private List<int> GetInventory(List<string> input)
        {
            var inventory = new List<int>();
            var currentInventory = 0;
            input.ForEach(callories =>
            {
                if (string.IsNullOrEmpty(callories))
                {
                    inventory.Add(currentInventory);
                    currentInventory = 0;
                }
                else
                {
                    currentInventory += int.Parse(callories);
                }
            });

            inventory.Add(currentInventory);
            return inventory;
        }
    }
}
