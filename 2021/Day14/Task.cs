using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021.Day14
{
    class Task : BaseTask<long>
    {
        public override long ExpectedPart1Test { get; set; } = 1588;
        public override long ExpectedPart2Test { get; set; } = 2188189693529;

        public override long SolvePart1(IEnumerable<string> input)
        {
            return RunInjections(input, 10);
        }

        public override long SolvePart2(IEnumerable<string> input)
        {
            return RunInjections(input, 40);
        }

        private long RunInjections(IEnumerable<string> input, int iterations)
        {
            var template = input.ElementAt(0);
            var rules = input.Skip(2)
                .Select(p => p.Split(" -> "))
                .ToDictionary(p => (p[0][0], p[0][1]), p => p[1][0]);

            var elements = new Dictionary<char, long>();

            template.ToList().ForEach(p =>
            {
                if (elements.ContainsKey(p))
                    elements[p]++;
                else
                    elements[p] = 1;
            });

            var cache = new Dictionary<(int, (char, char)), Dictionary<char, long>>();
            template.Zip(template.Skip(1))
                .ToList()
                .ForEach(pair => {
                    RunInjection(pair, iterations, rules, elements, cache);
                });


            return elements.Max(p => p.Value) - elements.Min(p => p.Value);
        }

        private Dictionary<char, long> RunInjection((char First, char Second) pair, int iterations, Dictionary<(char, char), char> rules, Dictionary<char, long> elements, Dictionary<(int, (char, char)), Dictionary<char, long>> cache)
        {
            Dictionary<char, long> result;

            if(cache.TryGetValue((iterations, pair), out result))
            {
                result.ToList().ForEach(p =>
                {
                    elements[p.Key] = elements.ContainsKey(p.Key)
                        ? elements[p.Key] + p.Value
                        : p.Value;
                });
            }
            else
            {
                result = new Dictionary<char, long>();

                if (iterations > 0)
                {
                    if (rules.TryGetValue(pair, out var injection))
                    {
                        var left = RunInjection((pair.First, injection), iterations - 1, rules, elements, cache);
                        var right = RunInjection((injection, pair.Second), iterations - 1, rules, elements, cache);
                        left.ToList().ForEach(p =>
                        {
                            result[p.Key] = result.ContainsKey(p.Key)
                                ? result[p.Key] + p.Value
                                : p.Value;
                        });
                        right.ToList().ForEach(p =>
                        {
                            result[p.Key] = result.ContainsKey(p.Key)
                                ? result[p.Key] + p.Value
                                : p.Value;
                        });
                        result[injection] = result.ContainsKey(injection)
                            ? result[injection] + 1
                            : 1;
                        elements[injection] = elements.ContainsKey(injection)
                            ? elements[injection] + 1
                            : 1;
                    }
                    else
                    {
                        cache[(iterations, pair)] = new Dictionary<char, long>();
                        throw new NotImplementedException();
                    }
                }
                cache[(iterations, pair)] = result;

            }

            return cache[(iterations, pair)];
        }
    }
}
