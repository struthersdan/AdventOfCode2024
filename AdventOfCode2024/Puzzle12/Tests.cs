namespace AdventOfCode2024.Puzzle12
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 1930)]
        [TestCase("input.txt", 1533644)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 1206)]
        [TestCase("input.txt", 936718)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}