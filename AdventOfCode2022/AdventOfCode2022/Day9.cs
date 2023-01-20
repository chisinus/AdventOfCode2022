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
    internal class Day9
    {
        //Point orgin = new Point() { X = 100, Y = 100 };
        Point [] knots = new Point[0];

        List<Point> visited = new List<Point>();

        internal async Task<string> Process(int noOfKnots)
        {
            knots = new Point[noOfKnots];
            for (int i=0; i<noOfKnots; i++)
            {
                knots[i] = new Point() { X = 100, Y = 100 };
            }

            List<string> data = await Utils.ReadFile("day9_1.txt");
            foreach (string s in data)
            {
                char direction = s[0];
                int moves = int.Parse(s.Substring(1));
                for (int i=0; i<moves; i++)
                {
                    switch (direction)
                    {
                        case 'R':
                            knots[0].X++;
                            break;
                        case 'L':
                            knots[0].X--;
                            break;
                        case 'U':
                            knots[0].Y--;
                            break;
                        case 'D':
                            knots[0].Y++;
                            break;
                    }

                    MoveRestKnots();
                }
            }

            return visited.Count.ToString();
        }

        private void MoveRestKnots()
        {
            for (int j = 1; j < knots.Length; j++)
            {
                MoveKnot(j);
            }
            SaveVisitedPoint();
        }

        private void SaveVisitedPoint()
        {
            if (visited.Where(p => p.X == knots[knots.Length-1].X && p.Y == knots[knots.Length - 1].Y).Any()) return;
                
            visited.Add(knots[knots.Length - 1]);
        }

        private void MoveKnot(int pos)
        {
            if (knots[pos-1].X== knots[pos].X)  // same column
            {
                if (knots[pos - 1].Y - knots[pos].Y == 2)
                {
                    knots[pos].Y++;
                }
                else if (knots[pos - 1].Y - knots[pos].Y == -2)
                {
                    knots[pos].Y--;
                }
            }
            else if (knots[pos - 1].Y == knots[pos].Y)  // same row
            {
                if (knots[pos - 1].X- knots[pos].X == 2)
                {
                    knots[pos].X++;
                }
                else if (knots[pos - 1].X- knots[pos].X == -2)
                {
                    knots[pos].X--;
                }
            }
            else  // diagally
            {
                if (knots[pos - 1].X- knots[pos].X == 2)
                {
                    if (knots[pos - 1].Y > knots[pos].Y)  // right-down
                    {
                        knots[pos].X++;
                        knots[pos].Y++;
                    }
                    else            // right-up
                    {
                        knots[pos].X++;
                        knots[pos].Y--;
                    }
                }
                else if (knots[pos - 1].X- knots[pos].X == -2)
                {
                    if (knots[pos - 1].Y > knots[pos].Y)  // left-down
                    {
                        knots[pos].X--;
                        knots[pos].Y++;
                    }
                    else            // left-up
                    {
                        knots[pos].X--;
                        knots[pos].Y--;
                    }
                }
                if (knots[pos - 1].Y - knots[pos].Y == 2)
                {
                    if (knots[pos - 1].X> knots[pos].X)  // right-down
                    {
                        knots[pos].X++;
                        knots[pos].Y++;
                    }
                    else            // left-down
                    {
                        knots[pos].X--;
                        knots[pos].Y++;
                    }
                }
                else if (knots[pos - 1].Y - knots[pos].Y == -2)
                {
                    if (knots[pos - 1].X> knots[pos].X)  // right-up
                    {
                        knots[pos].X++;
                        knots[pos].Y--;
                    }
                    else            // left-up
                    {
                        knots[pos].X--;
                        knots[pos].Y--;
                    }
                }
            }
        }
    }
}
