using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day5
{
    internal class Puzzle
    {
        private readonly Stack<char>[] _stacks;
        private readonly List<Move> _moves;

        public Puzzle(string[] rows)
        {
            Rows = rows;
            var stackCount = (int) Math.Ceiling((decimal) rows.Length / 4);
            _stacks = new Stack<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                _stacks[i] = new Stack<char>();
            }

            var rowIndex = 0;
            var row = Rows[rowIndex];
            while (!string.IsNullOrEmpty(row))
            {
                var i = 0;
                while (row.Length > 0)
                {
                    var length = Math.Min(row.Length, 4);
                    var stack = row[..(length)];
                    var match = StackRegex.Match(stack);
                    if (match.Success)
                    {
                        _stacks[i].Push(match.Value[0]);
                    }

                    row = row[length..];
                    i++;
                }

                row = Rows[++rowIndex];
            }

            for (int i = 0; i < stackCount; i++)
            {
                _stacks[i] = new Stack<char>(_stacks[i]);
            }

            _moves = new List<Move>();

            rowIndex++;
            while (rowIndex < Rows.Length)
            {
                row = Rows[rowIndex];

                var match = MoveRegex.Match(row);
                _moves.Add(new Move(int.Parse(match.Groups["count"].Value), int.Parse(match.Groups["start"].Value),
                    int.Parse(match.Groups["finish"].Value)));

                rowIndex++;
            }
        }

        private string[] Rows { get; set; }

        private static readonly Regex StackRegex = new(@"(?<=\[)\w(?=\])");
        private static readonly Regex MoveRegex = new(@"move\s(?'count'\d*)\sfrom\s(?'start'\d)\sto\s(?'finish'\d)");

        public string Solve()
        {
            foreach (var move in _moves)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    _stacks[move.Finish - 1].Push(_stacks[move.Start - 1].Pop());
                }
            }


            string result = "";
            foreach (var stack in _stacks)
            {
                if (stack.TryPop(out var c))
                {
                    result += c;
                }
            }

            return result;
        }

        public record Move(int Count, int Start, int Finish);


        public string SolveB()
        {
            foreach (var move in _moves)
            {
                var tempStack = new Stack<char>();
                for (int i = 0; i < move.Count; i++)
                {
                    tempStack.Push(_stacks[move.Start - 1].Pop());
                }

                for (int i = 0; i < move.Count; i++)
                {
                    _stacks[move.Finish-1].Push(tempStack.Pop());
                }
            }


            string result = "";
            foreach (var stack in _stacks)
            {
                if (stack.TryPop(out var c))
                {
                    result += c;
                }
            }

            return result;
        }
    }
}