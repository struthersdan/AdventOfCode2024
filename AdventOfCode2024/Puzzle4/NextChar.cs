namespace AdventOfCode2024.Puzzle4;

public record NextChar
{
    public NextChar(char current, int I, int J, Direction Direction)
    {
        Current = current;
        i = I + Direction.vertical;
        j = J + Direction.horizontal;
        direction = Direction;
    }

    public char Current { get; init; }
    public int i { get; init; }
    public int j { get; init; }
    public Direction direction { get; init; }

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

    public void Deconstruct(out char next, out int i, out int j, out Direction direction)
    {
        next = this.Current;
        i = this.i;
        j = this.j;
        direction = this.direction;
    }
}