using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle13;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

        var currMachine = new GameMachine();
        foreach (var row in Rows)
        {
            if (string.IsNullOrEmpty(row))
            { 
                continue;
            }

            var input = regex.Match(row);

            var rowName = input.Groups["title"].Value;
            var x = int.Parse(input.Groups["x"].Value);
            var y = int.Parse(input.Groups["y"].Value);
            if (rowName.Contains("A"))
            {
                currMachine.a =
                    new ButtonSetting(x, y);
            }else if (rowName.Contains("B"))
            {
                currMachine.b = new ButtonSetting(x, y);
            }
            else
            {
                currMachine.p = new PrizeCoordinate(x, y);
                Games.Add(currMachine);
                currMachine = new GameMachine();
            }

        }
    }

    private Regex regex = new Regex(@"(?'title'.*)\:\sX.{1}(?'x'\d*)\,\s*Y.{1}(?'y'\d*)");


    public readonly List<GameMachine> Games = new List<GameMachine>();

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    
    private readonly HashSet<(int, int)> _visitedGardenPlots = new HashSet<(int, int)>();

    public long Solve()
    {
        return Games.Sum(gameMachine => gameMachine.TryGetSolve());
    }







    public long SolveB()
    {

        return Games.Sum(gameMachine => gameMachine.TryGetSolveB());
    }
}

internal record PrizeCoordinate(int x, int y);

internal class GameMachine
{
    public ButtonSetting a { get; set; }
    public ButtonSetting b { get; set; }
    public PrizeCoordinate p { get; set; }

    public long TryGetSolve()
    {
        var AMoves = ((decimal)b.y * p.x - b.x * p.y) / (a.x * b.y - a.y * b.x);
        var BMoves = ((decimal)a.x * p.y - a.y * p.x) / (a.x * b.y - a.y * b.x);
        if (AMoves < 100 && BMoves < 100 && AMoves % 1 == 0 && BMoves % 1 == 0)
        {
            return (int)AMoves * 3 + (int)BMoves;
        }

        return 0;
    }

    public long TryGetSolveB()
    {
      var x = p.x +  10000000000000;
       var y =  p.y + 10000000000000;
       var aMoves = ((decimal)b.y * x - b.x * y) / (a.x * b.y - a.y * b.x);
       var bMoves = ((decimal)a.x * y - a.y * x) / (a.x * b.y - a.y * b.x);
       if (aMoves % 1 == 0 && bMoves % 1 == 0)
       {
           return (long)aMoves * 3 + (long)bMoves;
       }

       return 0;
    }
}

internal record ButtonSetting(int x, int y);