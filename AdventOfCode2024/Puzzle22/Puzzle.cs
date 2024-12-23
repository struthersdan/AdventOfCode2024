using System.ComponentModel.Design;
using AdventOfCode2024.Puzzle21;

namespace AdventOfCode2024.Puzzle22
{
    internal class Puzzle
    {
        private readonly List<long> _startingNumbers;
        private string[] Rows { get; set; }


        public Puzzle(string inputName)
        {
            Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
            var startingNumbers = Rows.Select(long.Parse).ToList();
            _startingNumbers = startingNumbers;
        }


        private static string GetInputNameInFolder(string inputName)
        {
            return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
        }


        public long Solve()
        {
            var total = 0L;

            foreach (var startingNumber in _startingNumbers)
            {

                var number = startingNumber;
                for (int i = 0; i < 2000; i++)
                {
                    number = DailyTransform(number);
                }

                total += number;
            }


            return total;
        }


        private long DailyTransform(long start)
        {
            start = Mix(start, start * 64);
            start = Prune(start);
            start = Mix(start, TruncateDivide(start));
            start = Prune(start);
            start = Mix(start, start * 2048);
            start = Prune(start);
            return start;
        }

        private long TruncateDivide(long start)
        {
            return (long) Math.Truncate((decimal) start / 32);
        }

        private long Mix(long start, long input)
        {
            return start ^ input;
        }

        public long Prune(long number)
        {
            return number % 16777216;
        }

        public long SolveB()
        {
            var monkeySequences = new Dictionary<string, long>();

            foreach (var startingNumber in _startingNumbers)
            {
                var sequences = new HashSet<string>();
                var first = long.MaxValue;
                var second = long.MaxValue;
                var third = long.MaxValue;
                var fourth = startingNumber % 10;
                var number = startingNumber;
                for (int i = 0; i < 2000; i++)
                {
                    number = DailyTransform(number);

                    var lastDigit = number % 10;

                    if (first != long.MaxValue)
                    {
                        var sequence = $"{second - first},{third - second},{fourth - third},{lastDigit - fourth}";
                        if (sequences.Add(sequence))
                        {
                            if (!monkeySequences.TryGetValue(sequence, out var existingCount))
                            {
                                monkeySequences[sequence] = lastDigit;
                            }
                            else
                            {
                                monkeySequences[sequence] = existingCount + lastDigit;
                            }
                        }
                    }

                    first = second;
                    second = third;
                    third = fourth;
                    fourth = lastDigit;

                }
            }

            return monkeySequences.Select(x=>x.Value).Max();

        }
    }

}