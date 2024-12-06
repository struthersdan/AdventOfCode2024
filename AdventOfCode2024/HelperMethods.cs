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

        public static bool ContainsCoordinates(this char[][] input, int i, int j)
        {
            return i >= 0 && i < input.Length && j >= 0 && j < input[i].Length;
        }

        public static bool IsValidCoordinates(int rows, int cols, int x, int y) =>
            x >= 0 && x < rows && y >= 0 && y < cols;
    }
}