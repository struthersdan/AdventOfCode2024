namespace AdventOfCode2024.Puzzle20
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 44, 2)]
        [TestCase("sample.txt", 1, 64)]
        [TestCase("input.txt", 1409, 100)]
    
        public void PartA(string inputName, long answer, int minSavings)
        {
            var result = new Puzzle(inputName).Solve(minSavings);
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


       
        [TestCase("sample.txt", 29, 72)]
        [TestCase("sample.txt", 3, 76)]
        [TestCase("input.txt", 1012821, 100)]
        public void PartB(string inputName, long answer, int minSavings)
        {
            var result = new Puzzle(inputName).SolveB(minSavings);
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}