using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

static class Utils
{
    public static T NextEnum<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }

    public static T PrevEnum<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) - 1;
        return (j == -1) ? Arr[Arr.Length - 1] : Arr[j];
    }

    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)
    {
        int index = 0;
        foreach (T item in sequence)
        {
            action(item, index);
            index++;
        }
    }

    public static List<string> GetInputLines(string day)
    {
        return GetAllLines(day, "input.txt");
    }

    public static List<string> GetTestLines(string day)
    {
        return GetAllLines(day, "test.txt");
    }

    private static List<string> GetAllLines(string day, string fileName)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..", day, fileName);
        return File.ReadAllLines(path).ToList();
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
