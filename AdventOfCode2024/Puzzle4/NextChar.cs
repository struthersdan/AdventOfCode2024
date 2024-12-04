namespace AdventOfCode2024.Puzzle4;

public record NextChar
{
    public NextChar(char current, int I, int J, Direction Direction)
    {
        Current = current;
        this.I = I + Direction.vertical;
        this.J = J + Direction.horizontal;
        this.Direction = Direction;
    }

    public char Current { get; init; }
    public int I { get; init; }
    public int J { get; init; }
    public Direction Direction { get; init; }

    public char GetNext()
    {
        switch (Current)
        {
            case 'M':
                return 'A';
            case 'A':
                return 'S';
            default:
                return ' ';
        }
    }
}