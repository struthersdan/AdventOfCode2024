﻿namespace AdventOfCode2024.Puzzle4;

public record Cross(int I, int J)
{
    public bool  IsValid(char[][] input)
    {
        var ul = UpLeft(input);
        var ur = UpRight(input);
        var dl = DownLeft(input);
        var dr = DownRight(input);
        var result = ul != null && ur != null && dl != null && dr != null && (ul == dl && ur == dr && ul != ur || ul == ur && dl == dr && ul != dl);
        return result;
    }

    private char? UpLeft(char[][] input) => SafeAccess(input, I - 1, J - 1);
    private char? UpRight(char[][] input) => SafeAccess(input, I - 1, J + 1);
    private char? DownLeft(char[][] input) => SafeAccess(input, I + 1, J - 1);
    private char? DownRight(char[][] input) => SafeAccess(input, I + 1, J + 1);


    private static char? SafeAccess(char[][] input, int i, int j)
    {
        if (!input.ContainsCoordinates(i, j)) return null;
        var c = input[i][j];
        return c != 'M' && c != 'S' ? null : c;
    }

}