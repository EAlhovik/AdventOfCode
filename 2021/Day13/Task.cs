using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day13
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 17;
        public override int ExpectedPart2Test { get; set; } = 16;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var dots = input.ToList().GetRange(0, input.ToList().FindIndex(string.IsNullOrEmpty))
                .Select(p => Tuple.Create(int.Parse(p.Split(",")[0]), int.Parse(p.Split(",")[1])))
                .ToHashSet();

            var foldInstructions = input.ToList().GetRange(input.ToList().FindIndex(string.IsNullOrEmpty) + 1, input.Count() - dots.Count() - 1).Select(p => p.Split(" ")[2]).Select(p => p.Split("=")).ToList();

            var foldInstruction = foldInstructions[0];
            dots = Fold(dots, foldInstruction[0], int.Parse(foldInstruction[1]));
            return dots.Count;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var dots = input.ToList().GetRange(0, input.ToList().FindIndex(string.IsNullOrEmpty))
                .Select(p => Tuple.Create(int.Parse(p.Split(",")[0]), int.Parse(p.Split(",")[1])))
                .ToHashSet();

            var foldInstructions = input.ToList().GetRange(input.ToList().FindIndex(string.IsNullOrEmpty) + 1, input.Count() - dots.Count() - 1).Select(p => p.Split(" ")[2]).Select(p => p.Split("=")).ToList();


            foldInstructions.ForEach(foldInstruction => dots = Fold(dots, foldInstruction[0], int.Parse(foldInstruction[1])));

            Print(dots);
            return dots.Count;
        }

        private void Print(HashSet<Tuple<int, int>> dots)
        {
            var cols = dots.Max(p => p.Item1) + 1;
            var rows = dots.Max(p => p.Item2) + 1;

            for (int r = 0; r < rows; r++)
            {
                var s = "";
                for (int c = 0; c < cols; c++)
                    s += dots.Contains(Tuple.Create(c, r)) ? '#' : ' ';
                Console.WriteLine(s);
            }
            Console.WriteLine("");
        }

        private HashSet<Tuple<int, int>> Fold(HashSet<Tuple<int, int>> dots, string foldDirection, int line)
        {
            HashSet<Tuple<int, int>> foldedDots = new HashSet<Tuple<int, int>>();
            if (foldDirection == "y")
            {
                foldedDots = dots.Where(p => p.Item2 < line).ToHashSet();
                dots.Where(p => p.Item2 > line).ToList().ForEach(dot =>
                {
                    foldedDots.Add(Tuple.Create(dot.Item1, line - (dot.Item2 - line)));
                });
            }
            else
            {
                foldedDots = dots.Where(p => p.Item1 < line).ToHashSet();
                dots.Where(p => p.Item1 > line).ToList().ForEach(dot =>
                {
                    foldedDots.Add(Tuple.Create(line - (dot.Item1 - line), dot.Item2));
                });

            }
            return foldedDots;
        }
    }
}
