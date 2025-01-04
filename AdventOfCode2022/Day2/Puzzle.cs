using System.ComponentModel;

namespace AdventOfCode2022.Day2
{
    internal class Puzzle(string [] rows)
    {
        private string[] Rows { get; set; } = rows;
        private const string Rock = "A";
        private const string Paper = "B";
        private const string Scissors = "C";

        public long Solve()
        {
            var games = Rows.Select(x => x.Split(" ")).Select(x => (opponent: x.First(), you: x.Last()));
            var score = 0L;
            foreach (var (opponent, you) in games)
            {
                score += CalculateRoundResult(opponent, you);
            }

            return score;
        }

    
        private static int CalculateRoundResult(string opponent, string you)
        {
            var shapeScore = you switch
            {
                "X" => 1,
                "Y" => 2,
                _ => 3
            };
            var roundResult = ScoreHand(opponent, you);

            return roundResult + shapeScore;
        }

        private static int ScoreHand(string opponent, string you)
        {
            return (opponent, you) switch
            {
                (Rock, "Y") or (Paper, "Z") or (Scissors, "X") => 6,
                (Rock, "X") or (Paper, "Y") or (Scissors, "Z") => 3,
                _ => 0
            };
        }

        public long SolveB()
        {
            var games = Rows.Select(x => x.Split(" ")).Select(x => (opponent: x.First(),  you: x.Last()));
            var score = 0L;
            foreach (var (opponent, you) in games)
            {
                score += FinishRound(opponent, you);
            }

            return score;
        }

        private long FinishRound(string opponent, string you)
        {
            var requireHand = you switch
            {
                "Y" => opponent,
                "X" => Lose(opponent),
                "Z" => Win(opponent),
                _ => throw new ArgumentOutOfRangeException(you)
            };

            var roundScore = you switch
            {
                "X" => 0,
                "Y" => 3,
                _ => 6
            };

            var shapeScore = requireHand switch
            {
                Rock => 1,
                Paper => 2,
                Scissors => 3,
                _ => throw new ArgumentOutOfRangeException(requireHand)
            };

            return roundScore + shapeScore;
        }

        private static string Lose(string opponent)
        {
            return opponent switch
            {
                Rock => Scissors,
                Paper => Rock,
                Scissors => Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(opponent))
            };
        }

        private static string Win(string opponent)
        {
            return opponent switch
            {
               Rock => Paper,
                Paper => Scissors,
                Scissors => Rock,
                _ => throw new ArgumentOutOfRangeException(nameof(opponent))
            };
        }
    }
}
