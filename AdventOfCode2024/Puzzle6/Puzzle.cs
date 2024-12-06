using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2024.Puzzle6;

internal class Puzzle(string inputName)
{
    private string[] Rows { get; set; } = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public long Solve()
    {
        var total = 1L;

        var input = Rows.Select(x => x.ToCharArray()).ToArray();

        var (i, j) = FindStart(input);

        var curr = input[i][j];

        var direction = GetDirection(curr);
        while (input.ContainsCoordinates(i, j))
        {
            var nextI = i + direction.y;
            var nextJ = j + direction.x;
            if (!input.ContainsCoordinates(nextI, nextJ)) break;
            var next = input[nextI][nextJ];

            if (next == '.' || next == 'x')
            {
                if (next == '.')
                {
                    total++;
                }

                input[i][j] = 'x';
                input[nextI][nextJ] = curr;
                i = nextI;
                j = nextJ;
            }
            else if (next == '#')
            {
                direction = GetNextDirection(direction);
            }
        }


        return total;
    }

    private static void WriteInput(char[][] input)
    {
        foreach (var c in input)
        {
            foreach (var c1 in c)
            {
                Console.Write(c1);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static StepDirection GetNextDirection(StepDirection direction)
    {
        if (direction == Up) return Right;
        if (direction == Down) return Left;
        if (direction == Left) return Up;
        return Down;
    }

    private static readonly StepDirection Up = new(-1, 0);
    private static readonly StepDirection Left = new(0, -1);
    private static readonly StepDirection Right = new(0, 1);
    private static readonly StepDirection Down = new(1, 0);

    private static StepDirection GetDirection(char curr)
    {
        switch (curr)
        {
            case '^':
                return Up;
            case '>':
                return Right;
            case '<': return Left;
            case 'v': return Down;
            default: throw new Exception("invalid direction");
        }
    }

    private static (int i, int j) FindStart(char[][] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (new char[] {'^', '>', '<', 'v'}.Contains(input[i][j])) return (i, j);
            }
        }

        throw new Exception("invalid start");
    }


    public long SolveB()
    {
        var input = Rows.Select(x => x.ToCharArray()).ToArray();

        var (i, j) = FindStart(input);

        var curr = input[i][j];

        var direction = GetDirection(curr);
        var places = new HashSet<(int i, int j)> ();
        while (input.ContainsCoordinates(i, j))
        {
            var nextI = i + direction.y;
            var nextJ = j + direction.x;
            if (!input.ContainsCoordinates(nextI, nextJ))
            {
                places.Add((i, j));
                break;
            }
            var next = input[nextI][nextJ];

            if (next is not '#')
            {
                if (curr != '^') 
                {
                    places.Add((i, j));
                }

                i = nextI;
                j = nextJ;
            }
            else if (next == '#')
            {
                direction = GetNextDirection(direction);
            }
            curr = next;
        }

        return CountBlockers(places, input);
    }

    private static long CountBlockers(HashSet<(int i, int j)> places, char[][] input)
    {
        var count = 0L;
        foreach (var (i,j) in places)
        {
            input[i][j] = '#';
            if (!TryToEscape(input)) count++;
            input[i][j] = '.';
        }

        return count;
    }

    private static bool TryToEscape(char[][] input)
    {
        var (i, j) = FindStart(input);
        var direction = GetDirection(input[i][j]);

        var steps = new HashSet<Vector> ();
        while (input.ContainsCoordinates(i, j))
        {
            var vector = new Vector(i, j, direction);
            if (!steps.Add(vector)) return false;
            var nextI = i + direction.y;
            var nextJ = j + direction.x;
            if (!input.ContainsCoordinates(nextI, nextJ)) break;
            var next = input[nextI][nextJ];

            if (next is not '#')
            {
                i = nextI;
                j = nextJ;
            }
            else
            {
                direction = GetNextDirection(direction);
            }
        }

        return true;
    }

    private record Vector(int i, int j, StepDirection direction);
    internal record StepDirection(int y, int x);
}