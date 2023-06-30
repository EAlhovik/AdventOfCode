using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace _2022.Day13
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(13, 140) { }

        public override int SolvePart1(List<string> input)
        {
            var numbers = input.Chunk(3)
                .Select((p, index) => new
                {
                    index = index + 1,
                    left = p[0],
                    right = p[1],
                    isRightOrder = IsRightOrder(Parse(p[0]), Parse(p[1])) < 0
                })
                .ToList();

            return numbers.Where(p => p.isRightOrder).Sum(p => p.index);
        }

        public override int SolvePart2(List<string> input)
        {
            var two = Parse("[[2]]");
            var six = Parse("[[6]]");
            var items = new List<JsonElement> { two, six };
            items.AddRange(input
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(Parse));
            items.Sort(IsRightOrder);

            return (items.IndexOf(two) + 1) * (items.IndexOf(six) + 1);
        }

        private static int Compare(JsonElement leftJson, JsonElement rightJson)
        {
            return (leftJson.ValueKind, rightJson.ValueKind) switch
            {
                (JsonValueKind.Number, JsonValueKind.Number) =>
                    leftJson.GetInt32() - rightJson.GetInt32(),
                (_, JsonValueKind.Number) =>
                    IsRightOrder(leftJson, Parse($"[{rightJson}]")),
                (JsonValueKind.Number, _) =>
                    IsRightOrder(Parse($"[{leftJson}]"), rightJson),
                (_, _) =>
                    IsRightOrder(leftJson, rightJson),
            };
        }

        private static int IsRightOrder(JsonElement left, JsonElement right)
        {
            // -1 right order
            // 0 keep checking
            // 1 not right order
            var leftList = left.EnumerateArray().ToList();
            var rightList = right.EnumerateArray().ToList();

            for (int i = 0; i < Math.Min(leftList.Count, rightList.Count); i++)
            {
                var compareResult = Compare(leftList[i], rightList[i]);
                if (compareResult != 0)
                {
                    return compareResult;
                }
            }

            return leftList.Count - rightList.Count;
        }

        private static JsonElement Parse(string json)
        {
            return JsonSerializer.Deserialize<JsonElement>(json);
        }
    }
}
