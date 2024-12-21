using System.Text.RegularExpressions;
using AdventOfCode2024.Puzzle17;

namespace AdventOfCode2024.Puzzle19;

internal class Puzzle
{
    private readonly List<string> _towels = new List<string>();
    private readonly List<string> _combos = new List<string>();

    public Puzzle(string inputName)
    {
        var i = 0;
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

        var row = Rows[i];
        while (!string.IsNullOrEmpty(row))
        {
            _towels.AddRange(row.Split(',').Select(x=>x.Trim()));
            row = Rows[++i];
        }

        while (i < Rows.Length-1)
        {
            row = Rows[++i];
            _combos.Add(row);
        }
    }

   

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }


    readonly HashSet<string> _successfulCombinations = new();
    private HashSet<string> _unsuccessfulCombinations = new();

    public long Solve()
    {
        var total = _combos.Count(combo => IsSuccessfulCombination(combo, _towels.Where(combo.Contains).ToList()));
        return total;
    }

    private bool IsSuccessfulCombination(string combo, List<string> towels)
    {
        if (string.IsNullOrEmpty(combo)) return true;
        if (_unsuccessfulCombinations.Contains(combo))
        {
            return false;
        }
        if (_successfulCombinations.Contains(combo))
        {
            return true;
        }
        foreach (var towel in towels.Where(combo.StartsWith))
        {
            var subCombo = combo.Remove(0, towel.Length);
            var subTowels = _towels.Where(x => subCombo.Contains(x)).ToList();
            if (IsSuccessfulCombination(subCombo, subTowels))
            {
                _successfulCombinations.Add(combo);
                return true;
            }
        }

        _unsuccessfulCombinations.Add(combo);
        return false;
    }

    private readonly Dictionary<string, long> _successfulCombinationsCount = new();

    private long IsSuccessfulCombinations(string combo, List<string> towels)
    {
        if (string.IsNullOrEmpty(combo)) return 1;
        if (_successfulCombinationsCount.TryGetValue(combo, out var combinations))
        {
            return combinations;
        }

        if (_unsuccessfulCombinations.Contains(combo))
        {
            return 0;
        }


        var count = 0L;
        foreach (var towel in towels.Where(combo.StartsWith))
        {
            var subCombo = combo.Remove(0, towel.Length);
            var subTowels = _towels.Where(x => subCombo.Contains(x)).ToList();

            count += IsSuccessfulCombinations(subCombo, subTowels);

        }

        if (count == 0)
        {
            _unsuccessfulCombinations.Add(combo);
        }
        else
        {
            _successfulCombinationsCount.Add(combo, count);
        }
           
       
        return count;
    }



    public long SolveB()
    {
        return _combos.Sum(combo => IsSuccessfulCombinations(combo, _towels.Where(combo.Contains).ToList()));
    }

   
}