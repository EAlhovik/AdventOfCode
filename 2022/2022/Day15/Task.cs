using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _2022.Day15
{
    internal class Task : BaseTask<long>
    {
        public Task() : base(26, 56000011) { }

        public override long SolvePart1(List<string> input)
        {
            var map = new Dictionary<Point, string>();
            var sensors = GetSensors(input);
            sensors.ForEach(p =>
            {
                map.TryAdd(p.Position, "S");
                map.TryAdd(p.Beacon, "B");
            });
            int targetLineY = input.Count > 27 ? 2_000_000 : 10;
            foreach (var sensor in sensors)
            {
                DrawSensor(map, sensor, targetLineY);
            }

            return map.Values.Where(p => p == "#").Count();
        }

        public override long SolvePart2(List<string> input)
        {
            var sensors = GetSensors(input);
            var max = 4_000_000;

            var map = new Dictionary<Point, string>();
            foreach (var sensor in sensors)
            {
                DrawSensor(map, sensor, 2);
            }

            for (int targetLineY = 1; targetLineY < max; targetLineY++)
            {
                var range = sensors.Select(sensor => GetRange(sensor, targetLineY))
                    .Where(p => p != null)
                    .OrderBy(p => p.Start)
                    .Aggregate(new Range(), (accumulate, next) => accumulate.Union(next));

                if(range.Gap.End - range.Gap.Start > 0)
                {
                    long x = range.Gap.Start + 1;
                    return x * max + targetLineY;
                }
            }

            throw new Exception("didn't solve the problem");
        }

        private Range GetRange(Sensor s, int targetLineY)
        {
            Point sensor = s.Position;
            if ((sensor.Y > targetLineY && sensor.Y - s.N <= targetLineY))
            { // below sensor
                var y = sensor.Y + s.N * (-1);
                var a1 = -1;
                var a2 = 1;
                var x1 = (targetLineY - (y - a1 * sensor.X)) / a1;
                var x2 = (targetLineY - (y - a2 * sensor.X)) / a2;

                return new Range(x1, x2);
            }
            else if (sensor.Y < targetLineY && sensor.Y + s.N >= targetLineY)
            { // above sensor
                var y = sensor.Y + s.N * (1);
                var a1 = 1;
                var a2 = -1;

                var x1 = (targetLineY - (y - a1 * sensor.X)) / a1;
                var x2 = (targetLineY - (y - a2 * sensor.X)) / a2;

                return new Range(x1, x2);
            }
            else if (sensor.Y == targetLineY)
            { // the same level
                var x1 = sensor.X - s.N;
                var x2 = sensor.X + s.N;
                return new Range(x1, x2);
            }
            else
            {
                return null;
            }
        }

        private void DrawSensor(Dictionary<Point, string> map, Sensor s, int targetLineY)
        {
            Point sensor = s.Position;
            if (s.N == 0)
            {
                Point beacon = s.Beacon;
                var beaconPosition = beacon.X >= sensor.X && beacon.Y >= sensor.Y
                    ? 1
                    : beacon.X >= sensor.X && beacon.Y <= sensor.Y
                        ? 2
                        : beacon.X <= sensor.X && beacon.Y <= sensor.Y
                            ? 3
                            : beacon.X <= sensor.X && beacon.Y >= sensor.Y
                                ? 4
                                : 0;

                int a = beaconPosition switch
                {
                    1 => -1,
                    2 => 1,
                    3 => -1,
                    4 => 1,
                    _ => throw new NotImplementedException()
                };
                int n = beaconPosition switch
                {
                    1 => ((sensor.Y - GetB(a, beacon)) / a) - sensor.X,
                    2 => (sensor.Y - GetB(a, beacon)) / a - sensor.X,
                    3 => sensor.X - ((sensor.Y - GetB(a, beacon)) / a),
                    4 => sensor.X - (sensor.Y - GetB(a, beacon)) / a,
                    _ => throw new NotImplementedException()
                };

                s.N = n;
            }

            if ((sensor.Y > targetLineY && sensor.Y - s.N <= targetLineY))
            { // below sensor
                DrawSignal(map, sensor, s.N * (-1), -1, 1, targetLineY);
            }
            else if (sensor.Y < targetLineY && sensor.Y + s.N >= targetLineY)
            { // above sensor
                DrawSignal(map, sensor, s.N * (1), 1, -1, targetLineY);
            }
            else if (sensor.Y == targetLineY)
            { // the same level
                for (int x = sensor.X - s.N; x <= sensor.X + s.N; x++) map.TryAdd(new Point(x, targetLineY), "#");
            }
        }

        private void DrawSignal(Dictionary<Point, string> map, Point sensor, int n, int a1, int a2, int targetLineY)
        {
            var y = sensor.Y + n;

            var x1 = (targetLineY - (y - a1 * sensor.X)) / a1;
            var x2 = (targetLineY - (y - a2 * sensor.X)) / a2;

            for (int x = x1; x <= x2; x++) map.TryAdd(new Point(x, targetLineY), "#");
        }

        private int GetB(int a, Point beacon)
        {
            return beacon.Y - a * beacon.X;
        }

        private List<Sensor> GetSensors(List<string> input)
        {
            return input
                .Select(p => p.Substring(10, p.Length - 10))
                .Select(p =>
                {
                    var coords = p.Split(": closest beacon is at ");
                    return new Sensor
                    {
                        Position = GetPosition(coords[0]),
                        Beacon = GetPosition(coords[1])
                    };
                })
                .ToList();
        }

        private Point GetPosition(string coord)
        {
            var x = int.Parse(coord.Split(", ")[0].Split("=")[1]);
            var y = int.Parse(coord.Split(", ")[1].Split("=")[1]);
            return new Point(x, y);
        }

        private class Sensor
        {
            public Point Position { get; set; }

            public Point Beacon { get; set; }

            public int N { get; set; }
        }

        public class Range
        {
            public Range() { }

            public Range(int start, int end)
            {
                Start = start;
                End = end;
                Gap = new Range();
            }

            public int Start { get; set; }

            public int End { get; set; }

            public Range Gap { get; set; }

            public Range Union(Range next)
            {
                var range = new Range(Math.Min(Start, next.Start), Math.Max(End, next.End));
                if(next.Start > End)
                {
                    range.Gap.Start = End;
                    range.Gap.End = next.Start;
                }

                if(Gap != null && Gap?.End != 0)
                {
                    if(Gap.Start < next.Start)
                    {
                        range.Gap.Start = Gap.Start;
                        range.Gap.End = Math.Min(Gap.End, next.Start);
                    }
                }

                return range;
            }
        }
    }
}
