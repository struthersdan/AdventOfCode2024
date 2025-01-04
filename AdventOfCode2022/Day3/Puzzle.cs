namespace AdventOfCode2022.Day3
{
    internal class Puzzle(string [] rows)
    {
        private string[] Rows { get; set; } = rows;
        private const string Rock = "A";
        private const string Paper = "B";
        private const string Scissors = "C";

        public long Solve()
        {
            var priorities = 0L;
            foreach (var row in Rows)
            {
                var array = row.ToCharArray();
                var length = row.Length /2;

                var firstHalf = array.Take(length);
                var secondHalf = array.TakeLast(length);

                var dupe = firstHalf.Intersect(secondHalf).First();
           
                if (dupe - 'a' >= 0)
                {
                    priorities += dupe - 'a' + 1;
                }
                else
                {
                    priorities += dupe - 'A' + 27;
                }
                
               
            }

            return priorities;
        }

    
     
        public long SolveB()
        {  var badges = 0L;

            while (Rows.Length >=3)
            {
                var group = Rows.Take(3).Select(x=>x.ToCharArray()).ToArray();

                var dupe = group[0].Intersect(group[1]).Intersect(group[2]).First();
                
                if (dupe - 'a' >= 0)
                {
                    badges += dupe - 'a' + 1;
                }
                else
                {
                    badges += dupe - 'A' + 27;
                }
                Rows = Rows[3..];

            }

            return badges;
        }

    }
}
