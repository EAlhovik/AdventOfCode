using System;
using System.Collections.Generic;
using System.Linq;

namespace _2022.Day05
{
    internal class Task : BaseTask<string>
    {
        public Task() : base("CMZ", "MCD") { }

        public override string SolvePart1(List<string> input)
        {
            List<Stack<string>> ships = null;
            var index = input.FindIndex(string.IsNullOrEmpty);
            for (int i = index - 1; i >= 0; i--)
            {
                if(ships == null)
                {
                    var shipCount = int.Parse(input[i].Split(' ').Where(p => !string.IsNullOrWhiteSpace(p)).Last());
                    ships = Enumerable.Range(1, shipCount).Select(_ => new Stack<string>()).ToList();
                }
                else
                {
                    var row = input[i];
                    var shipIndex = 0;
                    for (int j = 0; j < row.Length; j+=4)
                    {
                        var crate = row.Substring(j + 1, 1);
                        if (!string.IsNullOrWhiteSpace(crate))
                        {
                            ships[shipIndex].Push(crate);
                        }
                        shipIndex++;
                    }
                }
            }

            for (int i = index + 1; i < input.Count; i++)
            {
                var r = input[i].Split(' ');
                var move = int.Parse(r[1]);
                var from = int.Parse(r[3]);
                var to = int.Parse(r[5]);

                for (int j = 0; j < move; j++)
                {
                    var create = ships[from - 1].Pop();
                    ships[to - 1].Push(create);
                }
            }

            return string.Join(string.Empty, ships.Select(p => p.Pop()));
        }

        public override string SolvePart2(List<string> input)
        {
            List<Stack<string>> ships = null;
            var index = input.FindIndex(string.IsNullOrEmpty);
            for (int i = index - 1; i >= 0; i--)
            {
                if (ships == null)
                {
                    var shipCount = int.Parse(input[i].Split(' ').Where(p => !string.IsNullOrWhiteSpace(p)).Last());
                    ships = Enumerable.Range(1, shipCount).Select(_ => new Stack<string>()).ToList();
                }
                else
                {
                    var row = input[i];
                    var shipIndex = 0;
                    for (int j = 0; j < row.Length; j += 4)
                    {
                        var crate = row.Substring(j + 1, 1);
                        if (!string.IsNullOrWhiteSpace(crate))
                        {
                            ships[shipIndex].Push(crate);
                        }
                        shipIndex++;
                    }
                }
            }

            for (int i = index + 1; i < input.Count; i++)
            {
                var r = input[i].Split(' ');
                var move = int.Parse(r[1]);
                var from = int.Parse(r[3]);
                var to = int.Parse(r[5]);

                var creates = new List<string>();
                for (int j = 0; j < move; j++)
                {
                    creates.Add(ships[from - 1].Pop());
                }

                creates.Reverse();
                creates.ForEach(create => ships[to - 1].Push(create));
                
            }

            return string.Join(string.Empty, ships.Select(p => p.Pop()));
        }
    }
}
