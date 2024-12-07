using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AdventOfCode2024.Puzzle7Recursion;

internal class Puzzle(string inputName)
{
    private string[] Rows { get; set; } = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public long Solve()
    {
        var total = 0L;
        var input = BuildInput();

        foreach (var (expected, set) in input)
        {
            if (CanSolve(set[^1], set.Length-1, set, expected)) total += expected;
        }

        return total;
    }

    public long SolveB()
    {
        var total = 0L;
        var input = BuildInput();
        foreach (var (expected, set) in input)
        {
            if (CanSolve(set[^1], set.Length-1, set, expected, true)) total += expected;
        }

        return total;
    }

    private static bool CanSolve(long current, int index, long[] set, long currentRemainder, bool concatenate = false)
    {
        if (current > currentRemainder) return false;
        if (index == 0) return currentRemainder == current;

        var nextIndex = index - 1;
        var nextNum = set[nextIndex];

        if (currentRemainder % current == 0 && CanSolve(nextNum, nextIndex, set, currentRemainder / current, concatenate))
            return true;
        if (currentRemainder >= current && CanSolve(nextNum, nextIndex, set, currentRemainder - current, concatenate))
            return true;
        if (!concatenate) return false;

        var remainderString = currentRemainder.ToString();
        var currentString = current.ToString();

        if (!remainderString.EndsWith(currentString)) return false;

        var nextString = remainderString[..^currentString.Length];

        if (nextString == "") return false;

        var nextRemainder = long.Parse(nextString);

        return CanSolve(nextNum, nextIndex, set, nextRemainder, concatenate);
    }

    private List<(long, long[])> BuildInput()
    {
        return (from t in Rows
            select t.Split(':').Select(x => x.Trim()).ToArray()
            into parts
            let set = parts[1].Split(' ').Select(x => long.Parse(x.Trim())).ToArray()
            select (long.Parse(parts[0]), set)).ToList();
    }
}