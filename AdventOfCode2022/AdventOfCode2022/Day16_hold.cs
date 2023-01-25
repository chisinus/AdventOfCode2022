namespace AdventOfCode2022
{
    class Valve
    {
        public string Name { get; set; } = "";
        public int Rate { get; set; }
        public List<Valve> Valves { get; set; } = new List<Valve>();
        public string ValvesString { get; set; } = "";
    }

    internal class Day16
    {
        List<Valve> valves = new List<Valve>();

        #region Part1
        internal async Task<string> Part1()
        {
            List<string> data = await Utils.ReadFile("day16_1.txt");

            foreach (string s in data)
            {
                ProcessData(s);
            }

            foreach (Valve valve in valves)
            {
                FixValveChildren(valve);
            }

            // Another path finding. will work on it later.

            // https://www.reddit.com/r/adventofcode/comments/zn6k1l/2022_day_16_solutions/

            int result = 0;

            return result.ToString();
        }

        private void ProcessData(string s)
        {
            Valve valve = new Valve();
            valve.Name = s.Substring(6, 2);
            valve.Rate = int.Parse(s.Substring(23, s.IndexOf(';') - s.IndexOf('=') - 1));
            if (s.IndexOf("valves") > 0)
                valve.ValvesString = s.Substring(s.IndexOf("valves") + 7);
            else
                valve.ValvesString = s.Substring(s.IndexOf("tunnel leads to valve") + 21);

            valves.Add(valve);
        }

        private void FixValveChildren(Valve valve)
        {
            string[] vList = valve.ValvesString.Split(',');
            foreach (string s in vList)
            {
                valve.Valves.Add(valves.Where(v => v.Name == s.Trim()).First());
            }
        }
        #endregion Part1

    }
}

