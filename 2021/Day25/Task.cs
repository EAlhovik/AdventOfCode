using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day25
{
    class Task : BaseTask<int>
    {
        private const char East = '>';
        private const char South = 'v';
        private const char Empty = '.';
        public override int ExpectedPart1Test { get; set; } = 58;

        public override int ExpectedPart2Test { get; set; } = 1;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var rows = input.Count();
            var cols = input.ElementAt(0).Count();
            char[][] map = new char[rows][];
            input.Select((row, rowIndex) =>
            {
                map[rowIndex] = new char[cols];
                row.Select((col, colInex) =>
                {
                    map[rowIndex][colInex] = col;
                    return 1;
                }).ToList();
                return 1;
            }).ToList();
            var moving = true;
            var stepCount = 0;
            while (moving)
            {
                var toBeUpdated = new List<(int, int, char)>();
                for (int i = 0; i < map.Length; i += 1)
                    for (int j = 0; j < map[i].Length; j += 1)
                        if (map[i][j] == East)
                        {
                            var jNext = j + 1 == cols ? 0 : j + 1;
                            if (map[i][jNext] == Empty)
                            {
                                toBeUpdated.Add((i, j, map[i][j]));
                            }
                        }
                        else if (map[i][j] == South)
                        {
                            var jPrev = j - 1 == -1 ? cols - 1 : j - 1;
                            var jNext = j + 1 == cols ? 0 : j + 1;
                            var iPrev = i - 1 == -1 ? rows - 1 : i - 1;
                            var iNext = i + 1 == rows ? 0 : i + 1;
                            if (map[iNext][jPrev] != East && map[iNext][j] == Empty)
                            {
                                toBeUpdated.Add((i, j, map[i][j]));
                            }
                            else if (map[iNext][j] == East && map[iNext][jNext] == Empty)
                            {
                                toBeUpdated.Add((i, j, map[i][j]));
                            }
                        }
                foreach (var item in toBeUpdated.Where(p => p.Item3 == East))
                {
                    var i = item.Item1;
                    var j = item.Item2;
                    var jNext = j + 1 == cols ? 0 : j + 1;
                    map[i][j] = Empty;
                    map[i][jNext] = item.Item3;
                }
                foreach (var item in toBeUpdated.Where(p => p.Item3 == South))
                {
                    var i = item.Item1;
                    var j = item.Item2;
                    var iNext = i + 1 == rows ? 0 : i + 1;
                    map[i][j] = Empty;
                    map[iNext][j] = item.Item3;
                }
                stepCount++;
                moving = toBeUpdated.Any();
            }
            return stepCount;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            return 1;
        }
    }
}
