using System.Collections;
using System.ComponentModel.Design;
using static AdventOfCode2024.HelperMethods;
using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode2024.Puzzle16;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
        _maze = Rows.Select(x => x.ToCharArray()).ToArray();
    }

    private readonly char[][] _maze;

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    private readonly Dictionary<Vector, long> _shortestByLocation = new Dictionary<Vector, long>();

    public long Solve()
    {
        var shortest = 1000000L;
        var start = HelperMethods.FindStart(_maze, 'S');
        var stack = new PriorityQueue<Runner, long>();
        stack.Enqueue(new Runner(new Vector(start, GridDirection.Right), 0, []), 0);
        while (stack.TryDequeue(out var curr, out _))
        {
            var (vector, runningTotal, visited) = curr;
            if(!visited.Add(vector)) continue;

            if(_shortestByLocation.TryGetValue(vector, out var currShortest ) && currShortest <= runningTotal) continue;
            _shortestByLocation[vector] = runningTotal;

            var (location, direction) = vector;
            var move = GetMoveDirection(direction);
           
            var currCharacter = _maze[location.i][location.j];
            if (currCharacter == '#') continue;
            if (currCharacter == 'E')
            {
                return runningTotal;
            }

            var ccVector = new Vector(location, RotateCounter(direction));
            if (!visited.Contains(ccVector)) 
            {
                var moveDirection = GetMoveDirection(ccVector.direction);
                if (_maze[location.i + moveDirection.y][location.j + moveDirection.x] != '#')
                {
                    var newRunning = runningTotal + 1000;
                    stack.Enqueue(new(ccVector, newRunning, [..visited]), newRunning);
                }
            }
            var cwVector = new Vector(location, RotateClockWise(direction));
            if (!visited.Contains(cwVector))
            {
                var moveDirection = GetMoveDirection(cwVector.direction);
                if (_maze[location.i + moveDirection.y][location.j + moveDirection.x] != '#')
                {
                    var newRunning = runningTotal + 1000;
                    stack.Enqueue(new (cwVector, newRunning, [..visited]), newRunning);
                }
            }

            (int i, int j ) next = (location.i + move.y, location.j + move.x);
            var straightVector = new Vector(next, direction);
            if (!visited.Contains(straightVector) && _maze[next.i][next.j] != '#' )
            {
                var newRunning = runningTotal + 1;
                stack.Enqueue(new Runner(straightVector, newRunning, [..visited]), newRunning);
            }
           
        }

        return shortest;
    }

    public record struct Vector(
        (int i, int j) location,
        GridDirection direction);

    public record struct Runner(Vector vector, int runningTotal, HashSet<Vector> visited);

    
  

   

    public MoveDirection GetMoveDirection(GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => Up,
            GridDirection.Down => Down,
            GridDirection.Left => Left,
            GridDirection.Right => Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public record MoveDirection(int x, int y);

    private readonly MoveDirection Up = new MoveDirection(0, -1);
    private readonly MoveDirection Down = new MoveDirection(0, 1);
    private readonly MoveDirection Left = new MoveDirection(-1, 0);
    private readonly MoveDirection Right = new MoveDirection(1, 0);


  

    public long SolveB()
    {
        var visitedTiles = new HashSet<(int, int)>();
        var shortest = 1000000L;
        var start = HelperMethods.FindStart(_maze, 'S');
        var stack = new PriorityQueue<Runner, long>();
        stack.Enqueue(new Runner(new Vector(start, GridDirection.Right), 0, []), 0);
        while (stack.TryDequeue(out var curr, out _))
        {
            var (vector, runningTotal, visited) = curr;
            if(!visited.Add(vector)) continue;

            if(_shortestByLocation.TryGetValue(vector, out var currShortest ) && currShortest <= runningTotal) continue;
            _shortestByLocation[vector] = runningTotal;

            var (location, direction) = vector;
            var move = GetMoveDirection(direction);
           
            var currCharacter = _maze[location.i][location.j];
            if (currCharacter == '#') continue;
            if (currCharacter == 'E')
            {
                shortest = runningTotal;

                break;
            }

            var ccVector = new Vector(location, RotateCounter(direction));
            if (!visited.Contains(ccVector)) 
            {
                var moveDirection = GetMoveDirection(ccVector.direction);
                if (_maze[location.i + moveDirection.y][location.j + moveDirection.x] != '#')
                {
                    var newRunning = runningTotal + 1000;
                    stack.Enqueue(new(ccVector, newRunning, [..visited]), newRunning);
                }
            }
            var cwVector = new Vector(location, RotateClockWise(direction));
            if (!visited.Contains(cwVector))
            {
                var moveDirection = GetMoveDirection(cwVector.direction);
                if (_maze[location.i + moveDirection.y][location.j + moveDirection.x] != '#')
                {
                    var newRunning = runningTotal + 1000;
                    stack.Enqueue(new (cwVector, newRunning, [..visited]), newRunning);
                }
            }

            (int i, int j ) next = (location.i + move.y, location.j + move.x);
            var straightVector = new Vector(next, direction);
            if (!visited.Contains(straightVector) && _maze[next.i][next.j] != '#' )
            {
                var newRunning = runningTotal + 1;
                stack.Enqueue(new Runner(straightVector, newRunning, [..visited]), newRunning);
            }

           
        }

        stack.Enqueue(new Runner(new Vector(start, GridDirection.Right), 0, []), 0);
        _shortestByLocation.Clear();
        while (stack.TryDequeue(out var curr, out _))
        {
            var (vector, runningTotal, visited) = curr;
            if(!visited.Add(vector)) continue;

            if(_shortestByLocation.TryGetValue(vector, out var currShortest ) && currShortest < runningTotal) continue;
            _shortestByLocation[vector] = runningTotal;

            var (location, direction) = vector;
            var move = GetMoveDirection(direction);
           
            var currCharacter = _maze[location.i][location.j];
            if (currCharacter == '#') continue;
            if (currCharacter == 'E')
            {
                if (runningTotal == shortest)
                {
                    foreach (var vector1 in visited)
                    {
                        visitedTiles.Add(vector1.location);
                    }
                }
            }

            var ccVector = new Vector(location, RotateCounter(direction));
            if (!visited.Contains(ccVector)) 
            {
                var moveDirection = GetMoveDirection(ccVector.direction);
                if (_maze[location.i + moveDirection.y][location.j + moveDirection.x] != '#')
                {
                    var newRunning = runningTotal + 1000;
                    stack.Enqueue(new(ccVector, newRunning, [..visited]), newRunning);
                }
            }
            var cwVector = new Vector(location, RotateClockWise(direction));
            if (!visited.Contains(cwVector))
            {
                var moveDirection = GetMoveDirection(cwVector.direction);
                if (_maze[location.i + moveDirection.y][location.j + moveDirection.x] != '#')
                {
                    var newRunning = runningTotal + 1000;
                    stack.Enqueue(new (cwVector, newRunning, [..visited]), newRunning);
                }
            }

            (int i, int j ) next = (location.i + move.y, location.j + move.x);
            var straightVector = new Vector(next, direction);
            if (!visited.Contains(straightVector) && _maze[next.i][next.j] != '#' )
            {
                var newRunning = runningTotal + 1;
                stack.Enqueue(new Runner(straightVector, newRunning, [..visited]), newRunning);
            }

           
        }

        return visitedTiles.Count;
    }
}