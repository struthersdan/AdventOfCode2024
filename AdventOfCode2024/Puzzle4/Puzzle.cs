using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle4
{
    internal class Puzzle(string inputName)
    {
        private string[] Rows { get; set; } = File.ReadAllLines($"{nameof(Puzzle4)}/{inputName}");

        public long Solve()
        {
            var total = 0L;
            var input = string.Join("",Rows.SelectMany(x => x));

      

            return total;
        }
        

        public long SolveB()
        {
            var total = 0L;
            var input = string.Join("",Rows.SelectMany(x => x));


            return total;
        }

     
    }
}