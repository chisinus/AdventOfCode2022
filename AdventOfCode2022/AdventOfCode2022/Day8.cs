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
    class Tree
    {
        public char Height { get; set; }
        public bool? UpVisible { get; set; }
        public bool? LeftVisible { get; set; }
        public bool? DownVisible { get; set; }
        public bool? RightVisible { get; set; }
    }

    internal class Day8
    {
        #region process_1
        internal async Task<string> Process_1()
        {
            List<string> data = await Utils.ReadFile("day8_1.txt");

            List<Tree[]> trees = new List<Tree[]>();
            foreach (string s in data)
            {
                Tree[] row = new Tree[s.Length];
                for (int i=0; i<s.Length; i++)
                {
                    row[i] = new Tree()
                    {
                        Height = s[i],
                        UpVisible = null,
                        LeftVisible = null,
                        DownVisible = null,
                        RightVisible = null
                    };
                }

                trees.Add(row);
            }

            int total = 0;
            for (int row = 0; row<trees.Count; row++)
            {
                for (int col =0; col < trees[row].Length; col++)
                {
                    trees[row][col].UpVisible = GetUpTallest(trees, row, col) < trees[row][col].Height;
                    trees[row][col].LeftVisible = GetLeftTallest(trees, row, col) < trees[row][col].Height;
                    trees[row][col].DownVisible = GetDownTallest(trees, row, col) < trees[row][col].Height;
                    trees[row][col].RightVisible = GetRightTallest(trees, row, col) < trees[row][col].Height;

                    if (trees[row][col].UpVisible == true || trees[row][col].LeftVisible == true || trees[row][col].DownVisible == true || trees[row][col].RightVisible == true)
                        total++;
                }
            }

            return total.ToString();
        }

        private int GetUpTallest(List<Tree[]> trees, int row, int col)
        {
            if (row == 0) return 0;

            if (trees[row - 1][col].UpVisible == null)
            {
                trees[row - 1][col].UpVisible = GetUpTallest(trees, row - 1, col) < trees[row - 1][col].Height;
            }

            if (trees[row - 1][col].UpVisible == true) return trees[row - 1][col].Height;

            return GetUpTallest(trees, row - 1, col);
        }

        private int GetLeftTallest(List<Tree[]> trees, int row, int col)
        {
            if (col == 0) return 0;

            if (trees[row][col - 1].LeftVisible == null)
            {
                trees[row][col - 1].LeftVisible = GetLeftTallest(trees, row, col - 1) < trees[row][col - 1].Height;
            }

            if (trees[row][col - 1].LeftVisible == true) return trees[row][col - 1].Height;

            return GetLeftTallest(trees, row, col - 1);
        }

        private int GetDownTallest(List<Tree[]> trees, int row, int col)
        {
            if (row == trees.Count - 1) return 0;

            if (trees[row + 1][col].DownVisible == null)
            {
                trees[row + 1][col].DownVisible = GetDownTallest(trees, row + 1, col) < trees[row + 1][col].Height;
            }

            if (trees[row + 1][col].DownVisible == true) return trees[row + 1][col].Height;

            return GetDownTallest(trees, row + 1, col);
        }

        private int GetRightTallest(List<Tree[]> trees, int row, int col)
        {
            if (col == trees[0].Length - 1) return 0;

            if (trees[row][col + 1].RightVisible == null)
            {
                trees[row][col + 1].RightVisible = GetRightTallest(trees, row, col + 1) < trees[row][col + 1].Height;
            }

            if (trees[row][col + 1].RightVisible == true) return trees[row][col + 1].Height;

            return GetRightTallest(trees, row, col + 1);
        }
        #endregion process_1

        #region process_2
        internal async Task<string> Process_2()
        {
            List<string> data = await Utils.ReadFile("day8_1.txt");

            List<Tree[]> trees = new List<Tree[]>();
            foreach (string s in data)
            {
                Tree[] row = new Tree[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    row[i] = new Tree()
                    {
                        Height = s[i]
                    };
                }

                trees.Add(row);
            }

            int max = 0;
            for (int row = 1; row < trees.Count; row++)
            {
                for (int col = 1; col < trees[row].Length; col++)
                {
                    int up = GetUpDistance(trees, row, col);
                    int left = GetLeftDistance(trees, row, col);
                    int down = GetDownDistance(trees, row, col);
                    int right = GetRightDistance(trees, row, col);
                    int total = up * left * down * right;
                    
                    if (max < total) max = total;
                }
            }

            return max.ToString();
        }

        private int GetUpDistance(List<Tree[]> trees, int row, int col)
        {
            if (row == 0) return 0;

            int count = 0;
            for (int i=row-1; i>=0; i--)
            {
                count++;

                if (trees[i][col].Height >= trees[row][col].Height) break;
            }

            return count;
        }

        private int GetLeftDistance(List<Tree[]> trees, int row, int col)
        {
            if (col == 0) return 0;

            int count = 0;
            for (int i = col - 1; i >= 0; i--)
            {
                count++;

                if (trees[row][i].Height >= trees[row][col].Height) break;
            }

            return count;
        }

        private int GetDownDistance(List<Tree[]> trees, int row, int col)
        {
            if (row == trees.Count - 1) return 0;

            int count = 0;
            for (int i = row + 1; i < trees.Count; i++)
            {
                count++;

                if (trees[i][col].Height >= trees[row][col].Height) break;
            }

            return count;
        }

        private int GetRightDistance(List<Tree[]> trees, int row, int col)
        {
            if (col == trees[0].Length - 1) return 0;

            int count = 0;
            for (int i = col + 1; i < trees[0].Length; i++)
            {
                count++;

                if (trees[row][i].Height >= trees[row][col].Height) break;
            }

            return count;
        }
        #endregion process_2

    }
}
