using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using AdventOfCode2024.Puzzle2;

namespace AdventOfCode2024.Puzzle2
{
    internal class Puzzle(string inputName)
    {
        private string[] Rows { get; set; } = File.ReadAllLines($"{nameof(Puzzle2)}/{inputName}");

        public long Solve()
        {
            var reports = Rows.Select(x =>
            {
                var items = x.Split(" ");
                return items.Where(i => !string.IsNullOrEmpty(i)).ToArray();
            });

            var safeCount = 0;

            foreach (var report in reports)
            {
                var isSafe = true;
                Direction? direction = null;
                for (var i = 1; i < report.Length; i++)
                {
                    var curr = int.Parse(report[i]);
                    var previous = int.Parse(report[i - 1]);
                    direction ??= curr > previous ? Direction.Increasing : Direction.Decreasing;
                    Console.WriteLine($"{previous}-{curr}-{direction}");

                    var difference = curr - previous;
                    if (CheckDifference(direction, difference))
                    {
                        Console.WriteLine("unsafe");
                        isSafe = false;
                        break;
                    }
                }

                if (isSafe)
                {
                    Console.WriteLine("safe");
                    safeCount++;
                }
            }

            return safeCount;
        }

        private static bool CheckDifference([DisallowNull] Direction? direction, int difference)
        {
            return direction == Direction.Increasing && difference is > 3 or < 1 ||
                   direction == Direction.Decreasing && difference is < -3 or > -1;
        }

        

        public long SolveB()
        {
            var reports = Rows.Select(x =>
            {
                var items = x.Split(" ");
                var numbers =  items.Where(i => !string.IsNullOrEmpty(i)).ToArray();
                return new Report(numbers);
            }).ToList();

            foreach (var report in reports)
            {
                report.CheckReport();
            }

            return reports.Count(x => x.ReportIsSafe);
        }

     
    }
}