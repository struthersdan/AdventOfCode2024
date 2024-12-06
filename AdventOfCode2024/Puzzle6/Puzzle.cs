using System.Diagnostics;
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

        var input = BuildInput();

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
        var input = BuildInput();

        var (i, j) = FindStart(input);
        var direction = GetDirection(input[i][j]);

        var places = new HashSet<(int, int)>();

        while (HelperMethods.IsValidCoordinates( input.Length,input[0].Length, i, j))
        {
            var nextI = i + direction.y;
            var nextJ = j + direction.x;
            if (!HelperMethods.IsValidCoordinates( input.Length,input[0].Length, nextI, nextJ))
            {
                places.Add((i, j));
                break;
            }
            var next = input[nextI][nextJ];

            if (next is not '#')
            {
                if (input[i][j] != '^') 
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
        }

        return CountBlockers(places, input);
    }

    private char[][] BuildInput()
    {
        var input = new char[Rows.Length][];
        for (int row = 0; row < Rows.Length; row++)
        {
            input[row] = Rows[row].ToCharArray();
        }

        return input;
    }

    private static long CountBlockers(HashSet<(int i, int j)> places, char[][] input)
    {
        var (i, j) = FindStart(input);
        var direction = GetDirection(input[i][j]);

        return places.AsParallel()
            .Select(x=> UpdateLocalInput(input, x))
            .Count(localInput => !TryToEscape(localInput, i, j, direction));
    }

    private static char[][] UpdateLocalInput(char[][] input, (int i, int j) place)
    {
        var localInput = new char[input.Length][];
        for (int row = 0; row < input.Length; row++)
        {
            localInput[row] = row == place.i
                ? input[row].Select((c, col) => col == place.j ? '#' : c).ToArray()
                : input[row];
        }
        return localInput;
    }

    private static bool TryToEscape(char[][] input, int i, int j, StepDirection direction)
    {
        var steps = new HashSet<(int, int, StepDirection)> ();

        while (HelperMethods.IsValidCoordinates( input.Length,input[0].Length, i, j))
        {
            if (!steps.Add((i, j, direction))) return false;
            var nextI = i + direction.y;
            var nextJ = j + direction.x;
            if (!HelperMethods.IsValidCoordinates(input.Length,input[0].Length, nextI, nextJ)) break;
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

    private record struct StepDirection(int y, int x);
}