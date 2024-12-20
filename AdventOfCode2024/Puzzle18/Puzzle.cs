using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle18;

internal class Puzzle
{
    private readonly int _height;
    private readonly int _width;
    private readonly (int i, int j)[] _bytes; 
    private readonly char[][] _memorySpace;

    public Puzzle(string inputName, int height, int width, int count)
    {
        _height = height;
        _width = width;
        _count = count;
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

        var bytes = new List<(int i, int j)>();
        foreach (var row in Rows)
        {
            var match = _r.Match(row);
            bytes.Add((int.Parse(match.Groups["y"].Value), int.Parse(match.Groups["x"].Value)));
        }

        _bytes = bytes.ToArray();

        _memorySpace = new char[_height][];

        var memorySpace = new List<char[]>();
        for (int i = 0; i < _height; i++)
        {
            var row = new List<char>();
            for (int j = 0; j < _width; j++)
            {
                row.Add('.');
            }
            memorySpace.Add(row.ToArray());
        }

        _memorySpace = memorySpace.ToArray();
    }

    private readonly Regex _r = new Regex(@"(?'x'\d*),(?'y'\d*)");
    private readonly int _count;


    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public long Solve()
    {
        _memorySpace[0][0] = 'S';
        _memorySpace[_height-1][_width-1] = 'E';
        for (int i = 0; i < _count; i++)
        {
            var currByte = _bytes[i];
            _memorySpace[currByte.i][currByte.j] = '#';
        }

        HelperMethods.WriteInput(_memorySpace);

        var queue = new Queue<State>();
        queue.Enqueue(new State((0, 0), 0, []));

        var routeCounts = new List<int>();

        while (queue.TryDequeue(out var curr))
        {
            var (location, runningTotal, visited) = curr;
            if(!visited.Add(location)) continue;
            var (i, j) = location;
            if (!_memorySpace.ContainsCoordinates(i, j)) continue;
            if (_memorySpace[i][j] == '#') continue;

            if (_memorySpace[i][j] == 'E')
            {
                routeCounts.Add(runningTotal);
            }

            queue.Enqueue(new State((i+1, j), runningTotal+1, visited));
            queue.Enqueue(new State((i-1, j), runningTotal+1, visited));
            queue.Enqueue(new State((i, j+1), runningTotal+1, visited));
            queue.Enqueue(new State((i, j-1), runningTotal+1, visited));
        }

        return routeCounts.Min();
    }



    public record State((int i, int j) location, int runningTotal, HashSet<(int i, int j)> visited);


    public (int i, int j) SolveB()
    {
        _memorySpace[0][0] = 'S';
        _memorySpace[_height-1][_width-1] = 'E';
        for (int i = 0; i < _count; i++)
        {
            var currByte = _bytes[i];
            _memorySpace[currByte.i][currByte.j] = '#';
        }


        for (int i = _count; i < _bytes.Length; i++)
        {
            var currByte = _bytes[i];
            _memorySpace[currByte.i][currByte.j] = '#';

            if (!FindExit())
            {
                return currByte;
            }
        }

        HelperMethods.WriteInput(_memorySpace);

        throw new UnreachableException();
    }

    private bool FindExit()
    {
        var queue = new Queue<State>();
        queue.Enqueue(new State((0, 0), 0, []));

        var routeCounts = new List<int>();

        while (queue.TryDequeue(out var curr))
        {
            var (location, runningTotal, visited) = curr;
            if(!visited.Add(location)) continue;
            var (i, j) = location;
            if (!_memorySpace.ContainsCoordinates(i, j)) continue;
            switch (_memorySpace[i][j])
            {
                case '#':
                    continue;
                case 'E':
                    routeCounts.Add(runningTotal);
                    continue;
            }

            queue.Enqueue(new State((i+1, j), runningTotal+1, visited));
            queue.Enqueue(new State((i-1, j), runningTotal+1, visited));
            queue.Enqueue(new State((i, j+1), runningTotal+1, visited));
            queue.Enqueue(new State((i, j-1), runningTotal+1, visited));
        }

        return routeCounts.Any();
    }
}