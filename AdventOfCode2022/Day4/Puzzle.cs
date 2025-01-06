using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day4
{
    internal class Puzzle(string [] rows)
    {
        private string[] Rows { get; set; } = rows;

        private static readonly Regex Regex = new(@"(?'first'\d*\-\d*),(?'second'\d*\-\d*)");

        public long Solve()
        {
            var overlaps = 0L;
            foreach (var row in Rows)
            {
                var match = Regex.Match(row);

                var first = match.Groups["first"].Value.Split("-").Select(int.Parse).ToArray();
                var second =  match.Groups["second"].Value.Split("-").Select(int.Parse).ToArray();

                if (first[0] <= second[0] && first[1] >= second[1] || second[0] <= first[0] && second[1] >= first[1])
                {
                    overlaps++;
                }


            }

            return overlaps;
        }

    
     
        public long SolveB()
        {  var badges = 0L;

            var overlaps = 0L;
            foreach (var row in Rows)
            {
                var match = Regex.Match(row);

                var first = match.Groups["first"].Value.Split("-").Select(int.Parse).ToArray();
                var second =  match.Groups["second"].Value.Split("-").Select(int.Parse).ToArray();

                if (first[0] <= second[1] && first[0] >= second[0] ||
                    second[0] >= first[0] && second[0] <= first[1])
                {
                    overlaps++;
                }


            }

            return overlaps;
        }

    }
}
