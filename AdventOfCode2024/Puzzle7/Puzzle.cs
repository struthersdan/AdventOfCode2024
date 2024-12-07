using System.Collections;
using System.Collections.Concurrent;

namespace AdventOfCode2024.Puzzle7;

internal class Puzzle(string inputName)
{
    private string[] Rows { get; set; } = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public long Solve()
    {
        var successfulAnswers = new HashSet<long>();
        var input = BuildInput();

        while (input.Count != 0)
        {
            var curr = input.Pop();
            if (successfulAnswers.Contains(curr.Answer)) continue;
            if (curr.IsFinished)
            {
                if (curr.IsSolved) successfulAnswers.Add(curr.Answer);
                continue;
            }

            input.Push(curr with {Set = BuildNewSet(curr.Set, Operators.Addition)});
            input.Push(curr with {Set = BuildNewSet(curr.Set, Operators.Multiplication)});
        }

        return successfulAnswers.Sum();
    }

    public long SolveB()
    {
        var successfulAnswers = new HashSet<long>();
        var input = BuildInput();
        while (input.Count != 0)
        {
            var curr = input.Pop();
            if (successfulAnswers.Contains(curr.Answer)) continue;
            if (curr.IsFinished)
            {
                if (curr.IsSolved) successfulAnswers.Add(curr.Answer);
                continue;
            }

            input.Push(curr with {Set = BuildNewSet(curr.Set, Operators.Addition)});
            input.Push(curr with {Set = BuildNewSet(curr.Set, Operators.Multiplication)});
            input.Push(curr with {Set = BuildNewSet(curr.Set, Operators.Concatenation)});
        }

        return successfulAnswers.Sum();
    }

    private long[] BuildNewSet(long[] currSet, Operators addition)
    {
        var newSet = new long[currSet.Length - 1];

        newSet[0] = addition switch
        {
            Operators.Addition => currSet[1] + currSet[0],
            Operators.Multiplication => currSet[1] * currSet[0],
            Operators.Concatenation => currSet[0] * (long) Math.Pow(10, (int) Math.Log10(currSet[1]) + 1) + currSet[1],
            _ => throw new ArgumentException("Unsupported operator", nameof(addition))
        };

        Array.Copy(currSet, 2, newSet, 1, newSet.Length - 1);

        return newSet;
    }

    private Stack<MathEquation> BuildInput()
    {
        var input = new Stack<MathEquation>();
        foreach (var t in Rows)
        {
            var parts = t.Split(':').Select(x => x.Trim()).ToArray();
            var set = parts[1].Split(' ').Select(x => long.Parse(x.Trim())).ToArray();
            input.Push(new MathEquation(long.Parse(parts[0]), set));
        }

        return input;
    }


    private readonly record struct MathEquation(long Answer, long[] Set)
    {
        private long First => Set[0];
        public bool IsFinished => First > Answer || Set.Length == 1;
        public bool IsSolved => Answer == First;
    }

    private enum Operators
    {
        Addition,
        Multiplication,
        Concatenation
    };
}