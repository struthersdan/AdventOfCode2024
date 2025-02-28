﻿namespace Utilities;

public static class HelperMethods
{
    public static char[][] Transpose(this char[][] original)
    {
        var transposed = new List<char[]>();

        for (int i = 0; i < original[0].Length; i++)
        {
            var transpose = new List<char>();
            foreach (var originalArray in original)
            {
                transpose.Add(originalArray[i]);
            }

            transposed.Add(transpose.ToArray());
        }

        return transposed.ToArray();
    }

    public static bool ContainsCoordinates<T>(this T[][] input, int i, int j)
    {
        return i >= 0 && i < input.Length && j >= 0 && j < input[i].Length;
    }

    public static bool SafeAccess(this char[][] input, int i, int j, out char c)
    {
        c = default;
        if (!input.ContainsCoordinates(i, j)) return false;
        c = input[i][j];
        return true;

    }

    public static void PrintGrid(this char[][] input)
    {
        foreach (var t in input)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                Console.Write(t[j]);
            }

            Console.WriteLine();
        }
    }

    public enum GridDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public static GridDirection RotateClockWise(GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => GridDirection.Right,
            GridDirection.Down => GridDirection.Left,
            GridDirection.Left => GridDirection.Up,
            GridDirection.Right => GridDirection.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static GridDirection RotateCounter(GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => GridDirection.Left,
            GridDirection.Down => GridDirection.Right,
            GridDirection.Left => GridDirection.Down,
            GridDirection.Right => GridDirection.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static void WriteInput(this char[][] input)
    {
        foreach (var t in input)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                Console.Write(t[j]);
            }

            Console.WriteLine();
        }
    }

    public static (int i, int j) FindStart(char[][] maze, char c)
    {
        (int i, int j) start = (0, 0);

        for (int i = 0; i < maze.Length; i++)
        {
            for (int j = 0; j < maze[i].Length; j++)
            {
                if (maze[i][j] == c) return (i, j);
            }
        }

        return start;
    }



    public static int ManhattanDistance((int x, int y) point1, (int x, int y) point2)
    {
        return Math.Abs(point2.x - point1.x) + Math.Abs(point2.y - point1.y);
    }
}