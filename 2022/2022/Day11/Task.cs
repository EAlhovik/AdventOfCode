using System;
using System.Collections.Generic;
using System.Linq;

namespace _2022.Day11
{
    internal class Task : BaseTask<long>
    {
        public Task() : base(10605, 2713310158) { }

        public override long SolvePart1(List<string> input)
        {
            var monkeys = ParseMonkeys(input);
            return Run(monkeys, 20);
        }

        public override long SolvePart2(List<string> input)
        {
            var monkeys = ParseMonkeys(input);
            var factor = monkeys.Select(p => p.Value.DivisibleTest)
                .Aggregate(1, (a, b) => a * b);
            return Run(monkeys, 10000, factor);
        }

        private long Run(Dictionary<string, Monkey> monkeys, int rounds, int? factor = null)
        {
            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.Value.Items)
                    {
                        var itemValue = item;
                        itemValue = Compute(monkey.Value.Operation, itemValue);
                        if (factor.HasValue)
                        {

                            itemValue = itemValue % factor.Value;
                        }
                        else
                        {
                            itemValue = Convert.ToInt64(Math.Floor((decimal)itemValue / 3));
                        }

                        monkey.Value.Inspections++;
                        if (itemValue % monkey.Value.DivisibleTest == 0)
                        {
                            monkeys[monkey.Value.TrueMonkey].Items.Add(itemValue);
                        }
                        else
                        {
                            monkeys[monkey.Value.FalseMonkey].Items.Add(itemValue);
                        }
                    }
                    monkey.Value.Items = new List<long>();
                }
            }
            return monkeys.Select(p => p.Value.Inspections)
                .OrderByDescending(p => p)
                .Take(2)
                .Aggregate(1L, (a, b) => a * b);
        }

        private Dictionary<string, Monkey> ParseMonkeys(List<string> input)
        {
            var monkeys = new Dictionary<string, Monkey>();
            foreach (var monkeyItems in input.Chunk(7))
            {
                var monkey = new Monkey
                {
                    Items = monkeyItems[1].Split(':')[1].Replace(" ", "").Split(",").Select(long.Parse).ToList(),
                    Operation = monkeyItems[2].Split('=')[1].Trim(),
                    DivisibleTest = int.Parse(monkeyItems[3].Split("by")[1].Trim()),
                    TrueMonkey = monkeyItems[4].Split("monkey")[1].Trim(),
                    FalseMonkey = monkeyItems[5].Split("monkey")[1].Trim()
                };
                monkeys[monkeyItems[0].Split(' ')[1].Trim(':')] = monkey;
            }
            return monkeys;
        }

        static long Compute(string expression, long value)
        {
            var expItems = expression.Replace("old", value.ToString()).Split(' ');
            switch (expItems[1])
            {
                case "+": return long.Parse(expItems[0]) + long.Parse(expItems[2]);
                case "-": return long.Parse(expItems[0]) - long.Parse(expItems[2]);
                case "*": return long.Parse(expItems[0]) * long.Parse(expItems[2]);
            }
            throw new NotImplementedException();
        }

        private class Monkey
        {
            public List<long> Items { get; set; }

            public string Operation { get; set; }

            public int DivisibleTest { get; set; }

            public string TrueMonkey { get; set; }

            public string FalseMonkey { get; set; }

            public long Inspections { get; set; } = 0;
        }
    }
}
