using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal static class Day3
    {
        internal async static Task<string> Process_1()
        {
            List<string> data = await Utils.ReadFile("day3_1.txt");

            List<char> chars = new List<char>();
            foreach (string s in data)
            {
                chars.AddRange(Process1(s));
            }

            return Calculate(chars).ToString();
        }

        private static List<char> Process1(string s)
        {
            List<char> first = s.Substring(0, s.Length/2).ToCharArray().Distinct().ToList();
            List<char> second = s.Substring(s.Length / 2).ToCharArray().Distinct().ToList();

            List<char> result = new List<char>();
            foreach (char c in first)
            {
                if (second.IndexOf(c) == -1) continue;

                result.Add(c);
            }

            return result;
        }

        private static int Calculate(List<char> chars)
        {
            int total = 0;
            foreach (char c in chars)
            {
                if (c >= 'a' && c <= 'z')
                    total += c - 'a' + 1;
                else
                    total += c - 'A' + 27;
            }

            return total;
        }

        internal async static Task<string> Process_2()
        {
            List<string> data = await Utils.ReadFile("day3_1.txt");

            int i = 0;
            List<char> chars = new List<char>();
            while (i < data.Count-2)
            {
                char c = Process2(data[i], data[i + 1], data[i + 2]);
                if (c != '\0') chars.Add(c);

                i+= 3;
            }

            return Calculate(chars).ToString();
        }

        private static char Process2(string s1, string s2, string s3)
        {
            List<char> first = s1.ToCharArray().Distinct().ToList();
            List<char> second = s2.ToCharArray().Distinct().ToList();
            List<char> third = s3.ToCharArray().Distinct().ToList();

            foreach (char c in first)
            {
                if (second.IndexOf(c) >=0 && third.IndexOf(c)>=0) return c;
            }

            return '\0';
        }
    }
}
