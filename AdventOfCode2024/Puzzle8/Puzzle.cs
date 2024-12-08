namespace AdventOfCode2024.Puzzle8
{
    internal class Puzzle
    {
        public Puzzle(string inputName)
        {
            Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
            Input = BuildInput();
        }
        private string[] Rows { get; set; } 
        private char[][] Input { get; set; } 
        private int rows => Input.Length;
        private int cols => Input[0].Length;
        private static string GetInputNameInFolder(string inputName)
        {
            return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
        }

        public long Solve()
        {
            var groupedNodes = FindAndGroupNodes();

            var antiNodeLocations = new HashSet<(int, int)?>();
            foreach (var groupedNode in groupedNodes)
            {
                foreach (var curr in groupedNode)
                {
                    foreach (var other in groupedNode.Except([curr]))
                    {
                        antiNodeLocations.Add(BuildAntiNode(curr, other));
                        antiNodeLocations.Add(BuildAntiNode(other, curr));
                    }
                }
            }

            return antiNodeLocations.Count(x => x != null);
            
        }

        private IEnumerable<IGrouping<char, Node>> FindAndGroupNodes()
        {
            var nodes = new List<Node>();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (Input[i][j] != '.')
                        nodes.Add(new Node(Input[i][j], j, i));
                }
            }

            return nodes.GroupBy(x=>x.freq);
        }


       

        private (int x, int y)? BuildAntiNode(Node curr, Node other)
        {
            var xDistance = Math.Abs(other.x - curr.x);
            var yDistance = Math.Abs(other.y - curr.y);
            var x = curr.x < other.x ? curr.x - xDistance : curr.x + xDistance;
            var y = curr.y < other.y ? curr.y - yDistance : curr.y + yDistance;
            return  IsValidCoordinate(x, y) ? (x, y) : null;
        }


        public long SolveB()
        {
            var groupedNodes = FindAndGroupNodes();
            var antiNodeLocations = new HashSet<(int, int)>();

            foreach (var groupedNode in groupedNodes)
            {
                foreach (var curr in groupedNode)
                {
                    foreach (var other in groupedNode.Except([curr]))
                    {
                        BuildAllNodes(antiNodeLocations, curr, other);
                    }
                }
            }
            Input.PrintGrid();

            return antiNodeLocations.Count;
        }

        private void BuildAllNodes(HashSet<(int, int)> antiNodeLocations, Node curr, Node other)
        {
            var xDistance = other.x - curr.x;
            var yDistance = other.y - curr.y;

            (int x, int y) nextAntiNode = (curr.x - xDistance, curr.y - yDistance);
            while (IsValidCoordinate(nextAntiNode.x, nextAntiNode.y))
            {
                antiNodeLocations.Add(nextAntiNode);
                Input[nextAntiNode.y][nextAntiNode.x] = '#';
                nextAntiNode = (nextAntiNode.x - xDistance, nextAntiNode.y - yDistance);
            }

            nextAntiNode =  (curr.x + xDistance, curr.y + yDistance);
            while (IsValidCoordinate(nextAntiNode.x, nextAntiNode.y))
            {
                antiNodeLocations.Add(nextAntiNode);
                Input[nextAntiNode.y][nextAntiNode.x] = '#';
                nextAntiNode = (nextAntiNode.x + xDistance, nextAntiNode.y + yDistance);
            }

        }

        private bool IsValidCoordinate(int x, int y) => x >= 0 && x < cols && y >= 0 && y < rows;

        private char[][] BuildInput()
        {
            var input = new char[Rows.Length][];
            for (int row = 0; row < Rows.Length; row++)
            {
                input[row] = Rows[row].ToCharArray();
            }

            return input;
        }
  

    }

    internal record struct Node(char freq, int x, int y);
}