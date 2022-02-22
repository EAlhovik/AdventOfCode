using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day16
{
    class Task : BaseTask<long>
    {
        public override long ExpectedPart1Test { get; set; } = 20;
        public override long ExpectedPart2Test { get; set; } = 1;

        public override long SolvePart1(IEnumerable<string> input)
        {
            var binaryData = string.Join(string.Empty,
              input.First().Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            ).ToCharArray();

            var packages = ParsePackages(binaryData).SelectMany(GetAllPackages);
            return packages
                .Select(p => p.Version)
                .Sum();
        }

        public override long SolvePart2(IEnumerable<string> input)
        {
            var binaryData = string.Join(string.Empty,
              input.First().Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            ).ToCharArray();

            var packages = ParsePackages(binaryData);
            if(packages.Count != 1)
            {
                throw new Exception();
            }

            return packages[0].GetValue();
        }

        private static List<Package> GetAllPackages(Package package)
        {
            var result = new List<Package>();
            result.Add(package);
            if (package.Packages != null)
            {
                foreach (var item in package.Packages)
                {
                    result.AddRange(GetAllPackages(item));
                }
            }
            return result;
        }

        private List<Package> ParsePackages(char[] binaryData)
        {
            var result = ParseSubPackages(binaryData, 0, 0, binaryData.Length);
            return result.Item1;
        }

        private (List<Package>, int) ParseSubPackages(char[] binaryData, int i, int lengthTypeId, int lengthOfSubPackages)
        {
            var subPackages = new List<Package>();
            int j = i;
            for (; j < binaryData.Length;)
            {
                if (binaryData.Skip(j).Take(3 + 3 + 5).Count() < 11)
                {
                    break;
                }

                var subPackage = new Package();
                (subPackage.Version, j) = GetValue(j, binaryData, 3);
                (subPackage.Type, j) = GetValue(j, binaryData, 3);
                if (subPackage.Type == 4)
                {
                    var chunks = new List<string>();
                    do
                    {
                        string chunk;
                        (chunk, j) = GetBytes(j, binaryData, 5);
                        chunks.Add(string.Join(string.Empty, chunk.Skip(1)));
                        if (chunk[0] == '0')
                        {
                            break;
                        }
                    }
                    while (true);
                    subPackage.Value = BinaryToDecimal(string.Join(string.Empty, chunks));
                }
                else
                {
                    (subPackage.LengthTypeId, j) = GetValue(j, binaryData, 1);
                    (subPackage.LengthOfSubPackages, j) = GetValue(j, binaryData, subPackage.LengthTypeId == 0 ? 15 : 11);

                    (subPackage.Packages, j) = ParseSubPackages(binaryData, j, subPackage.LengthTypeId, subPackage.LengthOfSubPackages);
                }

                subPackages.Add(subPackage);

                if (lengthTypeId == 0 && (j - i) >= lengthOfSubPackages)
                {
                    break;
                }
                else if (lengthTypeId == 1 && subPackages.Count == lengthOfSubPackages)
                {
                    break;
                }
            }

            return (subPackages, j);
        }

        private (string, int) GetBytes(int i, char[] binaryData, int length)
        {
            var value = string.Join(string.Empty, binaryData.Skip(i).Take(length));
            return (value, i + length);
        }
        private (int, int) GetValue(int i, char[] binaryData, int length)
        {
            var value = string.Join(string.Empty, binaryData.Skip(i).Take(length));
            return ((int)BinaryToDecimal(value), i + length);
        }

        private static long BinaryToDecimal(string binary)
        {
            return Convert.ToInt64(binary, 2);
        }
        private class Package
        {
            public int Version { get; set; }
            public int Type { get; set; }
            public int LengthTypeId { get; set; }
            public int LengthOfSubPackages { get; set; }

            public long Value { get; set; }

            public List<Package> Packages { get; set; }

            public long GetValue()
            {
                var values = Packages != null
                    ? Packages.Select(p => p.GetValue()).ToList()
                    : null;
                switch (Type)
                {
                    case 0: return values.Sum();
                    case 1: return values.Aggregate(1L, (a, b) => a * b);
                    case 2: return values.Min();
                    case 3: return values.Max();
                    case 4: return Value;
                    case 5: return values[0] > values[1] ? 1 : 0;
                    case 6: return values[0] < values[1] ? 1 : 0;
                    case 7: return values[0] == values[1] ? 1 : 0;
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
