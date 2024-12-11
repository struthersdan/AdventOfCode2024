namespace AdventOfCode2024.Puzzle10;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
        Input = Rows.Select(x => x.Select(i=> int.Parse($"{i}")).ToArray()).ToArray();
    }

    public int[][] Input { get; set; }

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    

    public long Solve()
    {
        var sum = 0L;
        var visitedTops = new HashSet<(int, int)>();

       
        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                var curr = Input[i][j];
                if (curr == 0)
                {
                    sum += FindPathCount(curr, i+1, j, Direction.Down);
                    sum += FindPathCount(curr,i-1, j, Direction.Up);
                    sum += FindPathCount(curr,i, j-1, Direction.Left);
                    sum += FindPathCount(curr, i, j+1, Direction.Right);
                    visitedTops.Clear();
                }
            }

        }

        return sum;
        long FindPathCount(int previous, int i, int j, Direction direction)
        {
            Console.WriteLine($"{i} {j} {direction}");
            var pathSum = 0L;
            if (!Input.ContainsCoordinates(i, j)) return pathSum;
            var curr = Input[i][j];
            if (curr != previous + 1) return pathSum;
            Console.WriteLine($"{curr} {previous}");
            if (curr == 9 && visitedTops.Add((i, j))) return 1;
            if(direction != Direction.Up)
                pathSum +=  FindPathCount(curr, i + 1, j, Direction.Down);
            if(direction != Direction.Down)
                pathSum += FindPathCount(curr, i - 1, j, Direction.Up);
            if(direction != Direction.Right)
                pathSum += FindPathCount(curr, i, j - 1, Direction.Left);
            if(direction != Direction.Left)
                pathSum += FindPathCount(curr, i, j + 1, Direction.Right);
          
            return pathSum;
        }
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }


    public long SolveB()
    {
        var sum = 0L;
       
        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                var curr = Input[i][j];
                if (curr == 0)
                {
                    sum += FindPathCount(curr, i+1, j, Direction.Down);
                    sum += FindPathCount(curr,i-1, j, Direction.Up);
                    sum += FindPathCount(curr,i, j-1, Direction.Left);
                    sum += FindPathCount(curr, i, j+1, Direction.Right);
                }
            }

        }


        return sum;
        long FindPathCount(int previous, int i, int j, Direction direction)
        {
            var pathCount = 0L;
            if (!Input.ContainsCoordinates(i, j)) return pathCount;
            var curr = Input[i][j];
            if (curr != previous + 1) return pathCount;
            if (curr == 9) return 1;
            if(direction != Direction.Up)
                pathCount +=  FindPathCount(curr, i + 1, j, Direction.Down);
            if(direction != Direction.Down)
                pathCount += FindPathCount(curr, i - 1, j, Direction.Up);
            if(direction != Direction.Right)
                pathCount += FindPathCount(curr, i, j - 1, Direction.Left);
            if(direction != Direction.Left)
                pathCount += FindPathCount(curr, i, j + 1, Direction.Right);
          
            return pathCount;
        }
    }

}