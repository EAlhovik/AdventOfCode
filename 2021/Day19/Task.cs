using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace _2021.Day19
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 79;

        public override int ExpectedPart2Test { get; set; } = 3621;

        public override int SolvePart1(IEnumerable<string> input)
        {
            var scanners = new List<List<Vector3>>();
            input.ToList().ForEach(p =>
            {
                if (p.Contains("---"))
                {
                    scanners.Add(new List<Vector3>());
                }
                else if (!string.IsNullOrWhiteSpace(p))
                {
                    var coords = p.Split(',').Select(float.Parse).ToList();
                    scanners.Last().Add(new Vector3(coords[0], coords[1], coords[2]));
                }
            });

            var knownScanners = new List<Vector3> { new Vector3(0, 0, 0) };
            var beacons = scanners[0];
            scanners.RemoveAt(0);

            Vector3 Rotate(Vector3 v, int direction)
            {
                var rotated = direction switch
                {
                    0 => (v.X, v.Y, v.Z),
                    1 => (v.Y, v.Z, v.X),
                    2 => (-v.Y, v.X, v.Z),
                    3 => (-v.X, -v.Y, v.Z),
                    4 => (v.Y, -v.X, v.Z),
                    5 => (v.Z, v.Y, -v.X),
                    6 => (v.Z, v.X, v.Y),
                    7 => (v.Z, -v.Y, v.X),
                    8 => (v.Z, -v.X, -v.Y),
                    9 => (-v.X, v.Y, -v.Z),
                    10 => (v.Y, v.X, -v.Z),
                    11 => (v.X, -v.Y, -v.Z),
                    12 => (-v.Y, -v.X, -v.Z),
                    13 => (-v.Z, v.Y, v.X),
                    14 => (-v.Z, v.X, -v.Y),
                    15 => (-v.Z, -v.Y, -v.X),
                    16 => (-v.Z, -v.X, v.Y),
                    17 => (v.X, -v.Z, v.Y),
                    18 => (-v.Y, -v.Z, v.X),
                    19 => (-v.X, -v.Z, -v.Y),
                    20 => (v.Y, -v.Z, -v.X),
                    21 => (v.X, v.Z, -v.Y),
                    22 => (-v.Y, v.Z, -v.X),
                    23 => (-v.X, v.Z, v.Y),
                };
                return new Vector3(rotated.Item1, rotated.Item2, rotated.Item3);
            }

            for (int i = 0; i < 10; i++)
            {
                foreach (var scanner in scanners)
                {
                    for (int orientation = 0; orientation < 24; orientation++)
                    {
                        var unknownScanner = scanner.Select(p => Rotate(p, orientation)).ToList();
                        foreach (var knownScanner in knownScanners)
                        {
                            foreach (var offset in unknownScanner.Select(p => knownScanner - p))
                            {
                                var t = unknownScanner.Select(p => offset + p).ToList();
                                if(beacons.Intersect(t).Count() >= 12)
                                {
                                    scanners.Remove(scanner);
                                    knownScanners.Add(offset);
                                    beacons.AddRange(t);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            var distances = from s in knownScanners
                            from s2 in knownScanners
                            select Math.Abs(s.X - s2.X) + Math.Abs(s.Y - s2.Y) + Math.Abs(s.Z - s2.Z);
            var w = (beacons.Count(), (long)distances.Max());
            return 2;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            return 1;
        }
    }
    class Task1 : BaseTask<int>
    {
        private class Scanner
        {
            public (int, int, int)? ScannerPosition { get; set; } = null;
            public Func<(int, int, int), (int, int, int)> Rotate = p => (p.Item1, p.Item2, p.Item3);
            public Scanner RelativeScanner { get; set; }
            public string Name { get; set; }

            public List<(int, int, int)> Beacons { get; set; } = new List<(int, int, int)>();

            public IList<Func<(int, int, int), (int, int, int)>> GetRotationsFunctions()
            {
                return new List<Func<(int, int, int), (int, int, int)>>
                {
                    //{ p => ((-1)*p.Item1, (1)*p.Item2, (-1)*p.Item3) },
                    

                    { p => ((1)*p.Item1, (1)*p.Item2, (1)*p.Item3) },
                    { p => ((1)*p.Item1, (1)*p.Item2, (-1)*p.Item3) },
                    { p => ((1)*p.Item1, (-1)*p.Item2, (1)*p.Item3) },
                    { p => ((1)*p.Item1, (-1)*p.Item2, (-1)*p.Item3) },
                    { p => ((-1)*p.Item1, (1)*p.Item2, (1)*p.Item3) },
                    { p => ((-1)*p.Item1, (1)*p.Item2, (-1)*p.Item3) },
                    { p => ((-1)*p.Item1, (-1)*p.Item2, (1)*p.Item3) },
                    { p => ((-1)*p.Item1, (-1)*p.Item2, (-1)*p.Item3) },

                    { p => ((1)*p.Item1,   (1)*p.Item3,  (1)*p.Item2) },
                    { p => ((1)*p.Item1,   (1)*p.Item3, (-1)*p.Item2) },
                    { p => ((1)*p.Item1,  (-1)*p.Item3,  (1)*p.Item2) },
                    { p => ((1)*p.Item1,  (-1)*p.Item3, (-1)*p.Item2) },
                    { p => ((-1)*p.Item1,  (1)*p.Item3,  (1)*p.Item2) },
                    { p => ((-1)*p.Item1,  (1)*p.Item3, (-1)*p.Item2) },
                    { p => ((-1)*p.Item1, (-1)*p.Item3,  (1)*p.Item2) },
                    { p => ((-1)*p.Item1, (-1)*p.Item3, (-1)*p.Item2) },

                    { p => ((1)*p.Item2, (1)*p.Item3, (1)*p.Item1) },
                    { p => ((1)*p.Item2, (1)*p.Item3, (-1)*p.Item1) },
                    { p => ((1)*p.Item2, (-1)*p.Item3, (1)*p.Item1) },
                    { p => ((1)*p.Item2, (-1)*p.Item3, (-1)*p.Item1) },
                    { p => ((-1)*p.Item2, (1)*p.Item3, (1)*p.Item1) },
                    { p => ((-1)*p.Item2, (1)*p.Item3, (-1)*p.Item1) },
                    { p => ((-1)*p.Item2, (-1)*p.Item3, (1)*p.Item1) },
                    { p => ((-1)*p.Item2, (-1)*p.Item3, (-1)*p.Item1) },

                    { p => ((1)*p.Item2,   (1)*p.Item1,  (1)*p.Item3) },
                    { p => ((1)*p.Item2,   (1)*p.Item1, (-1)*p.Item3) },
                    { p => ((1)*p.Item2,  (-1)*p.Item1,  (1)*p.Item3) },
                    { p => ((1)*p.Item2,  (-1)*p.Item1, (-1)*p.Item3) },
                    { p => ((-1)*p.Item2,  (1)*p.Item1,  (1)*p.Item3) },
                    { p => ((-1)*p.Item2,  (1)*p.Item1, (-1)*p.Item3) },
                    { p => ((-1)*p.Item2, (-1)*p.Item1,  (1)*p.Item3) },
                    { p => ((-1)*p.Item2, (-1)*p.Item1, (-1)*p.Item3) },

                    { p => ((1)*p.Item3, (1)*p.Item1, (1)*p.Item2) },
                    { p => ((1)*p.Item3, (1)*p.Item1, (-1)*p.Item2) },
                    { p => ((1)*p.Item3, (-1)*p.Item1, (1)*p.Item2) },
                    { p => ((1)*p.Item3, (-1)*p.Item1, (-1)*p.Item2) },
                    { p => ((-1)*p.Item3, (1)*p.Item1, (1)*p.Item2) },
                    { p => ((-1)*p.Item3, (1)*p.Item1, (-1)*p.Item2) },
                    { p => ((-1)*p.Item3, (-1)*p.Item1, (1)*p.Item2) },
                    { p => ((-1)*p.Item3, (-1)*p.Item1, (-1)*p.Item2) },

                    { p => ((1)*p.Item3,   (1)*p.Item2,  (1)*p.Item1) },
                    { p => ((1)*p.Item3,   (1)*p.Item2, (-1)*p.Item1) },
                    { p => ((1)*p.Item3,  (-1)*p.Item2,  (1)*p.Item1) },
                    { p => ((1)*p.Item3,  (-1)*p.Item2, (-1)*p.Item1) },
                    { p => ((-1)*p.Item3,  (1)*p.Item2,  (1)*p.Item1) },
                    { p => ((-1)*p.Item3,  (1)*p.Item2, (-1)*p.Item1) },
                    { p => ((-1)*p.Item3, (-1)*p.Item2,  (1)*p.Item1) },
                    { p => ((-1)*p.Item3, (-1)*p.Item2, (-1)*p.Item1) },
                };
            }

            public void AddBeacon(string coord)
            {
                var coords = coord.Split(',').Select(int.Parse).ToList();
                Beacons.Add((coords[0], coords[1], coords[2]));
            }
        }

        public override int ExpectedPart1Test { get; set; } = 4140;
        public override int ExpectedPart2Test { get; set; } = 3993;
        public override int SolvePart1(IEnumerable<string> input)
        {
            //        var scans = string.Join(" ", input).Split("  ")
            //.Select(s => s.Split("--- ").Last().Split(" ")
            //    .Select(l => l.Split(",").Select(s => float.Parse(s)).ToArray())
            //    .Select(f => new Vector3(f[0], f[1], f[2])).ToArray())
            //.ToList();

            //        Vector3 Rotate(Vector3 v, int direction)
            //        {
            //            var rotated = direction switch
            //            {
            //                0 => (v.X, v.Y, v.Z),
            //                1 => (v.Y, v.Z, v.X),
            //                2 => (-v.Y, v.X, v.Z),
            //                3 => (-v.X, -v.Y, v.Z),
            //                4 => (v.Y, -v.X, v.Z),
            //                5 => (v.Z, v.Y, -v.X),
            //                6 => (v.Z, v.X, v.Y),
            //                7 => (v.Z, -v.Y, v.X),
            //                8 => (v.Z, -v.X, -v.Y),
            //                9 => (-v.X, v.Y, -v.Z),
            //                10 => (v.Y, v.X, -v.Z),
            //                11 => (v.X, -v.Y, -v.Z),
            //                12 => (-v.Y, -v.X, -v.Z),
            //                13 => (-v.Z, v.Y, v.X),
            //                14 => (-v.Z, v.X, -v.Y),
            //                15 => (-v.Z, -v.Y, -v.X),
            //                16 => (-v.Z, -v.X, v.Y),
            //                17 => (v.X, -v.Z, v.Y),
            //                18 => (-v.Y, -v.Z, v.X),
            //                19 => (-v.X, -v.Z, -v.Y),
            //                20 => (v.Y, -v.Z, -v.X),
            //                21 => (v.X, v.Z, -v.Y),
            //                22 => (-v.Y, v.Z, -v.X),
            //                23 => (-v.X, v.Z, v.Y),
            //            };
            //            return new Vector3(rotated.Item1, rotated.Item2, rotated.Item3);
            //        }

            //        var scanners = new List<Vector3> { new Vector3(0, 0, 0) };
            //        var system = scans[0].ToArray();
            //        scans.RemoveAt(0);

            //        do
            //        {
            //            var linked = from match in scans
            //                         from orient in Enumerable.Range(0, 24)
            //                         let rotated = match.Select(s => Rotate(s, orient)).ToArray()
            //                         from vector in system
            //                         from offset in rotated.Select(s => vector - s)
            //                         let translated = rotated.Select(s => s + offset).ToArray()
            //                         where system.Intersect(translated).Count() >= 12
            //                         select (match, translated, offset);

            //            var link = linked.First();
            //            system = system.Union(link.translated).ToArray();
            //            scans.Remove(link.match);
            //            scanners.Add(link.offset);
            //        }
            //        while (scans.Count > 0);

            //        var distances = from s in scanners
            //                        from s2 in scanners
            //                        select Math.Abs(s.X - s2.X) + Math.Abs(s.Y - s2.Y) + Math.Abs(s.Z - s2.Z);
            // https://www.reddit.com/r/adventofcode/comments/rjpf7f/comment/hp7xftl/?utm_source=share&utm_medium=web2x&context=3
            //var w = (system.Count(), (long)distances.Max());
            var scanners = GetScanners(input)
                .Take(5)
                .ToList();
            scanners[0].ScannerPosition = (0, 0, 0);
            var beacons = scanners[0].Beacons.ToList();
            //for (int i = 0; i < 3; i++)
            foreach (var knownScanner in scanners.Where(p => p.ScannerPosition != null))
            {
                foreach (var unknownScanner in scanners.Where(p => p.ScannerPosition == null))
                {
                    if (unknownScanner.ScannerPosition != null) break;
                    foreach (var rotate in unknownScanner.GetRotationsFunctions())
                    {
                        if (unknownScanner.ScannerPosition != null) break;
                        foreach (var p1 in GetPossibleScannerPositions(beacons, unknownScanner.Beacons.Select(rotate)))
                            //foreach (var p1 in GetPossibleScannerPositions(knownScanner.Beacons.Select(knownScanner.Rotate), unknownScanner.Beacons.Select(rotate)))
                            {

                            var scannerPosition = p1;
                            //var scannerPosition = unknownScanner.Name.Contains("scanner 2")
                            //    ? (1105, -1205, 1229)
                            //    : p1;

                            var intersections = unknownScanner.Beacons
                                    .Select(rotate)
                                    .Select(p => (p.Item1 + scannerPosition.Item1, p.Item2 + scannerPosition.Item2, p.Item3 + scannerPosition.Item3))
                                    .Intersect(beacons)
                                    .ToList()
                                    ;
                            if (intersections.Count >= 12)
                            {
                                unknownScanner.ScannerPosition = scannerPosition;
                                unknownScanner.Rotate = rotate;
                                unknownScanner.RelativeScanner = knownScanner;
                                beacons.AddRange(unknownScanner.Beacons.Select(rotate));
                                var relativeScanner = knownScanner;
                                while (relativeScanner != null)
                                {
                                    unknownScanner.ScannerPosition = (
                                        unknownScanner.ScannerPosition.Value.Item1 + relativeScanner.ScannerPosition.Value.Item1,
                                        unknownScanner.ScannerPosition.Value.Item2 + relativeScanner.ScannerPosition.Value.Item2,
                                        unknownScanner.ScannerPosition.Value.Item3 + relativeScanner.ScannerPosition.Value.Item3);
                                    relativeScanner = relativeScanner.RelativeScanner;
                                }


                                break;
                            }
                        }

                    }

                }
            }
            return 1;
        }

        private IEnumerable<(int, int, int)> GetPossibleScannerPositions(IEnumerable<(int, int, int)> knownScannerBeacons, IEnumerable<(int, int, int)> unknownScannerBeacons)
        {
            var scannerPositions = new List<(int, int, int)>();
            foreach (var knownScannerBeacon in knownScannerBeacons)
            {
                foreach (var unknownScannerBeacon in unknownScannerBeacons)
                {
                    var scannerPosition = (
                            knownScannerBeacon.Item1 - unknownScannerBeacon.Item1,
                            knownScannerBeacon.Item2 - unknownScannerBeacon.Item2,
                            knownScannerBeacon.Item3 - unknownScannerBeacon.Item3);
                    scannerPositions.Add(scannerPosition);
                }
            }
            return scannerPositions.Distinct().ToList();
        }

        private List<Scanner> GetScanners(IEnumerable<string> input)
        {
            var scanners = new List<Scanner>();
            input.ToList().ForEach(p =>
            {
                if (p.Contains("---"))
                {
                    scanners.Add(new Scanner { Name = p });
                }
                else if (!string.IsNullOrWhiteSpace(p))
                {
                    scanners.Last().AddBeacon(p);
                }
            });
            return scanners;
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            return 2;
        }
    }
}
