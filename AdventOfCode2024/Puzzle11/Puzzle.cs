namespace AdventOfCode2024.Puzzle11;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
    }

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    private readonly Dictionary<(long stone, int remainingBlinks), long> _memo = new Dictionary<(long, int), long>();

    public long Solve()
    {
        var input = Rows.Select(x =>
            x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse($"{i}")).ToArray()).First();
        return input.Sum(l => Blink(l, 25));
    }

    public long SolveB()
    {
        var input = Rows.Select(x =>
            x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse($"{i}")).ToArray()).First();
        return input.Sum(l => Blink(l, 75));
    }

    private long Blink(long stone, int remainingBlinks)
    {
        if (_memo.TryGetValue((stone, remainingBlinks), out var memoCount)) return memoCount;
        var nextBlink = remainingBlinks - 1;
        var count = 0L;
        if (remainingBlinks == 0) return 1;
        if (stone == 0)
        {
            count += Blink(1, nextBlink);
        }
        else if ($"{stone}".Length % 2 == 0)
        {
            var middle = $"{stone}".Length / 2;
            var firstNumber = long.Parse($"{stone}"[..(middle)]);
            var secondNumber = long.Parse($"{stone}"[(middle)..]);
            count += Blink(firstNumber, nextBlink);
            count += Blink(secondNumber, nextBlink);

        }
        else
        {
            count += Blink(stone * 2024, nextBlink);
        }

        _memo.Add((stone, remainingBlinks), count);
        return count;
    }
}