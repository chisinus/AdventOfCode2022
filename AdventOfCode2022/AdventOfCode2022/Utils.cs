namespace AdventOfCode2022
{
    internal static class Utils
    {
        public static async Task<List<string>> ReadFile(string filename, bool ignoreBlankLine = false)
        {
            const string DataFolder = @"Z:\Projects\AdventOfCode2022\AdventOfCode2022\AdventOfCode2022\Data\";

            List<string> result = new List<string>();

            try
            {
                FileStream file = File.OpenRead(DataFolder + filename);
                StreamReader sr = new StreamReader(file);
                while (sr.Peek() != -1)
                {
                    string line = await sr.ReadLineAsync() ?? "";
                    if (!ignoreBlankLine || !string.IsNullOrWhiteSpace(line))
                        result.Add(line);
                }

                sr.Close();
            }
            catch (Exception e)
            {
                return new List<string>();
            }

            return result;
        }
    }
}
