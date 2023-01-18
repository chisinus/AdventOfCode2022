using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2022
{
    internal static class Day5
    {
        internal async static Task<string> Process(bool reverse)
        {
            List<string> data = await Utils.ReadFile("day5_1.txt");

            List<char>[] stacks = new List<char>[9];
            for (int i = 0; i < 9; i++)
                stacks[i] = new List<char>();

            foreach (string s in data)
            {
                if (s.Trim().Length > 0 && s.Trim()[0] == '[')
                    AssignStack(s, stacks);
                else if (s.StartsWith("move"))
                    ProcessInstruction(stacks, s, reverse);
            }

            string result = "";
            foreach (List<char> s in stacks)
            {
                if (s.Count == 0) continue;

                result += s[0];
            }
            
            return result;
        }

        private static void AssignStack(string s, List<char>[] stacks)
        {
            int i = s.IndexOf('[');
            while (i >= 0)
            {
                int pos = i / 4;
                stacks[pos].Add(s[i + 1]);

                i = s.IndexOf('[', i + 1);
            }
        }

        private static void ProcessInstruction(List<char>[] stacks, string s, bool reverse)
        {
            string[] values = s.Split(' ');

            int moves = int.Parse(values[1]);
            int from = int.Parse(values[3]) - 1;
            int to = int.Parse(values[5]) - 1;

            var crates = stacks[from].Take(moves).ToList();
            if (reverse) crates.Reverse();
            crates.AddRange(stacks[to]);
            stacks[to] = crates;
            stacks[from].RemoveRange(0, moves);
        }
    }
}
