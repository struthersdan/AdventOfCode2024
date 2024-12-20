namespace AdventOfCode2024.Puzzle17;

public class Computer
{
    public long A { get; set; }
    public long B { get; set; }
    public long C { get; set; }
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
                    Output.Add((ushort) (GetComboOperand(operand) % 8));
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