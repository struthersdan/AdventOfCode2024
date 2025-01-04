namespace AdventOfCode2022.Day1
{
    internal class Puzzle(string [] rows)
    {
        private string[] Rows { get; set; } = rows;
        public long Solve()
        {
            var backPacks = new List<long>();
            var backPack = 0L;
            foreach (var row in Rows)
            {
                if (string.IsNullOrEmpty(row))
                {
                    backPacks.Add(backPack);
                    backPack = 0;
                    continue;
                }

                backPack += long.Parse(row);
            }

            return backPacks.Max();
        }

        public long SolveB()
        {
            var backPacks = new List<long>();
            var backPack = 0L;
            foreach (var row in Rows)
            {
                if (string.IsNullOrEmpty(row))
                {
                    backPacks.Add(backPack);
                    backPack = 0;
                    continue;
                }

                backPack += long.Parse(row);
            }

            backPacks.Add(backPack);

            return backPacks.OrderByDescending(x=>x).Take(3).Sum();
        }
    }
}
