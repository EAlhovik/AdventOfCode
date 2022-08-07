using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2021
{
    static class Utils
    {
        public static IEnumerable<string> GetInputLines(string day)
        {
            return GetAllLines(day, "input.txt");
        }

        public static IEnumerable<string> GetTestLines(string day)
        {
            return GetAllLines(day, "test.txt");
        }

        private static IEnumerable<string> GetAllLines(string day, string fileName)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..", day, fileName);
            return File.ReadAllLines(path);
        }

        public static IEnumerable<string> SplitBy(this string str, int chunkLength)
        {
            if (string.IsNullOrEmpty(str)) throw new ArgumentException();
            if (chunkLength < 1) throw new ArgumentException();

            for (int i = 0; i < str.Length; i += chunkLength)
            {
                if (chunkLength + i > str.Length)
                    chunkLength = str.Length - i;

                yield return str.Substring(i, chunkLength);
            }
        }
        public static IEnumerable<List<TValue>> Chunk<TValue>(
        this IEnumerable<TValue> values,
        int chunkSize)
        {
            return values
                   .Select((v, i) => new { v, groupIndex = i / chunkSize })
                   .GroupBy(x => x.groupIndex)
                   .Select(g => g.Select(x => x.v).ToList());
        }
    }
}
