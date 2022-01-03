using System.Collections.Generic;
using System.Linq;

namespace _2021.Day2
{
    class Task : BaseTask<int>
    {
        class SubmarinePosition
        {
            public int Horizontal { get; set; } = 0;
            public int Depth { get; set; } = 0;
            public int Aim { get; set; } = 0;
        }

        public override int ExpectedPart1Test { get; set; } = 150;
        public override int ExpectedPart2Test { get; set; } = 900;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var p = input
                .Aggregate(new SubmarinePosition(), (position, seed) => {
                    var command = seed.Split(' ')[0];
                    var value = int.Parse(seed.Split(' ')[1]);
                    switch (command)
                    {
                        case "forward":
                            position.Horizontal += value;
                            break;
                        case "down":
                            position.Depth += value;
                            break;
                        case "up":
                            position.Depth -= value;
                            break;
                    }
                    return position;
                });
            return p.Depth * p.Horizontal;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var p = input
                .Aggregate(new SubmarinePosition(), (position, seed) => {
                    var command = seed.Split(' ')[0];
                    var value = int.Parse(seed.Split(' ')[1]);
                    switch (command)
                    {
                        case "forward":
                            position.Horizontal += value;
                            position.Depth += value * position.Aim;
                            break;
                        case "down":
                            position.Aim += value;
                            break;
                        case "up":
                            position.Aim -= value;
                            break;
                    }
                    return position;
                });
            return p.Depth * p.Horizontal;
        }
    }
}
