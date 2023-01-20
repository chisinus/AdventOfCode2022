using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal static class Day1
    {
        public async static Task<string> Process(int topN, int seqNo)
        {
            List<string> data = await Utils.ReadFile(string.Format("day1_{0}.txt", seqNo));

            List<long> totals = new List<long>();
            long total = 0;
            foreach (string amt in data)
            {
                if (string.IsNullOrEmpty(amt))
                {
                    totals.Add(total);
                    total = 0;
                    continue;
                }

                total += int.Parse(amt);
            }

            totals.Sort();

            total = 0;
            for (int i=0; i<topN; i++)
                total += totals[totals.Count-i-1];

            return total.ToString();
        }
    }
}
