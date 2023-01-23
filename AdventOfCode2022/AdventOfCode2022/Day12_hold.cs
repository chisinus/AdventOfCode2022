namespace AdventOfCode2022
{
    class Mark
    {
        public Point MarkLocation { get; set; }
        public bool Visited { get; set; }
        public int TentativeDistance { get; set; }
        //public Mark Up { get; set; } 
        //public Mark Right { get; set; }
        //public Mark Down { get; set; }
        //public Mark Left { get; set; }  
    }

    // Will come back to this later.
    // https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
    // https://www.dotnetoffice.com/2022/10/dijkstra-algorithm-for-determining.html
    // https://www.dotnetlovers.com/article/234/dijkstras-shortest-path-algorithm
    internal class Day12
    {
        int sX = -1, sY, eX = -1, eY;
        char[][] marks;
        const int InfinityValue = 10000000;

        internal async Task<string> Process(int round, bool useFixedRelieveLevel)
        {
            List<string> data = await Utils.ReadFile("day12_1.txt");

            marks = new char[data.Count][];
            ProcessData(data);

            FindPath(new Point(sX, sY));

            return 0.ToString();
        }

        private void ProcessData(List<string> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    Mark mark = new Mark()
                    {
                        MarkLocation = new Point(i, j),
                        Visited = false,
                        TentativeDistance = data[i][j] == 'S' ? 0 : InfinityValue,
                    };

                    if (data[i][j] == 'S')
                    {
                        sX = data[i].IndexOf('S');
                        sY = i;
                    }

                    if (data[i][j] == 'E')
                    {
                        eX = data[i].IndexOf('E');
                        eY = i;
                    }
                }
            }

        }
        private void FindPath(Point mark)
        {
            //MoveUp(mark);
            //MoveRight(mark);
            //MoveDown(mark);
            //MoveLeft(mark);


        }
    }
}
