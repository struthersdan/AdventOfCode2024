namespace AdventOfCode2024.Puzzle5;

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
        return total;
    }


    public long SolveB()
    {
        var total = 0L;
        return total;
    }
}