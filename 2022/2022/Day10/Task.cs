using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2022.Day10
{
    internal class Task : BaseTask<int>
    {
        public Task() : base(13140, 1) { }

        public override int SolvePart1(List<string> input)
        {
            var x = 1;
            var cycle = 0;
            var signalStrength = new Dictionary<int, int>();
            foreach (var instruction in input)
            {
                if(instruction == "noop")
                {
                    cycle++;
                    signalStrength.Add(cycle, cycle * x);
                }
                else
                {
                    cycle++;
                    signalStrength.Add(cycle, cycle * x);
                    cycle++;
                    signalStrength.Add(cycle, cycle * x);
                    x += int.Parse(instruction.Split(' ')[1]);
                }
            }

            return signalStrength[20]
                + signalStrength[60]
                + signalStrength[100]
                + signalStrength[140]
                + signalStrength[180]
                + signalStrength[220];
        }

        public override int SolvePart2(List<string> input)
        {
            var x = 1;
            var cycle = 0;
            var signalStrength = new Dictionary<int, int>();
            var crtRow = new StringBuilder();
            foreach (var instruction in input)
            {
                if (instruction == "noop")
                {
                    DrawPixel(crtRow, cycle, x);
                    cycle++;
                    signalStrength.Add(cycle, cycle * x);
                }
                else
                {
                    DrawPixel(crtRow, cycle, x);
                    cycle++;
                    signalStrength.Add(cycle, cycle * x);
                    DrawPixel(crtRow, cycle, x);
                    cycle++;
                    signalStrength.Add(cycle, cycle * x);
                    x += int.Parse(instruction.Split(' ')[1]);
                }
            }
            var image = crtRow.ToString().Chunk(40).ToList();
            foreach (var row in image)
            {
                Console.WriteLine(string.Join("", row));
            }
            Console.WriteLine(string.Empty);

            return 1;
        }

        private void DrawPixel(StringBuilder crtRow, int cycle, int x)
        {
            if(Math.Abs( x - (cycle % 40) ) <= 1)
            {
                crtRow.Append("#");
            }
            else
            {
                crtRow.Append(".");
            }
        }
    }
}
