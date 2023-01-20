using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal static class Day2
    {
        internal async static Task<string> Part1()
        {
            List<string> data = await Utils.ReadFile("day2_1.txt");

            int total = 0;
            foreach (string s in data)
            {
                total += Judge1(s);
            }

            return total.ToString();
        }

        private static int Judge1(string s)
        {
            int elf = s[0] - 'A' + 1;
            int me = s[2] - 'X' + 1;

            int result;
            if (elf == me) // draw
                result = me + 3;
            else if ((elf + 1  == me) || (elf == 3 && me == 1)) // win
                result = me + 6;
            else // lost
                result = me;

            return result;
        }

        internal async static Task<string> Part2()
        {
            List<string> data = await Utils.ReadFile("day2_1.txt");

            int total = 0;
            foreach (string s in data)
            {
                total += Judge2(s);
            }

            return total.ToString();
        }

        private static int Judge2(string s)
        {
            int elf = s[0] - 'A' + 1;
            char end = s[2];

            int result;
            if (end == 'X') // lost
                result = elf == 1 ? 3: elf - 1;
            else if (end == 'Y') // draw
                result = elf + 3;
            else // win
                result = elf == 3 ? 1 + 6 : elf + 1 + 6;


            return result;
        }
    }
}
