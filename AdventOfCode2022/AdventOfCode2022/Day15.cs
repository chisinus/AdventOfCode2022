namespace AdventOfCode2022
{
    class Beacon
    {
        public Point Location { get; set; }
        public List<Point> Sensors { get; set; } = new List<Point>();
    }

    class Row
    {
        public int Y { get; set; }
        public List<Range> Ranges { get; set; } = new List<Range>();
    }

    internal class Day15
    {
        List<Beacon> beacons = new List<Beacon>();

        #region Part1
        internal async Task<string> Part1()
        {
            List<string> data = await Utils.ReadFile("day15_1.txt");

            foreach (string s in data)
            {
                ProcessData(s);
            }

            int result = FindLocations(2000000);

            return result.ToString();
        }

        private void ProcessData(string s)
        {
            int sensorXStart = s.IndexOf("x=") + 2;
            int sensorXEnd = s.IndexOf(",", sensorXStart);
            int sensorYStart = s.IndexOf("y=") + 2;
            int sensorYEnd = s.IndexOf(":", sensorYStart);
            int beaconXStart = s.IndexOf("x=", sensorYEnd) + 2;
            int beaconXEnd = s.IndexOf(",", beaconXStart);
            int beaconYStart = s.IndexOf("y=", beaconXEnd) + 2;

            int sensorX = int.Parse(s.Substring(sensorXStart, sensorXEnd - sensorXStart));
            int sensorY = int.Parse(s.Substring(sensorYStart, sensorYEnd - sensorYStart));
            int beaconX = int.Parse(s.Substring(beaconXStart, beaconXEnd - beaconXStart));
            int beaconY = int.Parse(s.Substring(beaconYStart));

            var beacon = beacons.Where(b => b.Location.X == beaconX && b.Location.Y == beaconY).FirstOrDefault();
            if (beacon == null)
            {
                beacon = new Beacon();
                beacon.Location = new Point(beaconX, beaconY);
            }

            beacon.Sensors.Add(new Point(sensorX, sensorY));

            beacons.Add(beacon);
        }

        private int FindLocations(int y)
        {
            Beacon target = beacons.Where(b => b.Location.Y == y).First();

            List<int> positions = new List<int>() { target.Location.X };

            foreach (Beacon beacon in beacons)
            {
                foreach (Point sensor in beacon.Sensors)
                {
                    int distance = Math.Abs(sensor.X - beacon.Location.X) + Math.Abs(sensor.Y - beacon.Location.Y);
                    int verticalDistance = Math.Abs(sensor.Y - target.Location.Y);
                    if (verticalDistance > distance) continue;

                    int xLimit = distance - verticalDistance;
                    if (sensor.X != beacon.Location.X)
                    {
                        for (int x = sensor.X - xLimit; x <= sensor.X + xLimit; x++)
                        {
                            //if (positions.IndexOf(x) == -1) positions.Add(x);
                            positions.Add(x);
                        }
                    }
                }
            }

            return positions.Distinct().Count() - 1;
        }
        #endregion Part1

        #region Part2
        const int minX = 0;
        const int maxX = 4000000;
        const int minY = 0;
        const int maxY = 4000000;

        List<Row> rows = new List<Row>();

        internal async Task<string> Part2()
        {
            List<string> data = await Utils.ReadFile("day15_1.txt");

            foreach (string s in data)
            {
                ProcessData(s);
            }

            for (int i = minY; i < maxY; i++)
            {
                rows.Add(new Row() { Y = i });
            }

            MarkCoveredAreas();

            foreach (Row row in rows)
            {
                row.Ranges = MergeRanges(row.Ranges);
            }

            Point beacon = FindTheBeacon();

            double result = beacon.X * (double)4000000 + beacon.Y;

            return result.ToString();
        }

        private void MarkCoveredAreas()
        {
            foreach (Beacon beacon in beacons)
            {
                foreach (Point sensor in beacon.Sensors)
                {
                    int distance = Math.Abs(sensor.X - beacon.Location.X) + Math.Abs(sensor.Y - beacon.Location.Y);

                    int startY = Math.Max(minY, sensor.Y - distance);
                    int endY = Math.Min(maxY - 1, sensor.Y + distance);

                    for (int y = startY; y <= endY; y++)
                    {
                        MarkLine(sensor, y, distance);
                    }
                }
            }
        }

        private void MarkLine(Point sensor, int y, int distance)
        {
            int remainDistance = distance - Math.Abs(sensor.Y - y);

            int xStart = Math.Max(minX, sensor.X - remainDistance);
            int xEnd = Math.Min(maxX - 1, sensor.X + remainDistance);
            rows[y].Ranges.Add(new Range(xStart, xEnd));
        }

        private List<Range> MergeRanges(List<Range> ranges)
        {
            List<Range> result = new List<Range>();

            bool rangesChanged = false;

            int start, end;

            Range mergedRange = ranges[0];

            int i = 1;
            while (i < ranges.Count)
            {
                MergeTwoRanges(mergedRange, ranges[i], out start, out end);
                if (start != -1)    // merged
                {
                    mergedRange = new Range(start, end);
                    rangesChanged = true;
                }
                else
                {
                    result.Add(ranges[i]);
                }

                i++;
            }

            result.Add(mergedRange);

            if (rangesChanged && result.Count > 1)
                result = MergeRanges(result);

            return result;
        }

        private bool MergeTwoRanges(Range r1, Range r2, out int start, out int end)
        {
            // overlapping?
            if ((r1.Start.Value > r2.End.Value + 1) || (r2.Start.Value > r1.End.Value + 1))
            {
                start = -1;
                end = -1;
                return false;
            }

            start = Math.Min(r1.Start.Value, r2.Start.Value);
            end = Math.Max(r1.End.Value, r2.End.Value);

            return true;
        }

        private Point FindTheBeacon()
        {
            foreach (Row row in rows)
            {
                if (row.Ranges.Count == 1) continue;    // assume it covers the whole row if there is just one range, otherwise check

                // assume the undetected beacon is always in the middle
                List<Range> ranges = row.Ranges.OrderBy(r => r.Start.Value).ToList();

                // assume only one correct answer
                return new Point(ranges[1].Start.Value - 1, row.Y);
            }

            return Point.Empty;
        }
        #endregion Part2
    }
}

