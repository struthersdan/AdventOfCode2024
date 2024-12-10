using System.ComponentModel;

namespace AdventOfCode2024.Puzzle9
{
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
            for (var i =0; i< input.Length; i++)
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
            var end = allBlocks.Count-1;

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

                if(start >= end) break;

                
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
            var id = 0;
            var input = Rows.FirstOrDefault()?.Select(ch => int.Parse($"{ch}")).ToArray() ?? [];
            var fileBlocks = new Stack<DiskFile>();
            var allBlocks = new List<DiskPart>();
            var checkSum = 0L;
            var remainingSizes = new Dictionary<Guid, int>();
            for (var i =0; i< input.Length; i++)
            {
                var sizeIdentifier = Guid.NewGuid();
                var size = input[i];
                remainingSizes.Add(sizeIdentifier, size);
                if (i % 2 == 0)
                {
                    for (int j = 0; j < size; j++)
                    {
                        var part = new DiskFile(id, sizeIdentifier);
                        allBlocks.Add(part);
                    }

                    id++;
                }
                else
                {
                    for (int j = 0; j < size; j++)
                    {
                        var part = new DiskSpace(sizeIdentifier);
                        allBlocks.Add(part);
                        
                    }
                }
            }

            var arr = allBlocks.ToArray();

            var start = 0;
            
            var searchEnd = allBlocks.Count-1;
            while (start < arr.Length)
            {
                var end = searchEnd;
                var front = arr[start];
                var back = arr[end];

                while (front is not DiskSpace)
                {
                    start++;
                    front = arr[start];
                }


                while (back is not DiskFile || remainingSizes[back.sizeIdentifier] > remainingSizes[front.sizeIdentifier])
                {
                    end--;
                    if (end <= 0) break;
                    back = arr[end];
                }

                if (remainingSizes[front.sizeIdentifier] >= remainingSizes[back.sizeIdentifier] && start <= end)
                {
                    
                    arr[start] = back;
                    arr[end] = front;
                    remainingSizes[front.sizeIdentifier]--;
                    remainingSizes[back.sizeIdentifier]--;
                }

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
  

    }

    internal record  DiskPart(Guid sizeIdentifier = default);

    internal record DiskSpace(Guid sizeIdentifier= default) :DiskPart(sizeIdentifier);
    internal record  DiskFile(int id, Guid sizeIdentifier= default): DiskPart(sizeIdentifier);
}