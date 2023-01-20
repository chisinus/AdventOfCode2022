using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace AdventOfCode2022
{
    internal class Day10
    {
        int regX = 1;
        int[] cycleStages = new int[] { 20, 60, 100, 140, 180, 220 };
        List<int> stageTotal = new List<int>();
        int currentStage = 0;
        int totalCycles = 0;

        #region Day10_Part1
        internal async Task<string> Part1()
        {
            List<string> data = await Utils.ReadFile("day10_1.txt");
            foreach (string s in data)
            {
                ExecuteCommand(s);
                if (currentStage == cycleStages.Length) break;
            }

            return stageTotal.Sum().ToString();
        }

        private void ExecuteCommand(string s)
        {
            string cmd = s.Substring(0, 4);
            int cycle, x;
            if (cmd == "noop")
            {
                cycle = 1;
                x = 0;
            }
            else
            {
                cycle = 2;
                x = int.Parse(s.Substring(5));
            }

            totalCycles += cycle;

            if (totalCycles >= cycleStages[currentStage])
            {
                stageTotal.Add(cycleStages[currentStage] * regX);
                regX += x;
                currentStage++;
            }
            else { regX += x; }
        }

        #endregion Day10_Part1

        #region Day10_Part2
        char[][] screen = new char[6][];
        int row = 0;
        int col = 0;

        internal async Task<string> Part2()
        {
            for (int i = 0; i < screen.Length; i++)
            {
                screen[i] = new char[40];
            }

            List<string> data = await Utils.ReadFile("day10_1.txt");
            foreach (string s in data)
            {
                UpdateScreen(s);
            }

            for (int i = 0; i < screen.Length; i++)
            {
                Debug.WriteLine(new string(screen[i]));
            }

            return "no returned value";
        }

        private void UpdateScreen(string s)
        {
            string cmd = s.Substring(0, 4);
            int cycle, x;
            if (cmd == "noop")
            {
                cycle = 1;
                x = 0;
            }
            else
            {
                cycle = 2;
                x = int.Parse(s.Substring(5));
            }

            for (int i=0; i< cycle; i++)
            {
                screen[row][col] = (regX - 1 <= col && col <= regX + 1) ? '#' : '.';
                col++;
                if (col == 40)
                {
                    row++;
                    col = 0;
                }
            }

            regX += x;
        }
        #endregion Day10_Part2
    }
}
