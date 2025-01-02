using System.Text.RegularExpressions;


namespace AdventOfCode2024.Puzzle24;

internal class Puzzle
{
    private string[] Rows { get; set; }
    private readonly List<Gate> _gates;
    private readonly Dictionary<string, (string label, bool value)> _startingValues;
    private readonly Regex _gateRegex = new Regex(@"(?'left'\w*)\s(?'operator'\w*)\s(?'right'\w*) \-\> (?'output'\w*)");
    private readonly Regex _startingValuesRegex = new Regex(@"(?'label'\S*):\s(?'value'\d)");

    public Puzzle(string inputName, int? expected = null)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

        var i = 0;
        var row = Rows[0];
        var startingValues = new Dictionary<string, (string label, bool value)>();

        while (!string.IsNullOrEmpty(row))
        {
            var match = _startingValuesRegex.Match(row);
            startingValues.Add(match.Groups["label"].Value,
                (match.Groups["label"].Value, match.Groups["value"].Value == "1"));
            i++;
            row = Rows[i];
        }

        _startingValues = startingValues;

        i++;


        var gates = new List<Gate>();
        while (i < Rows.Length)
        {
            row = Rows[i];
            var match = _gateRegex.Match(row);

            var op = Enum.Parse<Operator>(match.Groups["operator"].Value);
            var left = match.Groups["left"].Value;
            var right = match.Groups["right"].Value;
            var output = match.Groups["output"].Value;

            gates.Add(new Gate( left, right, output, op));

            i++;
        }

        _gates = gates;
    }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public long Solve()
    {
        return CalculateReturnValue();
    }

    private long CalculateReturnValue()
    {
     
        foreach (var gate in _gates)
        {
            FindOutputValue(gate.Output);
        }

        var zGateValues = _gates
            .Where(x => x.Output.StartsWith('z') && x.Value != null)
            .OrderByDescending(x => x.Output)
            .Select(x => x.Value!.Value ? 1 : 0);
        return Convert.ToInt64(string.Join("", zGateValues), 2);

        bool FindOutputValue(string wire)
        {
            var findGate = _gates.FirstOrDefault(x => x.Output == wire);
            if (findGate is not { } gate) return _startingValues[wire].Item2;
            if (gate.Value is { } value) return value;

            var leftValue = FindOutputValue(gate.LeftInput);
            var rightValue = FindOutputValue(gate.RightInput);
            gate.Value = gate.Op switch
            {
                Operator.OR => leftValue | rightValue,
                Operator.AND => leftValue & rightValue,
                Operator.XOR => leftValue ^ rightValue,
                _ => throw new ArgumentOutOfRangeException()
            };

            Console.WriteLine($" ");
            return (bool) gate.Value;
        }
    }




    public string SolveB()
    {
        foreach (var gate in _gates.Where(x => x.Output.StartsWith('z')).OrderBy(x => x.Output))
        {
            switch (gate.Op)
            {
                case Operator.OR:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=diamond, label=\"{gate.Op}\"]");
                    break;
                case Operator.AND:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=square,  label=\"{gate.Op}\"]");
                    break;
                case Operator.XOR:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=star, label=\"{gate.Op}\"]");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Console.WriteLine($"{{{gate.LeftInput}, {gate.RightInput}}} -> {_gates.IndexOf(gate)} -> {gate.Output};");
        }

        foreach (var gate in _gates.Where(x=>x.LeftInput.StartsWith('x') && !x.Output.StartsWith('z')).OrderBy(x => x.LeftInput))
        {
            switch (gate.Op)
            {
                case Operator.OR:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=diamond, label=\"{gate.Op}\"]");
                    break;
                case Operator.AND:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=square,  label=\"{gate.Op}\"]");
                    break;
                case Operator.XOR:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=star, label=\"{gate.Op}\"]");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Console.WriteLine($"{{{gate.LeftInput}, {gate.RightInput}}} -> {_gates.IndexOf(gate)} -> {gate.Output};");
        }

        foreach (var gate in _gates.Where(x=> !x.Output.StartsWith('z') && !x.LeftInput.StartsWith('x')).OrderByDescending(x => x.LeftInput))
        {
            switch (gate.Op)
            {
                case Operator.OR:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=diamond, label=\"{gate.Op}\"]");
                    break;
                case Operator.AND:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=square,  label=\"{gate.Op}\"]");
                    break;
                case Operator.XOR:
                    Console.WriteLine($"{_gates.IndexOf(gate)}[shape=star, label=\"{gate.Op}\"]");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Console.WriteLine($"{{{gate.LeftInput}, {gate.RightInput}}} -> {_gates.IndexOf(gate)} -> {gate.Output};");
        }

        return string.Join(",", GenerateSwaps([]).Keys.Distinct().Order());
    }


    private Dictionary<string, string> GenerateSwaps(Dictionary<string, string> swaps)
    {
        var count = Convert.ToInt32(_startingValues.Keys
            .Where(k => k.StartsWith('x'))
            .Order()
            .Last()[1..]);

        var co = GetOut(swaps, "x00", "y00", Operator.AND);
        for (int i = 1; i <= count; i++)
        {
            void Swap(string a, string b)
            {
                swaps.Add(a, b);
                swaps.Add(b, a);
                i--;
            }

            var x = $"x{i:d2}";
            var y = $"y{i:d2}";
            var z = $"z{i:d2}";

            var xor = GetOut(swaps, x, y, Operator.XOR)!;
            var and = GetOut(swaps, x, y, Operator.AND)!;

            var cXor = GetOut(swaps, co, xor, Operator.XOR);
            var cAnd = GetOut(swaps, co, xor, Operator.AND);

            if (cXor == null && cAnd == null)
            {
                Swap(xor, and);
                continue;
            }

            if (cXor != z)
            {
                Swap(cXor!, z);
                continue;
            }

            co = GetOut(swaps, and, cAnd, Operator.OR);
        }
        return swaps;
    }
    private string? GetOut(Dictionary<string, string> swaps, string? in1, string? in2, Operator op)
        => ApplySwaps(swaps, GetOut(in1, in2, op) ?? GetOut(in2, in1, op));
    private string? GetOut(string? in1, string? in2, Operator op)
        => _gates.FirstOrDefault(r => r.LeftInput == in1 && r.RightInput == in2 && r.Op == op)?.Output;

    private static string? ApplySwaps(Dictionary<string, string> swaps, string? @out)
        => @out is not null && swaps.TryGetValue(@out, out var result) ? result : @out;


}

