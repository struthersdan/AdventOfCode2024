using System.Collections;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using static AdventOfCode2024.HelperMethods;

namespace AdventOfCode2024.Puzzle17;

internal class Puzzle
{
    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
        _computer = new Computer();
        foreach (var row in Rows)
        {
            if (string.IsNullOrEmpty(row)) continue;
            var match = r.Match(row);


            if (match.Groups["label"].Value.Contains("A"))
            {
                _computer.A = long.Parse(match.Groups["value"].Value);
            }
            else if (match.Groups["label"].Value.Contains("B"))
            {
                _computer.B = long.Parse(match.Groups["value"].Value);
            }
            else if (match.Groups["label"].Value.Contains("C"))
            {
                _computer.C = long.Parse(match.Groups["value"].Value);
            }
            else
            {
                _computer.Instructions = match.Groups["value"].Value.Split(',').Select(ushort.Parse).ToArray();

            }
        }
    }

    private Regex r = new Regex(@"(?'label'[^\:]*)\:\s(?'value'.*)");
    private readonly Computer _computer;

    public class Computer
    {
        public long A { get; set; }
        public long B { get; set; }
        public long C  { get; set; }
        public ushort[] Instructions { get; set; } = [];

        public List<ushort> Output { get; set; } = new();
        public string OutputString => string.Join(',', Output);
        public string InstructionsString => string.Join(',', Instructions);

        public long GetComboOperand(long operand)
        {
            return operand switch
            {
                0 or 1 or 2 or 3 => operand,
                4 => A,
                5 => B,
                6 => C,
                _ => throw new ArgumentOutOfRangeException(nameof(operand))
            };
        }

        public void Operate()
        {
            ushort i = 0;
            while (i < Instructions.Length)
            {
                var incrementNaturally = true;
                var opcode = Instructions[i];
                var operand = Instructions[i + 1];

                switch (opcode)
                {
                    case 0:
                    {
                        var denominator = GetComboOperand(operand);
                        var result = (long) Math.Truncate(A / Math.Pow(2, denominator)); 
                        A = result; 
                        break;
                    }
                    case 1:
                        B ^= operand;
                        break;
                    case 2:
                        var number = GetComboOperand(operand);
                        B = number % 8;
                        break;
                    case 3:
                        if (A != 0)
                        {
                            i = operand;
                            incrementNaturally = false;
                        }
                        break;
                    case 4:
                        B ^= C;  
                        break;
                    case 5:
                        Output.Add((ushort)(GetComboOperand(operand) % 8));
                        break;
                    case 6:
                    {
                        var denominator = GetComboOperand(operand);
                        var result = (long) Math.Truncate(A / Math.Pow(2, denominator)); 
                        B = result; 
                        break;
                    }
                    case 7:
                    {
                        var denominator = GetComboOperand(operand);
                        var result = (long) Math.Truncate(A / Math.Pow(2, denominator));
                        C = result; 
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (incrementNaturally) i += 2;
            }
            
            
        }

        public void Reset(long start)
        {
            A = start;
            B = 0;
            C = 0;
            Output.Clear();
        }
    }

    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public string Solve()
    {
        _computer.Operate();

        return string.Join(',',_computer.Output);
    }







    public long SolveB()
    {
        var stack = new Stack<State>();
        stack.Push(new State(24, _computer.Instructions.Length - 2));

        var totals = new List<long>();
        while (stack.Any())
        {
            var curr = stack.Pop();
            
            for (var j = 0; j < 8; j++)
            {
                if (CalculateNext(curr, j))
                {
                    if (curr.Index == 0)
                    {
                        var total = curr.Total + j;
                        Console.WriteLine(total);
                        totals.Add(total);
                    }
                    else
                    {
                        stack.Push(new State((curr.Total + j ) * 8, curr.Index - 1));
                    }
                       
                }
            }
                
            
        }

        Console.WriteLine();
        Console.WriteLine(totals.Min());

        return totals.Min();


        bool CalculateNext(State state, int j)
        {
            var expected = _computer.Instructions[state.Index];
            var a = state.Total + j;
            var b = a % 8;
            b ^= 5;
            var c = (long) Math.Truncate(a / Math.Pow(2, b));
            b ^= c;
            b ^= 6;
            b %= 8;
            return b == expected;
        }
    }

    private record State(long Total, int Index);
}