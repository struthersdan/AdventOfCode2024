using System.Text.RegularExpressions;
using AdventOfCode2024.Puzzle24;

namespace AdventOfCode2024.Puzzle25;

internal class Puzzle
{
    private string[] Rows { get; set; }

    private List<int[]> locks = new();
    private List<int[]> keys = new();

    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

        var i = 0;

        while (i < Rows.Length)
        {
            char[][] schematic = new char[7][];
            var index = 0;
            while ( i< Rows.Length && !string.IsNullOrEmpty(Rows[i]))
            {
                schematic[index] = Rows[i].ToCharArray();
                i++;
                index++;
            }

            var isLock = schematic[0].Count(x => x == '#') == 5;
            var flippedSchematic = schematic.Transpose();

            var numbSchematic = new int[5];
            for (var j= 0; j < flippedSchematic.Length; j++)
            {
                numbSchematic[j] = flippedSchematic[j].Count(x => x == '#') - 1;
            }

            if (isLock)
            {
                locks.Add(numbSchematic);
            }
            else
            {
                keys.Add(numbSchematic);
            }

            i++;
        }
    }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }

    public long Solve()
    {
        var count = 0L;
        foreach (var @lock in locks)
        {
            foreach (var key in keys)
            {
                var overlap = false;
                for (var i = 0; i < 5; i++)
                {
                    if (@lock[i] + key[i] > 5)
                    {
                        overlap = true;
                        break;
                    };
                }

                if (!overlap)
                {
                    count++;
                }
            }
        }

        return count;
    }


    public long SolveB()
    {
        return 0L;
    }
}

