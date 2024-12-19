using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle14;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        var index = 0;
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");


        foreach (var row in Rows)
        {
            if (index == 0)
            {
                Height = int.Parse(row);
                index++;
            }
            else if (index == 1)
            {
                Width = int.Parse(row);
                index++;
            }
            else
            {
                var text = regex.Match(row);
                var i = int.Parse(text.Groups["i"].Value);
                var x = int.Parse(text.Groups["x"].Value);
                var y = int.Parse(text.Groups["y"].Value);
                var j = int.Parse(text.Groups["j"].Value);
                Robots.Add(new Robot(i, j, x, y));
            }
        }
    }

    public static int Width { get; set; }

    public static int Height { get; set; }

    private Regex regex = new Regex(@"p\=(?'j'\S*)\,(?'i'\S*)\sv\=(?'x'\S*)\,(?'y'\S*)");


    public readonly List<Robot> Robots = new List<Robot>();

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }


    private readonly HashSet<(int, int)> _visitedGardenPlots = new HashSet<(int, int)>();

    public long Solve()
    {
        var positions = Robots.Select(robot => robot.CalculatePosition(100)).ToList();

        var middleX = Width / 2;
        var middleY = Height / 2;

        var quadrants = new Dictionary<int, long>
        {
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0}
        };

        WriteInput();
        foreach (var robot in Robots)
        {
            var newPosition = robot.CalculatePosition(100);

           

            if (newPosition.i < middleY)
            {
                if (newPosition.j < middleX)
                {
                    quadrants[1]++;
                }
                else if(newPosition.j > middleX)
                {
                    quadrants[2]++;
                }
            }
            else if(newPosition.i > middleY)
            {
                if (newPosition.j < middleX)
                {
                    quadrants[3]++;
                }
                else if(newPosition.j > middleX)
                {
                    quadrants[4]++;
                }
            }
        }

        void WriteInput()
        {
            for(int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if(i == middleY || j == middleX) Console.Write(" ");
                    else
                    {
                        var robotCount = positions.Count(x => x.i == i && x.j == j);
                        if(robotCount > 0) Console.Write(robotCount);
                        else  Console.Write(".");
                    }
                   
                }

                Console.WriteLine();
            }
        }

        return quadrants[1] * quadrants[2] * quadrants[3] * quadrants[4];
    }


    public long SolveB()
    {
        for (var i = 0; i < 10000; i++)
        {
            var positions = Robots.Select(robot => robot.CalculatePosition(i)).ToList();
            var posHash = positions.ToHashSet();
            if(positions.Count != posHash.Count) continue;

            Console.Write(i);

            WriteInput(positions);
        }

        return 0;

        void WriteInput(List<(int i, int j)> positions)
        {
            for(int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var robotCount = positions.Count(x => x.i == i && x.j == j);
                    Console.Write(robotCount > 0 ? "X" : ".");
                }

                Console.WriteLine();
            }
        }
    }

    public record Robot(int i, int j, int x, int y)
    {
        public (int i, int j) CalculatePosition(int seconds)
        {
            var newI = (i + y * seconds) % Height;
            if (newI < 0) newI = Height + newI;
            var newJ = (j + x * seconds) % Width;
            if (newJ < 0) newJ = Width + newJ;
            return (newI, newJ);
        }
    }
}