using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle15;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
        var i = 0;
        var row = Rows[0];
        var warehouse = new List<char[]>();
        while (!string.IsNullOrEmpty(row))
        {
            warehouse.Add(row.ToCharArray());
            i++;
            row = Rows[i];
        }

        i++;


        var commands = new List<char>();
        while (i < Rows.Length)
        {
            row = Rows[i];
            commands.AddRange(row.ToCharArray());
            i++;
        }


        _warehouse = warehouse.ToArray();
        _commands = commands.ToArray();
    }

    public static int Width { get; set; }

    public static int Height { get; set; }


    private readonly char[][] _warehouse;
    private readonly char[] _commands;


    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }


    private readonly HashSet<(int, int)> _visitedGardenPlots = new HashSet<(int, int)>();

    public long Solve()
    {
        var robotLocation = FindRobot();
        robotLocation = _commands.Aggregate(robotLocation, MoveRobot);

        return CountCoordinates('O');
    }

    private long CountCoordinates(char c)
    {
        var total = 0L;
        for (int i = 0; i < _warehouse.Length; i++)
        {
            for (int j = 0; j < _warehouse[i].Length; j++)
            {
                if (_warehouse[i][j] == c) total += 100 * i + j;
            }
        }

        return total;
    }


    private (int i, int j) MoveRobot((int i, int j) robotLocation, char command)
    {
        return command switch
        {
            '>' => TryMove(robotLocation, Right),
            '<' => TryMove(robotLocation, Left),
            'v' => TryMove(robotLocation, Down),
            '^' => TryMove(robotLocation, Up),
            _ => throw new ArgumentOutOfRangeException(nameof(command))
        };
    }


    public record MoveDirection(int x, int y);

    private readonly MoveDirection Up = new MoveDirection(0, -1);
    private readonly MoveDirection Down = new MoveDirection(0, 1);
    private readonly MoveDirection Left = new MoveDirection(-1, 0);
    private readonly MoveDirection Right = new MoveDirection(1, 0);

    private (int i, int j) TryMove((int i, int j) location, MoveDirection moveDirection)
    {
        var (i, j) = location;
        var curr = _warehouse[i][j];
        var (nextI, nextJ) = (i + moveDirection.y, j + moveDirection.x);
        var next = _warehouse[nextI][nextJ];
        if (next == '#') return (i, j);
        if (next == '.' || TryMove((nextI, nextJ), moveDirection) != (nextI, nextJ))
        {
            _warehouse[i][j] = _warehouse[nextI][nextJ];
            _warehouse[nextI][nextJ] = curr;
            return (nextI, nextJ);
        }

        return (i, j);
    }

    private (int i, int j) FindRobot()
    {
        for (int i = 0; i < _warehouse.Length - 1; i++)
        {
            for (int j = 0; j < _warehouse[i].Length - 1; j++)
            {
                if (_warehouse[i][j] == '@') return (i, j);
            }
        }

        throw new ArgumentOutOfRangeException(nameof(_warehouse));
    }


    public long SolveB()
    {
        ExpandWarehouse();
        WriteInput();
        var robotLocation = FindRobot();
        robotLocation = _commands.Aggregate(robotLocation, MoveRobotB);

        WriteInput();

        return CountCoordinates('[');
    }

    private void ExpandWarehouse()
    {
        for (int i = 0; i < _warehouse.Length; i++)
        {
            var newRow = new List<char>();
            foreach (var ch in _warehouse[i])
            {
                if (ch == 'O')
                    newRow.AddRange(['[', ']']);
                else if (new[] {'#', '.'}.Contains(ch))
                    newRow.AddRange([ch, ch]);
                else
                {
                    newRow.AddRange([ch, '.']);
                }
            }

            _warehouse[i] = newRow.ToArray();
        }
    }

    private (int i, int j) TryMoveB((int i, int j) location, MoveDirection moveDirection)
    {
        var (i, j) = location;
        var curr = _warehouse[i][j];
        var (nextI, nextJ) = (i + moveDirection.y, j + moveDirection.x);
        var next = _warehouse[nextI][nextJ];
        switch (next)
        {
            case '#':
                return (i, j);
            case '.':
                _warehouse[i][j] = _warehouse[nextI][nextJ];
                _warehouse[nextI][nextJ] = curr;
                return (nextI, nextJ);
            default:
            {
                (int, int) other = next == '[' ? (nextI, nextJ + 1) : (nextI, nextJ - 1);

                if (CanMove((nextI, nextJ), moveDirection) && CanMove(other, moveDirection))
                {
                    DoMove((nextI, nextJ), moveDirection);
                    DoMove(other, moveDirection);
                    _warehouse[i][j] = _warehouse[nextI][nextJ];
                    _warehouse[nextI][nextJ] = curr;
                    return (nextI, nextJ);
                }

                return (i, j);
            }
        }
    }

    private (int i, int j) MoveRobotB((int i, int j) robotLocation, char command)
    {
        return command switch
        {
            '>' => TryMove(robotLocation, Right),
            '<' => TryMove(robotLocation, Left),
            'v' => TryMoveB(robotLocation, Down),
            '^' => TryMoveB(robotLocation, Up),
            _ => throw new ArgumentOutOfRangeException(nameof(command))
        };
    }

    private void DoMove((int i, int j) location, MoveDirection moveDirection)
    {
        var (i, j) = location;
        var curr = _warehouse[i][j];
        var (nextI, nextJ) = (i + moveDirection.y, j + moveDirection.x);
        var next = _warehouse[nextI][nextJ];
        switch (next)
        {
            case '#':
                return;
            case '.':
                _warehouse[i][j] = _warehouse[nextI][nextJ];
                _warehouse[nextI][nextJ] = curr;
                return;
            default:
            {
                (int, int) other = next == '[' ? (nextI, nextJ + 1) : (nextI, nextJ - 1);
                DoMove((nextI, nextJ), moveDirection);
                DoMove(other, moveDirection);
                _warehouse[i][j] = _warehouse[nextI][nextJ];
                _warehouse[nextI][nextJ] = curr;
                return;
            }
        }
    }


    private bool CanMove((int i, int j) location, MoveDirection moveDirection)
    {
        var (i, j) = location;
        var curr = _warehouse[i][j];
        var (nextI, nextJ) = (i + moveDirection.y, j + moveDirection.x);
        var next = _warehouse[nextI][nextJ];
        switch (next)
        {
            case '#':
                return false;
            case '.':
                return true;
            default:
            {
                (int, int) other = next == '[' ? (nextI, nextJ + 1) : (nextI, nextJ - 1);
                return (CanMove((nextI, nextJ), moveDirection) && CanMove(other, moveDirection));
            }
        }
    }

    private void WriteInput()
    {
        var output = "";
        foreach (var t in _warehouse)
        {
            for (int j = 0; j < _warehouse[0].Length; j++)
            {
                output += (t[j]);
            }

            output += "\n";
        }

        Console.WriteLine(output);
        Thread.Sleep(100);
    }
}