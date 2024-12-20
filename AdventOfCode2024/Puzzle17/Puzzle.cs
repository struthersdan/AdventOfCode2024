using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle17;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
        _computer = new Computer();
        foreach (var row in Rows)
        {
            if (string.IsNullOrEmpty(row)) continue;
            var match = r.Match(row);


            if (match.Groups["label"].Value.Contains("A"))
            {
                _computer.A = long.Parse(match.Groups["value"].Value);
            }
            else if (match.Groups["label"].Value.Contains("B"))
            {
                _computer.B = long.Parse(match.Groups["value"].Value);
            }
            else if (match.Groups["label"].Value.Contains("C"))
            {
                _computer.C = long.Parse(match.Groups["value"].Value);
            }
            else
            {
                _computer.Instructions = match.Groups["value"].Value.Split(',').Select(ushort.Parse).ToArray();
            }
        }
    }

    private Regex r = new Regex(@"(?'label'[^\:]*)\:\s(?'value'.*)");
    private readonly Computer _computer;

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public string Solve()
    {
        _computer.Operate();

        return string.Join(',', _computer.Output);
    }


    public long SolveB()
    {
        var stack = new Stack<State>();
        stack.Push(new State(24, _computer.Instructions.Length - 2));

        var totals = new List<long>();
        while (stack.Any())
        {
            var curr = stack.Pop();

            for (var j = 0; j < 8; j++)
            {
                if (CalculateNext(curr, j))
                {
                    if (curr.Index == 0)
                    {
                        var total = curr.Total + j;
                        totals.Add(total);
                    }
                    else
                    {
                        stack.Push(new State((curr.Total + j) * 8, curr.Index - 1));
                    }
                }
            }
        }

        return totals.Min();


        bool CalculateNext(State state, int j)
        {
            var expected = _computer.Instructions[state.Index];
            var a = state.Total + j;
            var b = a % 8;
            b ^= 5;
            var c = (long) Math.Truncate(a / Math.Pow(2, b));
            b ^= c;
            b ^= 6;
            b %= 8;
            return b == expected;
        }
    }

    private record State(long Total, int Index);
}