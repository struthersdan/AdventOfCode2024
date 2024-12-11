namespace AdventOfCode2024
{
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
    }
}