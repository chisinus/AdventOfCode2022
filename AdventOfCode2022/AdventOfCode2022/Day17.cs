using System.Diagnostics;

namespace AdventOfCode2022
{
    abstract class Rock
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public int[][] Pattern { get; set; } = new int[0][];

        public abstract void BuildRock();

        public int Height { get { return Pattern.Length; } }
        public int Width { get { return Pattern[0].Length; } }

        public int RightCol
        {
            get
            {
                return Col + Pattern[0].Length - 1;
            }
        }
        public int BottomRow
        {
            get
            {
                return Row + Pattern.Length - 1;
            }
        }

        public Rock()
        {
            BuildRock();
        }
    }

    class HRock : Rock
    {
        public override void BuildRock()
        {
            Pattern = new int[1][];
            Pattern[0] = new int[4] { 1, 1, 1, 1 };
        }
    }

    class PlusRock : Rock
    {
        public override void BuildRock()
        {
            Pattern = new int[3][];
            Pattern[0] = new int[3] { 0, 1, 0 };
            Pattern[1] = new int[3] { 1, 1, 1 };
            Pattern[2] = new int[3] { 0, 1, 0 };
        }
    }

    class LRock : Rock
    {
        public override void BuildRock()
        {
            Pattern = new int[3][];
            Pattern[0] = new int[3] { 0, 0, 1 };
            Pattern[1] = new int[3] { 0, 0, 1 };
            Pattern[2] = new int[3] { 1, 1, 1 };
        }
    }

    class VRock : Rock
    {
        public override void BuildRock()
        {
            Pattern = new int[4][];
            Pattern[0] = new int[1] { 1 };
            Pattern[1] = new int[1] { 1 };
            Pattern[2] = new int[1] { 1 };
            Pattern[3] = new int[1] { 1 };
        }
    }

    class SquareRock : Rock
    {
        public override void BuildRock()
        {
            Pattern = new int[2][];
            Pattern[0] = new int[2] { 1, 1 };
            Pattern[1] = new int[2] { 1, 1 };
        }
    }

    class Day17
    {
        string moves = "";
        int nextRockType = 1;
        int currentMove = 0;
        List<int[]> chamber = new List<int[]>();
        Rock currentRock;
        int aaa;

        #region Part1
        internal async Task<string> Part1()
        {
            List<string> data = await Utils.ReadFile("day17_1.txt");
            moves = data[0];

            for (int i = 0; i < 2022; i++)
            //for (int i = 0; i < 10; i++)
            {
                currentRock = GenerateCurrentRock();

                AdjustChamberAndRock();

                DropRock();

                int kk = 0;
                // Mark chamber
                for (int m = 0; m < currentRock.Height; m++)
                {
                    for (int n = 0; n < currentRock.Width; n++)
                    {
                        if (currentRock.Pattern[m][n] == 1)
                        {
                            if (chamber[currentRock.Row + m][currentRock.Col + n] == 1)
                            {
                                kk++;
                            }
                            chamber[currentRock.Row + m][currentRock.Col + n] = 1;
                        }
                    }
                }
            }

            PrintChamber(chamber.Count);
            int emptyRow = 0;
            for (emptyRow = 0; emptyRow < chamber.Count; emptyRow++)
            {
                if (chamber[emptyRow].Any(c => c == 1)) break;
            }

            int result = chamber.Count - emptyRow;


            return result.ToString();
        }

        private void PrintChamber(int row)
        {
            if (row > 100) row = 100;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (chamber[i][j] == 2)
                        Debug.Write('O');
                    else
                        Debug.Write(chamber[i][j] == 0 ? '.' : '#');
                }

                Debug.WriteLine("");
            }
        }

        private Rock GenerateCurrentRock()
        {
            Rock rock;
            switch (nextRockType)
            {
                case 1:
                    rock = new HRock();
                    break;
                case 2:
                    rock = new PlusRock();
                    break;
                case 3:
                    rock = new LRock();
                    break;
                case 4:
                    rock = new VRock();
                    break;
                default: // 5
                    rock = new SquareRock();
                    break;
            }

            nextRockType++;
            if (nextRockType == 6) nextRockType = 1;

            return rock;
        }

        private void AdjustChamberAndRock()
        {
            int topRow = 0;
            for (topRow = 0; topRow < chamber.Count; topRow++)
            {
                if (chamber[topRow].Any(c => c == 1)) break;
            }

            int newRows = currentRock.Height + 3 - topRow;
            for (int i = 0; i < newRows; i++)
            {
                chamber.Insert(0, new int[7]);
            }

            currentRock.Row = (topRow > currentRock.Height + 3) ? topRow - currentRock.Height - 3 : 0;
            currentRock.Col = 2;
        }

        private void DropRock()
        {
            //if (aaa == 21)
            //{
            //    for (int m = 0; m < currentRock.Height; m++)
            //    {
            //        for (int n = 0; n < currentRock.Width; n++)
            //        {
            //            if (currentRock.Pattern[m][n] == 1)
            //            {
            //                chamber[currentRock.Row + m][currentRock.Col + n] = 2;
            //            }
            //        }
            //    }

            //    PrintChamber();
            //}

            MoveRockRightLeft();

            if (MoveRockDown()) DropRock();
        }

        private void MoveRockRightLeft()
        {
            if (moves[currentMove] == '>') MoveRight();
            else MoveLeft();

            currentMove++;
            if (currentMove == moves.Length) currentMove = 0;
        }

        private void MoveRight()
        {
            // hit the wall
            if (currentRock.RightCol == 6) return;

            // hit an existing rock
            if (nextRockType == 3)  // current: plus rock
            {
                if ((chamber[currentRock.Row][currentRock.RightCol] == 1) ||
                    (chamber[currentRock.Row + 1][currentRock.RightCol + 1] == 1) ||
                    (chamber[currentRock.Row + 2][currentRock.RightCol] == 1))
                    return;
            }
            else
            {
                for (int i = 0; i < currentRock.Pattern.Length; i++)
                {
                    if (chamber[currentRock.Row + i][currentRock.RightCol + 1] == 1) return;
                }
            }

            currentRock.Col++;
        }

        private void MoveLeft()
        {
            // hit the wall
            if (currentRock.Col == 0) return;

            // hit an existing rock
            if (nextRockType == 3)  // current: plus rock
            {
                if ((chamber[currentRock.Row][currentRock.Col] == 1) ||
                    (chamber[currentRock.Row + 1][currentRock.Col - 1] == 1) ||
                    (chamber[currentRock.Row + 2][currentRock.Col] == 1))
                    return;
            }
            else if (nextRockType == 4) // current: L shape
            {
                if ((chamber[currentRock.Row][currentRock.Col + 1] == 1) ||
                    (chamber[currentRock.Row + 1][currentRock.Col + 1] == 1) ||
                    (chamber[currentRock.Row + 2][currentRock.Col - 1] == 1))
                    return;
            }
            else
            {
                for (int i = 0; i < currentRock.Pattern.Length; i++)
                {
                    if (chamber[currentRock.Row + i][currentRock.Col - 1] == 1) return;

                    // This can replace if/else, slower
                    //int mostLeft = Array.FindIndex(currentRock.Pattern[i], e => e == 1);
                    //if (chamber[currentRock.Row + i][currentRock.Col + mostLeft - 1] == 1) return;
                }
            }

            currentRock.Col--;
        }

        private bool MoveRockDown()
        {
            if (currentRock.BottomRow == chamber.Count - 1) return false;

            if (nextRockType == 3)  // current: plus rock
            {
                if ((chamber[currentRock.BottomRow][currentRock.Col] == 1) ||
                    (chamber[currentRock.BottomRow + 1][currentRock.Col + 1] == 1) ||
                    (chamber[currentRock.BottomRow][currentRock.Col + 2] == 1))
                    return false;
            }
            else
            {
                for (int i = 0; i < currentRock.Width; i++)
                {
                    if (chamber[currentRock.BottomRow + 1][currentRock.Col + i] == 1)
                    {
                        return false;
                    }
                }
            }

            currentRock.Row++;

            return true;
        }
        #endregion Part1

    }
}