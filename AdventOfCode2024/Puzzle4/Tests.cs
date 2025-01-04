namespace AdventOfCode2024.Puzzle4
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 18)]

        [TestCase("a.txt", 2543)]
        public void PartA(string inputName, int answer)
        {
            var result = new Puzzle4.Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 9)] 
     [TestCase("a.txt", 1930)]
        public void PartB(string inputName, int answer)
        {
            var result = new Puzzle4.Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}