namespace AdventOfCode2024.Puzzle24;

public class Gate(string leftInput, string rightInput, string output, Operator op)
{
    public string LeftInput { get; set; } = leftInput;
    public string RightInput { get; set; } = rightInput;
    public string Output { get; set; } = output;
    public Operator Op { get; set; } = op;
    public bool? Value { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}

public enum Operator
{
    OR,
    AND,
    XOR
};