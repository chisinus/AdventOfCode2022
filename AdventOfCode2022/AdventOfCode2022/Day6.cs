using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2022
{
    internal static class Day6
    {
        internal async static Task<string> Process_1(int num)
        {
            List<string> data = await Utils.ReadFile("day6_1.txt");

            int result;
            for (result=num; result < data[0].Length; result++)
            {
                if (AllUnique(data[0], result, num)) break;
            }
            
            return result.ToString();
        }

        private static bool AllUnique(string s, int end, int num)
        {
            string s1 = s.Substring(end - num, num);
            return s1.Distinct().Count() == num;
        }
    }
}
