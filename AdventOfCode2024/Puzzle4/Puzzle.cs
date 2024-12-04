using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle4
{
    internal class Puzzle(string inputName)
    {
        private string[] Rows { get; set; } = File.ReadAllLines($"{nameof(Puzzle4)}/{inputName}");

        public long Solve()
        {
            var total = 0L;
            var input = Rows.Select(x => x.ToCharArray()).ToArray();

            List<Direction> directions =
            [
                new Direction(-1, -1),
                new Direction(-1, 1),
                new Direction(1, 1),
                new Direction(1, -1),
                new Direction(0, -1),
                new Direction(0, 1),
                new Direction(1, 0),
                new Direction(-1, 0)
            ];

            var starts = new Stack<NextChar>();
            for (var i = 0; i < input.Length; i++)
            {
                var row = input[i];
                for (int j = 0; j < row.Length; j++)
                {
                    if (row[j] != 'X') continue;
                    foreach (var direction in directions)
                    {
                        starts.Push(new NextChar('M', i, j, direction));
                    }

                }
            }

            while (starts.Count != 0)
            {
                var current = starts.Pop();
                if (!IsSafeMatch(input, current)) continue;
                if (current.Current == 'S')
                {
                    total++;
                }
                else
                {
                    starts.Push(new NextChar(current.GetNext(), current.I, current.J, current.Direction));
                }
            }


            return total;
        }

        private static bool IsSafeMatch(char[][] input, NextChar current)
        {
            if (current.I < 0 || current.I>= input.Length || current.J < 0 || current.J >= input[current.I].Length)
                return false;
            return input[current.I][current.J] == current.Current;
        }
        



        public long SolveB()
        {
            var input = Rows.Select(x => x.ToCharArray()).ToArray();
      

            var starts = new List<Cross>();
            for (var i = 0; i < input.Length; i++)
            {
                var row = input[i];
                for (int j = 0; j < row.Length; j++)
                {
                    if (row[j] == 'A')
                    {
                        starts.Add(new Cross(i, j));
                    }
                    
                }
            }

            return starts.Count(x => x.IsValid(input));
        }
    }


    public record Direction(int vertical, int horizontal);
}