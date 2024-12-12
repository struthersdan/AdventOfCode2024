using System.ComponentModel;

namespace AdventOfCode2024.Puzzle9;

internal class Puzzle
{
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
        var id = 0;
        var input = Rows.FirstOrDefault()?.Select(ch => int.Parse($"{ch}")).ToArray() ?? [];
        var fileBlocks = new Stack<DiskFile>();
        var allBlocks = new List<DiskPart>();
        var checkSum = 0L;
        for (var i = 0; i < input.Length; i++)
        {
            var size = input[i];
            if (i % 2 == 0)
            {
                for (int j = 0; j < size; j++)
                {
                    var part = new DiskFile(id);
                    allBlocks.Add(part);
                }

                id++;
            }
            else
            {
                for (int j = 0; j < size; j++)
                {
                    var part = new DiskSpace();
                    allBlocks.Add(part);

                }
            }
        }

        var arr = allBlocks.ToArray();

        Console.WriteLine();
        var start = 0;
        var end = allBlocks.Count - 1;

        while (start < end)
        {
            var front = arr[start];
            var back = arr[end];
            while (back is not DiskFile)
            {
                end--;
                back = arr[end];
            }

            while (front is not DiskSpace)
            {
                start++;
                front = arr[start];
            }

            if (start >= end) break;


            arr[start] = back;
            arr[end] = front;

            end--;
            start++;
        }


        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] is not DiskFile file) continue;
            checkSum += i * file.id;
        }


        foreach (var allBlock in arr)
        {
            var ids = allBlock is DiskFile { } file ? $"{file.id}" : ".";
            Console.Write($"{ids}");
        }


        Console.WriteLine();
        return checkSum;

    }




    public long SolveB()
    {
        var checkSum = 0L;
        var input = Rows.FirstOrDefault()?.Select(ch => new FileChunk(int.Parse($"{ch}"))).ToArray() ?? [];

        var fileLocation = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var fileChunk = input[i];
            fileChunk.Id = i % 2 == 0 ? i / 2 : -1;
            fileChunk.Location = fileLocation;
            fileLocation += fileChunk.Size;
        }


        var start = 0;
        var end = input.Length - 1;


        while (start < input.Length)
        {
            var startPointer = input[start];
            var endPointer = input[end];

            if (startPointer.Size == 0 || end == 0)
            {
                start++;
            }

            if (startPointer.Id >= 0)
            {
                for (int i = 0; i < startPointer.Size; i++)
                {
                    checkSum += startPointer.Location * startPointer.Id;
                    startPointer.Location++;
                }

                start++;
            }

            if (end <= 0) continue;

            if (endPointer.Id >= 0)
            {
                var movingStart = start;
                while (movingStart < end && endPointer.Size > 0)
                {
                    var movingStartPointer = input[movingStart];
                    if (movingStartPointer.Id == -1 && movingStartPointer.Size >= endPointer.Size)
                    {
                        movingStartPointer.Size -= endPointer.Size;

                        for (int i = 0; i < endPointer.Size; i++)
                        {
                            checkSum += movingStartPointer.Location * endPointer.Id;
                            movingStartPointer.Location++;
                        }

                        endPointer.Size = 0;
                    }
                    else
                    {
                        movingStart++;
                    }
                }
            }

            end--;

        }

        return checkSum;
    }

}
    


internal class FileChunk(int size)
{
    public int Size { get; set; } = size;
    public bool IsStuck { get; set; }
    public int Location { get; set; }
    public int Id { get; set; }
}

internal record  DiskPart(Guid sizeIdentifier = default);

internal record DiskSpace(Guid sizeIdentifier= default) :DiskPart(sizeIdentifier);
internal record  DiskFile(int id, Guid sizeIdentifier= default): DiskPart(sizeIdentifier);