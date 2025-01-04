namespace AdventOfCode2024.Puzzle13
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 480)]
        [TestCase("input.txt", 33481)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 875318608908)]
        [TestCase("input.txt", 92572057880885)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}