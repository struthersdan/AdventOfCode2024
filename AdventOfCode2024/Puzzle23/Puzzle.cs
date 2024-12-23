using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle23;

internal class Puzzle
{
    private string[] Rows { get; set; }


    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
 
        foreach (var row in Rows)
        {
            var match = r.Match(row);

            _connections.Add((match.Groups["first"].Value, match.Groups["second"].Value));
        }
    }

    private readonly HashSet<(string, string)> _connections = new HashSet<(string, string)>();

    private readonly Regex r = new Regex(@"(?'first'[a-z]*)\-(?'second'[a-z]*)");

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
        return 0;
    }
}