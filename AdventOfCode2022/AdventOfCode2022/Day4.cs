using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal static class Day4
    {
        internal async static Task<string> Process_1()
        {
            List<string> data = await Utils.ReadFile("day4_1.txt");

            int count = 0;
            foreach (string s in data)
            {
                count += FullContains(s) ? 1 : 0;
            }

            return count.ToString();
        }

        private static bool FullContains(string s)
        {
            string first = s.Substring(0, s.IndexOf(','));
            string second = s.Substring(s.IndexOf(',')+1);

            int firstBegin = int.Parse(first.Substring(0, first.IndexOf('-')));
            int firstEnd = int.Parse(first.Substring(first.IndexOf('-')+1));
            int secondBegin = int.Parse(second.Substring(0, second.IndexOf('-')));
            int secondEnd = int.Parse(second.Substring(second.IndexOf('-')+1));

            return (firstBegin <= secondBegin && firstEnd >= secondEnd) ||
                    (secondBegin <= firstBegin && secondEnd >= firstEnd);
        }

        internal async static Task<string> Process_2()
        {
            List<string> data = await Utils.ReadFile("day4_1.txt");

            int count = 0;
            foreach (string s in data)
            {
                count += Overlaps(s) ? 1 : 0;
            }

            return count.ToString();
        }

        private static bool Overlaps(string s)
        {
            string first = s.Substring(0, s.IndexOf(','));
            string second = s.Substring(s.IndexOf(',') + 1);

            int firstBegin = int.Parse(first.Substring(0, first.IndexOf('-')));
            int firstEnd = int.Parse(first.Substring(first.IndexOf('-') + 1));
            int secondBegin = int.Parse(second.Substring(0, second.IndexOf('-')));
            int secondEnd = int.Parse(second.Substring(second.IndexOf('-') + 1));

            return (firstBegin >= secondBegin && firstBegin <= secondEnd) ||
                   (firstEnd >= secondBegin && firstEnd <= secondEnd) ||
                   (secondBegin >= firstBegin && secondBegin <= firstEnd) ||
                   (secondEnd >= firstBegin && secondEnd <= firstEnd);
                    ;
        }
    }
}
