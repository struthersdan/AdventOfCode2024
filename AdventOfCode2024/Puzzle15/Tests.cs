namespace AdventOfCode2024.Puzzle15
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 2028)]
        [TestCase("sample2.txt", 10092)]
        [TestCase("input.txt", 1487337)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample2.txt", 9021)]
        [TestCase("input.txt", 1521952)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}