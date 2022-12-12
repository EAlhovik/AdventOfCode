using System;
using System.Collections.Generic;
using System.Linq;

namespace _2022.Day09
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(13, 1) { }

        public override int SolvePart1(List<string> input)
        {
            var zeroPosition = (0, 0); // X, Y
            var dots = new Dictionary<string, (int, int)>
            {
                { "0", zeroPosition },
                { "1", zeroPosition }
            };
            var map = new Dictionary<(int, int), Cell>();
            map.Add(zeroPosition, new Cell
            {
                IsStart = true,
                Dots = dots.Keys.ToHashSet()
            });
            UpdateMap(map, dots, input);
            return map.Values.Where(p => p.Dots.Contains("1")).Count();
        }

        public override int SolvePart2(List<string> input)
        {
            var zeroPosition = (0, 0); // X, Y
            var dots = new Dictionary<string, (int, int)>
            {
                { "0", zeroPosition },
                { "1", zeroPosition },
                { "2", zeroPosition },
                { "3", zeroPosition },
                { "4", zeroPosition },
                { "5", zeroPosition },
                { "6", zeroPosition },
                { "7", zeroPosition },
                { "8", zeroPosition },
                { "9", zeroPosition }
            };
            var map = new Dictionary<(int, int), Cell>();
            map.Add(zeroPosition, new Cell
            {
                IsStart = true,
                Dots = dots.Keys.ToHashSet()
            });
            UpdateMap(map, dots, input);
            return map.Values.Where(p => p.Dots.Contains("9")).Count();
        }

        private void UpdateMap(Dictionary<(int, int), Cell> map, Dictionary<string, (int, int)> dots, List<string> input)
        {
            foreach (var motion in input)
            {
                var direction = motion.Split(' ')[0];
                var steps = int.Parse(motion.Split(' ')[1]);
                for (int i = 0; i < steps; i++)
                {
                    var headPosition = dots["0"];
                    switch (direction)
                    {
                        case "R":
                            {
                                headPosition = (headPosition.Item1 + 1, headPosition.Item2);
                                MoveDot(map, headPosition, "0");
                                break;
                            }
                        case "L":
                            {
                                headPosition = (headPosition.Item1 - 1, headPosition.Item2);
                                MoveDot(map, headPosition, "0");
                                break;
                            }
                        case "U":
                            {
                                headPosition = (headPosition.Item1, headPosition.Item2 + 1);
                                MoveDot(map, headPosition, "0");
                                break;
                            }
                        case "D":
                            {
                                headPosition = (headPosition.Item1, headPosition.Item2 - 1);
                                MoveDot(map, headPosition, "0");
                                break;
                            }
                        default:
                            break;
                    }

                    dots["0"] = headPosition;
                    string prevDot = null;
                    foreach (var dot in dots)
                    {
                        if (!string.IsNullOrEmpty(prevDot))
                        {
                            var nextDotPosition = GetNextTailPosition(dot.Value, dots[prevDot]);
                            dots[dot.Key] = nextDotPosition;
                            MoveDot(map, nextDotPosition, dot.Key);
                        }

                        prevDot = dot.Key;
                    }
                }
            }
        }

        private void MoveDot(Dictionary<(int, int), Cell> map, (int, int) position, string dot)
        {
            if (map.ContainsKey(position))
            {
                map[position].Dots.Add(dot);
            }
            else
            {
                map.Add(position, new Cell { Dots = new HashSet<string> { dot } });
            }
        }

        private (int, int) GetNextTailPosition((int, int) tailPosition, (int, int) headPosition)
        {
            var diff = (headPosition.Item1 - tailPosition.Item1, headPosition.Item2 - tailPosition.Item2);
            switch (diff)
            {
                case (-1, 0):
                case (0, 0):
                case (1, 0):
                case (-1, 1):
                case (0, 1):
                case (1, 1):
                case (-1, -1):
                case (0, -1):
                case (1, -1):
                    return tailPosition;
                case (2, 0):
                    return (tailPosition.Item1 + 1, tailPosition.Item2);
                case (2, 1):
                case (1, 2):
                    return (tailPosition.Item1 + 1, tailPosition.Item2 + 1);
                case (0, 2):
                    return (tailPosition.Item1, tailPosition.Item2 + 1);
                case (-1, 2):
                case (-2, 1):
                    return (tailPosition.Item1 - 1, tailPosition.Item2 + 1);
                case (-2, 0):
                    return (tailPosition.Item1 - 1, tailPosition.Item2);
                case (-2, -1):
                case (-1, -2):
                    return (tailPosition.Item1 - 1, tailPosition.Item2 - 1);
                case (0, -2):
                    return (tailPosition.Item1, tailPosition.Item2 - 1);
                case (1, -2):
                case (2, -1):
                    return (tailPosition.Item1 + 1, tailPosition.Item2 - 1);


                case (2, 2):
                    return (tailPosition.Item1 + 1, tailPosition.Item2 + 1);
                case (-2, 2):
                    return (tailPosition.Item1 - 1, tailPosition.Item2 + 1);
                case (-2, -2):
                    return (tailPosition.Item1 - 1, tailPosition.Item2 - 1);
                case (2, -2):
                    return (tailPosition.Item1 + 1, tailPosition.Item2 - 1);
                default: throw new NotImplementedException(string.Format("{0},{1}", diff.Item1, diff.Item2));
            }
        }

        public class Cell
        {
            public (int, int) Position { get; set; }

            public bool IsStart { get; set; }

            public HashSet<string> Dots { get; set; } = new HashSet<string>();
        }
    }
}
