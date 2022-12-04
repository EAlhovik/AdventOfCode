using System.Collections.Generic;
using System.Linq;

namespace _2022.Day02
{
    internal class Task : BaseTask<int>
    {
        public enum Choise
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        public Task() : base(15, 12) { }

        public override int SolvePart1(List<string> input)
        {
            var calculator = new Dictionary<string, int>
            {
                { "A X", (int)Choise.Rock + 3 },
                { "A Y", (int)Choise.Paper + 6 },
                { "A Z", (int)Choise.Scissors + 0 },

                { "B X", (int)Choise.Rock + 0 },
                { "B Y", (int)Choise.Paper + 3 },
                { "B Z", (int)Choise.Scissors + 6 },

                { "C X", (int)Choise.Rock + 6 },
                { "C Y", (int)Choise.Paper + 0 },
                { "C Z", (int)Choise.Scissors + 3 }
            };
            return input.Select(p => calculator[p]).Sum();
        }

        public override int SolvePart2(List<string> input)
        {
            var calculator = new Dictionary<string, int>
            {
                { "A X", (int)Choise.Scissors + 0 },
                { "A Y", (int)Choise.Rock + 3 },
                { "A Z", (int)Choise.Paper + 6 },

                { "B X", (int)Choise.Rock + 0 },
                { "B Y", (int)Choise.Paper + 3 },
                { "B Z", (int)Choise.Scissors + 6 },

                { "C X", (int)Choise.Paper + 0 },
                { "C Y", (int)Choise.Scissors + 3 },
                { "C Z", (int)Choise.Rock + 6 }
            };
            return input.Select(p => calculator[p]).Sum();
        }
    }
}
