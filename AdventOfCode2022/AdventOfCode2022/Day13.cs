using System.Text.Json;

namespace AdventOfCode2022
{
    internal class Day13
    {
        internal async Task<string> Process_1()
        {
            List<string> data = await Utils.ReadFile("day13_1.txt");

            int i = 0;
            int result = 0;
            while (i < data.Count)
            {
                if (CheckOrder(data[i], data[i + 1]) < 0)
                {
                    result += i / 3 + 1;
                }

                i += 3;
            }

            return result.ToString();
        }

        internal async Task<string> Process_2()
        {
            List<string> data = await Utils.ReadFile("day13_1.txt", true);
            data.Add("[[2]]");
            data.Add("[[6]]");

            data.Sort(delegate (string x, string y) { return CheckOrder(x, y); });

            int i2 = data.IndexOf("[[2]]") + 1;
            int i6 = data.IndexOf("[[6]]") + 1;

            return (i2 * i6).ToString();
        }

        private int CheckOrder(string left, string right)
        {
            List<string> leftElements = Parse(left);
            List<string> rightElements = Parse(right);

            int result = 0;
            for (int i = 0; i < leftElements.Count; i++)
            {
                if (i > rightElements.Count - 1) break;

                if ((leftElements[i][0] == '[') && rightElements[i][0] != '[')          // left is a set and right is not
                    rightElements[i] = $"[{rightElements[i]}]";                         // convert right to a set
                else if ((leftElements[i][0] != '[') && rightElements[i][0] == '[')     // right is a set and left is not
                    leftElements[i] = $"[{leftElements[i]}]";                            // convert left to a set

                if (leftElements[i][0] == '[')
                    result = CheckOrder(leftElements[i], rightElements[i]);
                else
                    result = int.Parse(leftElements[i]) - int.Parse(rightElements[i]);

                if (result != 0) break;
            }

            if (result == 0) result = leftElements.Count - rightElements.Count;

            return result;
        }

        private List<string> Parse(string s)
        {
            List<string> result = new List<string>();

            if (s.Length == 0) return result;

            var elements = JsonSerializer.Deserialize<JsonElement>(s).EnumerateArray();
            while (elements.MoveNext())
            {
                result.Add(elements.Current.ToString());
            }

            return result;
        }
    }
}

