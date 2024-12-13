namespace AdventOfCode2024.Puzzle12;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
        Input = Rows.Select(x => x.Select(i=> i).ToArray()).ToArray();
    }

    public char[][] Input { get; set; }

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    
    private readonly HashSet<(int, int)> _visitedGardenPlots = new HashSet<(int, int)>();

    public long Solve()
    {
        var total = 0L;
       
       
        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[0].Length; j++)
            {
                
                if (!_visitedGardenPlots.Add((i, j))) continue;
                var (area, perimeter) = (1L, 0L);
                var curr = Input[i][j];
                var result = FloodGarden(curr, i + 1, j, Direction.Down);
                area += result.area;
                perimeter += result.perimeter;
                result = FloodGarden(curr, i - 1, j, Direction.Up);
                area += result.area;
                perimeter += result.perimeter;
                result = FloodGarden(curr, i, j - 1, Direction.Left);
                area += result.area;
                perimeter += result.perimeter;
                result = FloodGarden(curr, i, j + 1, Direction.Right);
                area += result.area;
                perimeter += result.perimeter;

                total += area * perimeter;
            }
        }

        WriteInput(Input);

        return total;

    }

    private (long area, long perimeter) FloodGarden(char previous, int i, int j, Direction direction)
    {
        if (!Input.ContainsCoordinates(i, j)) return (0,1);
        var curr = Input[i][j];
        if (curr != previous) return (0, 1);
        if(!_visitedGardenPlots.Add((i, j))) return (0, 0);
        var (area, perimeter) = (1L, 0L);
        var result =  FloodGarden(curr, i + 1, j, Direction.Down);
        area+= result.area;
        perimeter += result.perimeter;
        result = FloodGarden(curr, i - 1, j, Direction.Up);
        area+= result.area;
        perimeter += result.perimeter;
        result = FloodGarden(curr, i, j - 1, Direction.Left);
        area+= result.area;
        perimeter += result.perimeter;
        result = FloodGarden(curr, i, j + 1, Direction.Right);
        area+= result.area;
        perimeter += result.perimeter;
        Console.WriteLine($"c:{curr} p:{previous} a:{area}p:{perimeter}");
        return (area, perimeter);
    }


    void WriteInput(char[][] input)
    {
        foreach (var t in input)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                Console.Write(t[j]);
            }

            Console.WriteLine();
        }
    }
    public enum Direction
    {
        Up, Down, Left, Right
    }


    public long SolveB()
    {
        
        var total = 0L;
       
       
        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                var (area, perimeter, corners) = FloodBulkGarden(null, i, j);
                total += area * corners;
            }
        }

        WriteInput(Input);

        return total;
    }

    private (long area, long perimeter, long corners) FloodBulkGarden(char? previous, int i, int j)
    {
        if (!Input.ContainsCoordinates(i, j)) return Side;
        Console.WriteLine($"{i}{j}");
        var curr = Input[i][j];
        if (previous != null && curr != previous) return Side;
        if(!_visitedGardenPlots.Add((i, j))) return (0, 0, 0);
        var (area, perimeter, corners) = (1L, 0L, 0L);
        var downResult =  FloodBulkGarden(curr, i + 1, j);
        var upResult = FloodBulkGarden(curr, i - 1, j);
        var leftResult = FloodBulkGarden(curr, i, j - 1);
        var rightResult = FloodBulkGarden(curr, i, j + 1);


        perimeter = perimeter + upResult.perimeter + downResult.perimeter + leftResult.perimeter +
                    rightResult.perimeter;

        area = area + upResult.area + downResult.area + leftResult.area + rightResult.area;

        corners = corners + upResult.corners + downResult.corners + leftResult.corners + rightResult.corners;

        var convexCorners = FindConvexCorners(upResult, rightResult, downResult, leftResult);

        var concaveCorners = FindConcaveCorners(i, j, upResult, rightResult, curr, downResult, leftResult);

        corners += concaveCorners;

        corners += convexCorners;
      
        Console.WriteLine($"c:{curr} p:{previous} a:{area} p{perimeter} c:{corners}");
        return (area, perimeter, corners);
    }

    private int FindConcaveCorners(int i, int j, (long area, long perimeter, long corners) upResult,
        (long area, long perimeter, long corners) rightResult, char curr,
        (long area, long perimeter, long corners) downResult, (long area, long perimeter, long corners) leftResult)
    {
        var concaveCorners = 0;
        if (upResult != Side  && rightResult != Side && Input[i-1][j+1] != curr)
        {
            concaveCorners++;
        }

        if (rightResult != Side && downResult != Side && Input[i + 1][j + 1] != curr)
        {
            concaveCorners++;
        }

        if (downResult != Side && leftResult != Side && Input[i + 1][j - 1] != curr)
        {
            concaveCorners++;
        }

        if (leftResult != Side && upResult != Side && Input[i - 1][j - 1] != curr)
        {
            concaveCorners++;
        }

        return concaveCorners;
    }

    private static long FindConvexCorners((long area, long perimeter, long corners) upResult,
        (long area, long perimeter, long corners) rightResult, (long area, long perimeter, long corners) downResult,
        (long area, long perimeter, long corners) leftResult)
    {
        var convexCorners = 0L;
        if (upResult == Side && rightResult == Side)
        {
            convexCorners++;
        }
        if (rightResult == Side && downResult == Side)
        {
            convexCorners++;
        }
        if (downResult == Side && leftResult == Side)
        {
            convexCorners++;
        }
        if (leftResult ==  Side && upResult ==  Side)
        {
            convexCorners++;
        }

        return convexCorners;
    }

    public static (int area, int perimeter, int corners) Side = (0, 1, 0);

    private static long Corners((long area, long perimeter) upResult)
    {
        return upResult.perimeter > 1 ? upResult.perimeter : 0;
    }
}