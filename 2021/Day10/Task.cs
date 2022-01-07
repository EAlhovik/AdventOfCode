using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace _2021.Day10
{
    class Task : BaseTask<BigInteger>
    {
        public override BigInteger ExpectedPart1Test { get; set; } = 26397;
        public override BigInteger ExpectedPart2Test { get; set; } = 288957;

        public override BigInteger SolvePart1(IEnumerable<string> input)
        {
            Func<string, bool> hasEmptyBracket = (string chunk) => {
                return chunk.Contains("()")
                || chunk.Contains("[]")
                || chunk.Contains("{}")
                || chunk.Contains("<>");
            };
            Func<string, string> removeEmptyBracket = (string chunk) => {
                return chunk.Replace("()", string.Empty)
                .Replace("[]", string.Empty)
                .Replace("{}", string.Empty)
                .Replace("<>", string.Empty);
            };
            return input.Select(p =>
            {
                while (hasEmptyBracket(p))
                {
                    p = removeEmptyBracket(p);
                }
                var foundIndex = p.IndexOfAny(new[] { '}', ')', ']', '>' });

                if (foundIndex == -1) {

                    return new
                    {
                        chunk = p,
                        expected = "",
                        found = ""
                    };
                }
                else
                {
                    return new
                    {
                        chunk = p,
                        expected = p[foundIndex - 1].ToString(),
                        found = p[foundIndex].ToString()
                    };
                };
            })
                .Select(p => {

                    switch (p.found)
                    {
                        case ")": return 3;
                        case "]": return 57;
                        case "}": return 1197;
                        case ">": return 25137;
                        default: return 0;
                    }
                }).Sum();
        }

        public override BigInteger SolvePart2(IEnumerable<string> input)
        {
            Func<string, bool> hasEmptyBracket = (string chunk) => {
                return chunk.Contains("()")
                || chunk.Contains("[]")
                || chunk.Contains("{}")
                || chunk.Contains("<>");
            };
            Func<string, string> removeEmptyBracket = (string chunk) => {
                return chunk.Replace("()", string.Empty)
                .Replace("[]", string.Empty)
                .Replace("{}", string.Empty)
                .Replace("<>", string.Empty);
            };
            var scores = input.Select(p =>
            {
                while (hasEmptyBracket(p))
                {
                    p = removeEmptyBracket(p);
                }
                var foundIndex = p.IndexOfAny(new[] { '}', ')', ']', '>' });
                
                if (foundIndex == -1)
                {
                    Func<char, int> getCharPoints = (char c) =>
                    {
                        switch (c)
                        {
                            case ')': return 1;
                            case ']': return 2;
                            case '}': return 3;
                            case '>': return 4;
                        }
                        throw new NotImplementedException();
                    };

    var completionString = p.Replace('(', ')')
                    .Replace('{', '}')
                    .Replace('[', ']')
                    .Replace('<', '>')
                    .Reverse();
                    BigInteger zeroScore = 0;
                    var score = completionString.Aggregate(zeroScore, (x, next) => {

                        return x * 5 + getCharPoints(next);
                    });
                    return score;
                }
                else
                {
                    return -1;
                };
            })
                .Where(p => p != -1)
                .OrderBy(p => p)
                .ToList();

            return scores[scores.Count / 2];
        }
    }
}
