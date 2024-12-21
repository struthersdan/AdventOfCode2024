namespace AdventOfCode2024.Puzzle21;

internal class Puzzle
{
    private readonly char[][] _raceTrack;

    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");
    }



    private string[] Rows { get; set; }

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }



    public long Solve()
    {
        
        return 0L;
    }

   



 

    public long SolveB()
    {
        return 0;
    }

   


   
}