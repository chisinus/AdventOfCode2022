namespace AdventOfCode2022
{
    class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Front { get; set; }
        public bool Back { get; set; }
    }

    class Day18
    {
        List<Cube> cubes = new List<Cube>();
        Cube[,,] cubeArray;
        int maxX = 0;
        int maxY = 0;
        int maxZ = 0;

        #region Part1
        internal async Task<string> Process(bool includeAirCube)
        {
            List<string> data = await Utils.ReadFile("day18_1.txt");

            foreach (string line in data)
            {
                ProcessData(line);
            }

            cubeArray = new Cube[maxX + 1, maxY + 1, maxZ + 1];

            foreach (Cube cube in cubes)
            {
                cubeArray[cube.X, cube.Y, cube.Z] = cube;
            }

            foreach (Cube cube in cubes)
            {
                cube.Up = cube.Y != 0 && cubeArray[cube.X, cube.Y - 1, cube.Z] != null;
                cube.Down = cube.Y != maxY && cubeArray[cube.X, cube.Y + 1, cube.Z] != null;
                cube.Left = cube.X != 0 && cubeArray[cube.X - 1, cube.Y, cube.Z] != null;
                cube.Right = cube.X != maxY && cubeArray[cube.X + 1, cube.Y, cube.Z] != null;
                cube.Front = cube.Z != 0 && cubeArray[cube.X, cube.Y, cube.Z - 1] != null;
                cube.Back = cube.Z != maxZ && cubeArray[cube.X, cube.Y, cube.Z + 1] != null;
            }


            int result = 0;
            // Party 1
            foreach (Cube cube in cubes)
            {
                result = result +
                         (cube.Up ? 0 : 1) +
                         (cube.Down ? 0 : 1) +
                         (cube.Left ? 0 : 1) +
                         (cube.Right ? 0 : 1) +
                         (cube.Front ? 0 : 1) +
                         (cube.Back ? 0 : 1);
            }


            // Part 1 & Part 2
            // check mark if a cube side can be "visible" from outside, mark a cube "visible" while checking
            //int airCubes = 0;
            //for (int x = 0; x < cubeArray.GetLength(0); x++)
            //{
            //    for (int y = 0; y < cubeArray.GetLength(1); y++)
            //    {
            //        for (int z = 0; z < cubeArray.GetLength(2); z++)
            //        {
            //            if (cubeArray[x, y, z] == null)
            //                airCubes += IsAireCube(x, y, z) ? 1 : 0;
            //            else
            //            {
            //                result = result +
            //                         (cubeArray[x, y, z].Up ? 0 : 1) +
            //                         (cubeArray[x, y, z].Down ? 0 : 1) +
            //                         (cubeArray[x, y, z].Left ? 0 : 1) +
            //                         (cubeArray[x, y, z].Right ? 0 : 1) +
            //                         (cubeArray[x, y, z].Front ? 0 : 1) +
            //                         (cubeArray[x, y, z].Back ? 0 : 1);
            //            }
            //        }
            //    }
            //}

            //if (!includeAirCube) result -= airCubes * 6;

            return result.ToString();
        }

        private bool IsAireCube(int x, int y, int z)
        {
            //return (y != 0 && cubeArray[x, y - 1, z] != null) &&
            //        (y != maxY && cubeArray[x, y + 1, z] != null) &&
            //        (x != 0 && cubeArray[x - 1, y, z] != null) &&
            //        (x != maxX && cubeArray[x + 1, y, z] != null) &&
            //        (z != 0 && cubeArray[x, y, z - 1] != null) &&
            //        (z != maxZ && cubeArray[x, y, z + 1] != null);


            if ((y == 0) || (y == maxY) || (x == 0) || (x == maxY) || (z == 0) || (z == maxZ)) return false;

            if (x == 2 && y == 9 && z == 9)
            {
                int kk = 0;
            }

            int i;
            for (i = y - 1; i >= 0; i--)
            {
                if (cubeArray[x, i, z] != null) break;
            }
            if (i < 0) return false;

            for (i = y + 1; i <= maxY; i++)
            {
                if (cubeArray[x, i, z] != null) break;
            }
            if (i > maxY) return false;

            for (i = x - 1; i >= 0; i--)
            {
                if (cubeArray[i, y, z] != null) break;
            }
            if (i < 0) return false;

            for (i = x + 1; i <= maxX; i++)
            {
                if (cubeArray[i, y, z] != null) break;
            }
            if (i > maxY) return false;

            for (i = z - 1; i >= 0; i--)
            {
                if (cubeArray[x, y, i] != null) break;
            }
            if (i < 0) return false;

            for (i = z + 1; i <= maxZ; i++)
            {
                if (cubeArray[x, y, i] != null) break;
            }
            if (i > maxZ) return false;

            return true;
        }

        private void ProcessData(string line)
        {
            int first = line.IndexOf(',');
            int second = line.IndexOf(',', first + 1);

            int x = int.Parse(line.Substring(0, first));
            int y = int.Parse(line.Substring(first + 1, second - first - 1));
            int z = int.Parse(line.Substring(second + 1));

            if (x > maxX) maxX = x;
            if (y > maxY) maxY = y;
            if (z > maxZ) maxZ = z;

            cubes.Add(new Cube
            {
                X = x,
                Y = y,
                Z = z
            });
        }
        #endregion Part1

    }
}