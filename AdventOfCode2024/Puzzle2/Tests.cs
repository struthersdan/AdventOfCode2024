namespace AdventOfCode2024.Puzzle2
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 2)]

        [TestCase("a.txt", 341)]
        public void PartA(string inputName, int answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 4)]

        [TestCase("a.txt", 404)]
        public void PartB(string inputName, int answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}