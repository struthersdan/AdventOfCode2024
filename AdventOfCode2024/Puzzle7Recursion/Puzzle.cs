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
            if (CanSolve(set[0], 0, set, expected)) total += expected;
        }

        return total;
    }


    public long SolveB()
    {
        var total = 0L;
        var input = BuildInput();
        foreach (var (expected, set) in input)
        {
            if (CanSolve(set[0], 0, set, expected, true)) total += expected;
        }

        return total;
    }

    private static bool CanSolve(long runningTotal, int index, long[] set, long expected, bool concatenate = false)
    {
        if (runningTotal > expected) return false;
        if (index == set.Length - 1) return expected == runningTotal;

        var nextIndex = index + 1;
        var nextNum = set[nextIndex];

        return CanSolve(runningTotal + nextNum, nextIndex, set, expected, concatenate) ||
               CanSolve(runningTotal * nextNum, nextIndex, set, expected, concatenate) ||
               concatenate &&
               CanSolve(long.Parse($"{runningTotal}{nextNum}"), nextIndex, set, expected, concatenate);
    }

    private List<(long, long[])> BuildInput()
    {
        var input = new List<(long, long[])>();
        foreach (var t in Rows)
        {
            var parts = t.Split(':').Select(x => x.Trim()).ToArray();
            var set = parts[1].Split(' ').Select(x => long.Parse(x.Trim())).ToArray();
            input.Add((long.Parse(parts[0]), set));
        }

        return input;
    }
}