using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle20;

internal class Puzzle
{
    private readonly char[][] _raceTrack;

    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

        _raceTrack = Rows.Select(row => row.ToCharArray()).ToArray();
    }


    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }



    public long Solve(int minSavings)
    {
        var visited = new HashSet<(int i, int j)>();
        var start = HelperMethods.FindStart(_raceTrack, 'S');


        var positions = FindRaceTrack(start, visited);

        var count = CountCheats(minSavings, positions);
        return count;
    }

    private static long CountCheats(int minSavings, (int i, int j)[] positions)
    {
        var count = 0L;
        for (int i = 0; i < positions.Length; i++)
        {
            var curr = positions[i];
            for (int j = i + 2 + minSavings; j < positions.Length; j++)
            {
                var next = positions[j];
                if(next.i != curr.i && next.j != curr.j) continue;

                var yDist = Math.Abs(next.i - curr.i);
                var xDist = Math.Abs(next.j - curr.j);
                if (xDist == 2 || yDist == 2)
                {
                   // Console.WriteLine($"{curr}-{next} \n yDist:{yDist}, xDist{xDist}\n savings:{j - i - 2 }");
                    count++;
                }
            }
        }

        return count;
    }




    private (int i, int j)[] FindRaceTrack((int i, int j) start, HashSet<(int i, int j)> visited)
    {
        var racePositions = new List<(int, int)>();
        var stack = new Stack<(int i, int j)>();
        stack.Push(start);
        while (stack.Any())
        {
            var (i, j) = stack.Pop();
            if(!visited.Add((i, j))) continue;
            if(! _raceTrack.SafeAccess(i, j, out var c)) continue;
            if (c == 'E')
            {
                racePositions.Add((i, j));
                break;
            }
            if (c != '#')
            {
                racePositions.Add((i, j));
                stack.Push((i+1, j));
                stack.Push((i-1, j));
                stack.Push((i, j+1));
                stack.Push((i, j-1));
            }
        }

        return racePositions.ToArray();
    }


    public long SolveB(int minSavings)
    {
        var visited = new HashSet<(int i, int j)>();
        var start = HelperMethods.FindStart(_raceTrack, 'S');


        var positions = FindRaceTrack(start, visited);

        var count = CountCheatsB(minSavings, positions);
        return count;
    }

    private static long CountCheatsB(int minSavings, (int i, int j)[] positions)
    {
        var count = 0L;
        for (int i = 0; i < positions.Length; i++)
        {
            var curr = positions[i];
            for (int j = i + minSavings + 2; j < positions.Length; j++)
            {
                var next = positions[j];

                var manhattanDistance = ManhattanDistance(curr, next);
                if (manhattanDistance > 20) continue;

                if (j - i - manhattanDistance >= minSavings)
                {
                    count++;
                }
            }
        }

        return count;
    }


    private static int ManhattanDistance((int x, int y) point1, (int x, int y) point2)
    {
        return Math.Abs(point2.x - point1.x) + Math.Abs(point2.y - point1.y);
    }
   
}