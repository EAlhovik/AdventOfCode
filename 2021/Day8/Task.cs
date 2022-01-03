using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day8
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 26;
        public override int ExpectedPart2Test { get; set; } = 61229;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var knownNumbers = new List<int> { 2,4,3,7};
            return input.Select(p => p.Split(" | ")[1].Split(' '))
                .SelectMany(p => p)
                .Where(p => knownNumbers.Contains(p.Length))
                .Count();
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            return input.Select(p =>
            {
                var digits = p.Split(" | ")[0].Split(' ');
                var map = new Dictionary<string, string>
                {
                    { "a", null },
                    { "b", null },
                    { "c", null },
                    { "d", null },
                    { "e", null },
                    { "f", null },
                    { "g", null },
                };
                var one = digits.First(p => p.Length == 2);
                var four = digits.First(p => p.Length == 4);
                var seven = digits.First(p => p.Length == 3);
                var eight = digits.First(p => p.Length == 7);
                map["a"] = seven.ToCharArray()
                    .Single(p => !one.Contains(p))
                    .ToString();
                digits
                    .Where(p => p.Length == 6)
                    .ToList()
                    .ForEach(sixOrZeroOrNine => { 
                        var letter = eight.ToCharArray()
                            .Single(p => !sixOrZeroOrNine.Contains(p))
                            .ToString();
                        if (one.Contains(letter))
                        {
                            map["c"] = letter;
                        }
                        else if(four.Contains(letter))
                        {
                            map["d"] = letter;
                        }
                        else
                        {
                            map["e"] = null; //letter;
                        }
                    });
                map["f"] = one.ToCharArray()
                    .First(p => p.ToString() != map["c"])
                    .ToString();
                map["b"] = four.ToCharArray()
                    .Single(p => !map.Values.Contains(p.ToString()))
                    .ToString();
                map["g"] = digits
                    .Where(p => p.Length == 5)
                    .Single(p => !p.Contains(map["c"]))
                    .ToCharArray()
                    .Single(p => !map.Values.Contains(p.ToString()))
                    .ToString();
                map["e"] = eight.ToCharArray()
                    .Single(p => !map.Values.Contains(p.ToString()))
                    .ToString();

                var number = int.Parse(string.Join("", p.Split(" | ")[1]
                    .Split(' ')
                    .Select(p => GetNumber(map, p))));
                return number;
            }).Sum();
        }

        private static int GetNumber(Dictionary<string, string> map, string code)
        {
            if(code.Length == 2)
            {
                return 1;
            }
            else if (code.Length == 3)
            {
                return 7;
            }
            else if (code.Length  == 4)
            {
                return 4;
            }
            else if (code.Length == 7)
            {
                return 8;
            }
            else if (code.Length == 6)
            {
                if (code.Contains(map["d"]) && !code.Contains(map["e"]))
                {
                    return 9;
                }
                else if (code.Contains(map["d"]))
                {
                    return 6;
                }
                else
                {
                    return 0;
                }
            }
            else if (code.Length == 5)
            {
                if (code.Contains(map["b"]))
                {
                    return 5;
                }
                else if (code.Contains(map["e"]))
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
