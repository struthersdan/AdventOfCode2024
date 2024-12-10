namespace AdventOfCode2024.Puzzle1
{
    internal class Puzzle(string inputName)
    {
        private string[][] Columns { get; set; } = File.ReadAllLines($"{nameof(Puzzle1)}/{inputName}")
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToArray();

        public long Solve()
        {
            var left = Columns.Select(x => int.Parse(x[0])).OrderBy(x => x).ToList();
            var right = Columns.Select(x => int.Parse(x[1])).OrderBy(x => x).ToList();

            return  left.Select((t, i) => Math.Abs(right[i] - t)).Sum();
        }

        public long SolveB()
        {
            var left = Columns.Select(x => uint.Parse(x[0])).ToList();
            var right = Columns.Select(x => uint.Parse(x[1])).ToList();
           
            var counts = new Dictionary<uint, ushort>();
            foreach (var r in right)
            {
                if (!counts.TryAdd(r, 1)) counts[r]++;
            }

            uint similarity = 0;
            foreach (var l in left)
            {
                counts.TryGetValue(l, out var score);

                similarity += l * score;
            }

            return similarity;
        }
    }
}
