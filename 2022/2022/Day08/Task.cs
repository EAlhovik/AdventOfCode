using System;
using System.Collections.Generic;
using System.Linq;

namespace _2022.Day08
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(21, 8) { }

        public override int SolvePart1(List<string> input)
        {
            Dictionary<(int, int), Tree> map = GenerateMap(input);

            return map.Values.Count(p => p.IsVisible(map));
        }

        public override int SolvePart2(List<string> input)
        {
            Dictionary<(int, int), Tree> map = GenerateMap(input);

            return map.Values.Select(p => p.CalculateScenicScore(map)).Max();
        }

        private Dictionary<(int, int), Tree> GenerateMap(List<string> input)
        {
            Dictionary<(int, int), Tree> map = new Dictionary<(int, int), Tree>();
            for (int rowIndex = 0; rowIndex < input.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < input[rowIndex].Length; colIndex++)
                {
                    map.Add((rowIndex, colIndex), new Tree
                    {
                        Position = (rowIndex, colIndex),
                        Value = int.Parse(input[rowIndex][colIndex].ToString())
                    });
                }
            }
            return map;
        }

        public class Tree
        {
            public (int, int) Position { get; set; }

            public int Value { get; set; }

            public int CalculateScenicScore(Dictionary<(int, int), Tree> map)
            {
                return CalculateScenicScoreLeft(map)
                    * CalculateScenicScoreRight(map)
                    * CalculateScenicScoreTop(map)
                    * CalculateScenicScoreBottom(map);
            }

            private int CalculateScenicScoreBottom(Dictionary<(int, int), Tree> map)
            {
                var count = 0;
                for (int i = Position.Item1 + 1; i < Math.Sqrt(map.Count); i++)
                {
                    count++;
                    if (map[(i, Position.Item2)].Value >= Value)
                    {
                        break;
                    }
                }

                return count;
            }

            private int CalculateScenicScoreTop(Dictionary<(int, int), Tree> map)
            {
                var count = 0;
                for (int i = Position.Item1 - 1; i >= 0; i--)
                {
                    count++;
                    if (map[(i, Position.Item2)].Value >= Value)
                    {
                        break;
                    }
                }

                return count;
            }

            private int CalculateScenicScoreRight(Dictionary<(int, int), Tree> map)
            {
                var count = 0;
                for (int i = Position.Item2 + 1; i < Math.Sqrt(map.Count); i++)
                {
                    count++;
                    if (map[(Position.Item1, i)].Value >= Value)
                    {
                        break;
                    }
                }

                return count;
            }

            private int CalculateScenicScoreLeft(Dictionary<(int, int), Tree> map)
            {
                var count = 0;
                for (int i = Position.Item2 - 1; i >= 0; i--)
                {
                    count++;
                    if (map[(Position.Item1, i)].Value >= Value)
                    {
                        break;
                    }
                }

                return count;
            }

            public bool IsVisible(Dictionary<(int, int), Tree> map)
            {
                return IsVisibleFromTheLeft(map)
                    || IsVisibleFromTheRight(map)
                    || IsVisibleFromTheTop(map)
                    || IsVisibleFromTheBottom(map);
            }

            private bool IsVisibleFromTheBottom(Dictionary<(int, int), Tree> map)
            {
                for (int i = Position.Item1 + 1; i < Math.Sqrt(map.Count); i++)
                {
                    if (map[(i, Position.Item2)].Value >= Value)
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool IsVisibleFromTheTop(Dictionary<(int, int), Tree> map)
            {
                for (int i = 0; i < Position.Item1; i++)
                {
                    if (map[(i, Position.Item2)].Value >= Value)
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool IsVisibleFromTheRight(Dictionary<(int, int), Tree> map)
            {
                for (int i = Position.Item2 + 1; i < Math.Sqrt(map.Count); i++)
                {
                    if (map[(Position.Item1, i)].Value >= Value)
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool IsVisibleFromTheLeft(Dictionary<(int, int), Tree> map)
            {
                for (int i = 0; i < Position.Item2; i++)
                {
                    if(map[(Position.Item1, i)].Value >= Value)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
