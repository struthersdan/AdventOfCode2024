using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle21
{
    internal class Puzzle
    {
        private string[] Rows { get; set; }
        private readonly Dictionary<int, string> _buttonGroups;

        private static readonly char[][] NumericButtons = new char[][]
        {
            ['7', '8', '9'],
            ['4', '5', '6'],
            ['1', '2', '3'],
            [' ', '0', 'A']
        };

        private static readonly char[][] ArrowButtons = new char[][]
        {
            [' ', '^', 'A'],
            ['<', 'v', '>']
        };

        private readonly Dictionary<char, (int i, int j)> _numericIndices;
        private readonly Dictionary<char, (int i, int j)> _arrowIndices;
        private readonly Dictionary<RecursiveState, long> _solvedLengths = new();
        private readonly Dictionary<(char c, char n), string> _calculatedMoves = new();
        private readonly Regex r = new Regex(@"\d*");

        public Puzzle(string inputName)
        {
            Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

            _buttonGroups = Rows.ToDictionary(x => int.Parse(r.Match(x).Value), x => x);
            _numericIndices = BuildNumericIndices();
            _arrowIndices = BuildArrowIndices();
        }

        private static Dictionary<char, (int i, int j)> BuildArrowIndices()
        {
            var arrowIndices = new Dictionary<char, (int i, int j)>();
            for (int i = 0; i < ArrowButtons.Length; i++)
            {
                for (int j = 0; j < ArrowButtons[i].Length; j++)
                {
                    arrowIndices.Add(ArrowButtons[i][j], (i, j));
                }
            }

            return arrowIndices;
        }

        private static Dictionary<char, (int i, int j)> BuildNumericIndices()
        {
            var numericIndices = new Dictionary<char, (int i, int j)>();
            for (int i = 0; i < NumericButtons.Length; i++)
            {
                for (int j = 0; j < NumericButtons[i].Length; j++)
                {
                    numericIndices.Add(NumericButtons[i][j], (i, j));
                }
            }

            return numericIndices;
        }


        private static string GetInputNameInFolder(string inputName)
        {
            return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
        }


        public long Solve()
        {
            var total = 0L;

            foreach (var buttonGroup in _buttonGroups)
            {
                var groupLength = 0L;

                var curr = 'A';
                foreach (var c in buttonGroup.Value)
                {
                    groupLength += CalculatePathRecursive(new RecursiveState(curr, c, 3), NumericButtons, _numericIndices);
                    curr = c;
                }


                total += buttonGroup.Key * groupLength;

                Console.WriteLine();
                Console.WriteLine();
            }

            foreach (var calculatedMove in _calculatedMoves)
            {
                Console.WriteLine($"{calculatedMove.Key.c} -> {calculatedMove.Key.n}: {calculatedMove.Value}");
            }

            return total;
        }


        public long SolveB()
        {
            var total = 0L;

            foreach (var buttonGroup in _buttonGroups)
            {
                var groupLength = 0L;

                var curr = 'A';
                foreach (var c in buttonGroup.Value)
                {
                    groupLength += CalculatePathRecursive(new RecursiveState(curr, c, 26), NumericButtons, _numericIndices);
                    curr = c;
                }


                total += buttonGroup.Key * groupLength;

                Console.WriteLine();
                Console.WriteLine();
            }

            foreach (var calculatedMove in _calculatedMoves)
            {
                Console.WriteLine($"{calculatedMove.Key.c} -> {calculatedMove.Key.n}: {calculatedMove.Value}");
            }

            return total;
        }


        private long CalculatePathRecursive(RecursiveState state, char[][] buttons, Dictionary<char, (int, int)> indices)
        {

            if (_solvedLengths.TryGetValue(state, out var length)) return length;
            var (start, next, level) = state;

            var path = WriteShortestPath(start, next, indices, buttons);


            if (level == 1)
            {
                length = path.Length;
            }
            else
            {
                var curr = 'A';
                var nextLevel = level - 1;
                foreach (var c in path)
                {
                    length += CalculatePathRecursive(new RecursiveState(curr, c, nextLevel), ArrowButtons, _arrowIndices);
                    curr = c;
                }
            }


            _solvedLengths.Add(state, length);

            return length;
        }


        private string WriteShortestPath(char curr, char next, Dictionary<char, (int i, int j)> indices,
            char[][] buttons)
        {
            if (_calculatedMoves.TryGetValue((curr, next), out var moves)) return moves;

            var currLocation = indices[curr];
            var nextLocation = indices[next];

            if (currLocation != nextLocation)
            {
                moves = RouteFinder.FindShortestRoute(currLocation, nextLocation, buttons);
            }

            moves += "A";


            _calculatedMoves.Add((curr, next), moves);

            return moves;
        }
    }
}