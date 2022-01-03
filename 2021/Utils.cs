using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    }
}
