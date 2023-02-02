namespace AdventOfCode2022
{

    class Day19
    {

        #region Part1
        internal async Task<string> Process(bool includeAirCube)
        {
            List<string> data = await Utils.ReadFile("day19_1.txt");

            foreach (string line in data)
            {
                ProcessData(line);
            }

            int result = 0;
            return result.ToString();
        }

        private void ProcessData(string line)
        {
        }
        #endregion Part1

    }
}