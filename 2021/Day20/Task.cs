using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day20
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 35;

        public override int ExpectedPart2Test { get; set; } = 3351;

        public override int SolvePart1(IEnumerable<string> input)
        {
            return Solve(input, 2);
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            return Solve(input, 50);
        }

        private int Solve(IEnumerable<string> input, int iterations)
        {
            var iea = input.ElementAt(0).Select(p => p == '#' ? (byte)1 : (byte)0).ToList();
            Dictionary<(int, int), byte> inputImage = input.Skip(2)
                .SelectMany((row, rowIndex) =>
                {
                    return row.Select((value, columnIndex) => (new
                    {
                        row = rowIndex,
                        column = columnIndex,
                        value = GetByte(value)
                    }));
                })
                .ToDictionary(p => (p.row, p.column), c => c.value);
            for (int i = 0; i < iterations; i++)
                inputImage = ExpendImage(inputImage);
            for (int i = 0; i < iterations; i++)
            {
                Dictionary<(int, int), byte> image = new Dictionary<(int, int), byte>();
                var defaultValue = iea[0] == 0 ? (byte)0 : (i % 2 == 0 ? (byte)0 : (byte)1);

                foreach (var imageItem in inputImage)
                {
                    var index = GetPixelIndex(imageItem.Key.Item1, imageItem.Key.Item2, inputImage, defaultValue);
                    image.Add((imageItem.Key.Item1, imageItem.Key.Item2), iea[index]);
                }

                inputImage = image;
            }

            return inputImage.Count(p => p.Value == 1);
        }

        private byte GetByte(char value)
        {
            return value == '#' ? (byte)1 : (byte)0;
        }

        private Dictionary<(int, int), byte> ExpendImage(Dictionary<(int, int), byte> inputImage)
        {
            var topRow = inputImage.Select(p => p.Key.Item1).Min() - 1;
            var bottomRow = inputImage.Select(p => p.Key.Item1).Max() + 1;
            var leftCol = inputImage.Select(p => p.Key.Item2).Min() - 1;
            var rightCol = inputImage.Select(p => p.Key.Item2).Max() + 1;

            for (int i = leftCol; i <= rightCol; i++)
            {
                inputImage.TryAdd((topRow, i), 0);
                inputImage.TryAdd((bottomRow, i), 0);
            }
            for (int i = topRow; i <= bottomRow; i++)
            {
                inputImage.TryAdd((i, leftCol), 0);
                inputImage.TryAdd((i, rightCol), 0);
            }
            return inputImage;
        }

        private int GetPixelIndex(int x, int y, Dictionary<(int, int), byte> image, byte defaultValue)
        {
            var binaryNumber = GetAdjacentCoords(x, y).Select(p =>
            {
                if (image.TryGetValue((p.Item1, p.Item2), out var value))
                {
                    return value;
                }
                else
                {
                    return defaultValue;
                }
            });

            return Convert.ToInt32(string.Join(string.Empty, binaryNumber).ToString(), 2);
        }

        private List<(int, int)> GetAdjacentCoords(int row, int col)
        {
            return new List<(int, int)>
            {
                { (row - 1, col - 1) },
                { (row - 1, col - 0) },
                { (row - 1, col + 1) },

                { (row + 0, col - 1) },
                { (row + 0, col - 0) },
                { (row + 0, col + 1) },

                { (row + 1, col - 1) },
                { (row + 1, col - 0) },
                { (row + 1, col + 1) },
            };
        }
    }
}
