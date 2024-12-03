using System.Diagnostics.CodeAnalysis;

public class Report(string[] report)
{
    private readonly int[] _numbers = report.Select(int.Parse).ToArray();
    public bool ReportIsSafe  { get; set; }

    public List<int[]> CheckReport()
    {
        var list = new List<int[]>();
        for (int i = 0; i < _numbers.Length; i++)
        {
            var report = new List<int>(_numbers);
            report.RemoveAt(i);
            list.Add(report.ToArray());
        }

        foreach (var report in list)
        {
            if (ReportIsSafe) break;
            var isSafe = true;
            Direction? direction = null;
            for (var i = 1; i < report.Length; i++)
            {
               
                var curr = report[i];
                var previous = report[i - 1];
                direction ??= curr > previous ? Direction.Increasing : Direction.Decreasing;
                var difference = curr - previous;
                if (CheckDifference(direction, difference))
                {
                    Console.WriteLine($"{string.Join("-", report)}: unsafe");
                    isSafe = false;
                    break;
                }
            }

            if (isSafe)
            {
                Console.WriteLine("safe");
                ReportIsSafe = true;
            }
        }

        return list;

        static bool CheckDifference([DisallowNull] Direction? direction, int difference)
        {
            return direction == Direction.Increasing && difference is > 3 or < 1 ||
                   direction == Direction.Decreasing && difference is < -3 or > -1;
        }
    }
}