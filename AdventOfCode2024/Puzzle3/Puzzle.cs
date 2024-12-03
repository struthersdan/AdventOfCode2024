using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle3
{
    internal class Puzzle(string inputName)
    {
        private readonly string _input = string.Join("", File.ReadAllLines($"{nameof(Puzzle3)}/{inputName}").SelectMany(x => x));

        public long Solve()
        {
            var total = 0L;

            Regex r = new(@"mul\((?'left'\d{1,3}),(?'right'\d{1,3})\)");
            var m = r.Match(_input);
            while (m.Success)
            {
                total += int.Parse(m.Groups["left"].ToString()) * int.Parse(m.Groups["right"].ToString());
                m = m.NextMatch();
            }

            return total;
        }
        

        public long SolveB()
        {
            var total = 0L;
            var mulActive = true;
            
            Regex r = new(@"mul\((?'left'\d{1,3}),(?'right'\d{1,3})\)|do\(\)|don\'t\(\)");

            var m = r.Match(_input);
            while (m.Success)
            {
                switch (m.Value)
                {
                    case "don't()":
                        mulActive = false;
                        break;
                    case "do()" :
                        mulActive = true;
                        break;
                    default:
                    {
                        if(mulActive)
                        {
                            total += int.Parse(m.Groups["left"].ToString()) * int.Parse(m.Groups["right"].ToString());
                        }

                        break;
                    }
                }


                m = m.NextMatch();
            }

            return total;
        }

    
    }
}