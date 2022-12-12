using System;
using System.Collections.Generic;
using System.Linq;

namespace _2022.Day04
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(2, 4) { }

        public override int SolvePart1(List<string> input)
        {
            return input
                .Where(p =>
                {
                    var p1 = p.Split(',')[0].Split('-').Select(int.Parse).ToList();
                    var p2 = p.Split(',')[1].Split('-').Select(int.Parse).ToList();

                    var p1Range = $",{string.Join(",", Enumerable.Range(p1[0], p1[1] - p1[0] + 1))},";
                    var p2Range = $",{string.Join(",", Enumerable.Range(p2[0], p2[1] - p2[0] + 1))},";

                    return p1Range.Contains(p2Range) || p2Range.Contains(p1Range);
                })
                .Count();
        }

        public override int SolvePart2(List<string> input)
        {
            return input
                .Where(p =>
                {
                    var p1 = p.Split(',')[0].Split('-').Select(int.Parse).ToList();
                    var p2 = p.Split(',')[1].Split('-').Select(int.Parse).ToList();

                    var p1Range = Enumerable.Range(p1[0], p1[1] - p1[0] + 1).ToList();
                    var p2Range = Enumerable.Range(p2[0], p2[1] - p2[0] + 1).ToList();

                    return p1Range.Join(p2Range, p => p, p => p, (p, _) => p).Any();
                })
                .Count();
        }
    }
}
