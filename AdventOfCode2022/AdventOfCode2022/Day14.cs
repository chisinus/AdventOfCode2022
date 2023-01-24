namespace AdventOfCode2022
{
    internal class Day14
    {
        enum TileStatuses
        {
            Empty,
            Sand,
            Rock
        }

        class Tile
        {
            public Point Location { get; set; }
            public TileStatuses TileStatus { get; set; }
        }

        Tile[][] tiles = new Tile[200][];
        Point originPoouringLocation = new Point() { X = 500, Y = 0 };

        int maxRow = 0;

        internal async Task<string> Part1()
        {
            List<string> data = await Utils.ReadFile("day14_1.txt");

            for (int i = 0; i < tiles.Length; i++)
            {
                Tile[] row = new Tile[600];
                tiles[i] = row;
                for (int j = 0; j < row.Length; j++)
                {
                    row[j] = new Tile()
                    {
                        Location = new Point(i, j),
                        TileStatus = TileStatuses.Empty
                    };
                }
            }

            foreach (string s in data)
            {
                ProcessLine(s);
            }

            int result = 0;
            while (true)
            {
                Point p = Pour(originPoouringLocation);
                if (p.X == 0 && p.Y == 0) break;

                result++;
            }

            return result.ToString();
        }

        internal async Task<string> Part2()
        {
            List<string> data = await Utils.ReadFile("day14_1.txt");

            for (int i = 0; i < tiles.Length; i++)
            {
                Tile[] row = new Tile[1000];
                tiles[i] = row;
                for (int j = 0; j < row.Length; j++)
                {
                    row[j] = new Tile()
                    {
                        Location = new Point(j, i),
                        TileStatus = TileStatuses.Empty
                    };
                }
            }

            foreach (string s in data)
            {
                ProcessLine(s);
            }

            // floor
            for (int i = 0; i < tiles[maxRow + 2].Length; i++)
            {
                tiles[maxRow + 2][i].TileStatus = TileStatuses.Rock;
            }
            maxRow += 2;

            int result = 0;
            while (true)
            {
                Point p = Pour(originPoouringLocation);
                result++;

                if (p.X == 500 && p.Y == 0) break;
            }

            //for (int i = 0; i < 15; i++)      // for testing data
            //{
            //    for (int j = 490; j < 530; j++)     // for testing data
            //    {
            //        string sign = ".";
            //        if (tiles[i][j].TileStatus == TileStatuses.Rock)
            //            sign = "#";
            //        else if (tiles[i][j].TileStatus == TileStatuses.Sand)
            //            sign = "O";

            //        Debug.Write(sign);
            //    }
            //    Debug.WriteLine("");
            //}

            return result.ToString();
        }

        private void ProcessLine(string s)
        {
            string[] locations = s.Split("->");

            int i = 0;
            while (i < locations.Length - 1)
            {
                int col1 = int.Parse(locations[i].Substring(0, locations[i].IndexOf(",")));
                int row1 = int.Parse(locations[i].Substring(locations[i].IndexOf(",") + 1));
                int col2 = int.Parse(locations[i + 1].Substring(0, locations[i + 1].IndexOf(",")));
                int row2 = int.Parse(locations[i + 1].Substring(locations[i + 1].IndexOf(",") + 1));

                Draw(row1, col1, row2, col2);

                i++;
            }
        }

        private void Draw(int row1, int col1, int row2, int col2)
        {
            if (row1 == row2)   // horizontal
            {
                int colStart = col1 < col2 ? col1 : col2;
                int colEnd = col1 < col2 ? col2 : col1;

                for (int col = colStart; col <= colEnd; col++)
                {
                    tiles[row1][col].TileStatus = TileStatuses.Rock;
                }
            }
            else
            {
                int rowStart = row1 < row2 ? row1 : row2;
                int rowEnd = row1 < row2 ? row2 : row1;

                if (rowEnd > maxRow) maxRow = rowEnd;

                for (int row = rowStart; row <= rowEnd; row++)
                {
                    tiles[row][col1].TileStatus = TileStatuses.Rock;
                }
            }
        }

        private Point Pour(Point location)
        {
            if (location.Y >= maxRow) return new Point(0, 0);   // for part 1

            if (tiles[location.Y + 1][location.X].TileStatus == TileStatuses.Empty)
            {
                return Pour(new Point(location.X, location.Y + 1));
            }
            else if ((tiles[location.Y + 1][location.X].TileStatus == TileStatuses.Rock) ||
                (tiles[location.Y + 1][location.X].TileStatus == TileStatuses.Sand))
            {
                if (tiles[location.Y + 1][location.X - 1].TileStatus == TileStatuses.Empty)
                    return Pour(new Point(location.X - 1, location.Y + 1));
                else if (tiles[location.Y + 1][location.X + 1].TileStatus == TileStatuses.Empty)
                    return Pour(new Point(location.X + 1, location.Y + 1));
                else
                    tiles[location.Y][location.X].TileStatus = TileStatuses.Sand;
            }

            return tiles[location.Y][location.X].Location;
        }
    }
}

