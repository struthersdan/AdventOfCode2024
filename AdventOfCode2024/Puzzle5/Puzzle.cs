using System.Collections;
using System.Net;

namespace AdventOfCode2024.Puzzle5;

internal class Puzzle(string inputName)
{
    public string[] Rules {get;set;} = File.ReadAllLines(GetInputNameInFolder("rules.txt"));
    private string[] Rows { get; set; } = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public long Solve()
    {
        var ruleDict = BuildRules();

        return Rows.Select(x => x.Split(",").Select(int.Parse).ToArray())
            .Where(row => CheckIfRowIsValid(row, ruleDict))
            .Aggregate(0L, (current, validRow) => current + validRow[validRow.Length / 2]);
    }


    public long SolveB()
    {
        var total = 0L;

        var ruleDict = BuildRules();

        var invalidRows = new Stack<int[]>();
        foreach(var row in Rows.Select(x=>x.Split(",").Select(int.Parse).ToArray()))
        {
            var isValid = CheckIfRowIsValid(row, ruleDict);

            if(!isValid) invalidRows.Push(row);
        }


        List<int[]> validRows = new();
        while(invalidRows.Any())
        {
            var row = invalidRows.Pop();
            //Console.WriteLine(string.Join("-", row));
            bool isValid = true;
            for (var i = 0; i < row.Length; i++)
            {
                var number = row[i];
              
                ruleDict.TryGetValue(number, out var rule);
                if(rule == null) continue;
                for (var j = i+1; j < row.Length; j++)
                {
                    if (rule.Contains(row[j]))
                    {
                        isValid = false;
                        var curr = row[j];
                        row[j] = number;
                        row[i] = curr;
                        break;
                    }
                }

               
            }

            switch (isValid)
            {
                case false:
                    invalidRows.Push(row);
                    break;
                case true:
                    validRows.Add(row);
                    break;
            }
        }
       
        
        foreach (var validRow in validRows)
        {
            total += validRow[validRow.Length / 2];
        }
        

        return total;
    }

    private static bool CheckIfRowIsValid(int[] row, Dictionary<int, List<int>> ruleDict)
    {
        bool isValid = true;
        for (var i = 0; i < row.Length; i++)
        {
            var number = row[i];
            ruleDict.TryGetValue(number, out var rule);
            if(rule == null) continue;
            for (var j = i + 1; j < row.Length; j++)
            {
                if (rule.Contains(row[j])) isValid = false;
                break;
            }

            if (!isValid) break;
        }

        return isValid;
    }

    private Dictionary<int, List<int>> BuildRules()
    {
        Dictionary<int, List<int>> ruleDict = new Dictionary<int, List<int>>();
        foreach(var rule in Rules)
        {
            var numbers = rule.Split('|').Select(int.Parse).ToArray();
            var right = numbers[1];

            ruleDict.TryGetValue(right, out var existing);

            if(existing!= null) existing.Add(numbers[0]);
            else ruleDict.Add(right, [numbers[0]]);

        }

        return ruleDict;
    }
}