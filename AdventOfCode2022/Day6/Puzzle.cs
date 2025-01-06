using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day6
{
    internal class Puzzle
    {
        public Puzzle(string[] rows)
        {
            Rows = rows;
        }

        private string[] Rows { get; set; }

        public int Solve()
        {
            return FindUniqueRange(4);
        }

        private int FindUniqueRange(int rangeLength)
        {
            var input = Rows[0];
            var currSet = new HashSet<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (!currSet.Add(input[i]))
                {
                    i -= (currSet.Count);
                    currSet.Clear();

                }

                if (currSet.Count == rangeLength) return i + 1;
            }

            return 0;
        }

        public int SolveB()
        {
            return FindUniqueRange(14);
        }
    }
}