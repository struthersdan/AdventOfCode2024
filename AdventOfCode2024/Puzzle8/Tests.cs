namespace AdventOfCode2024.Puzzle8
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 14)] 
        [TestCase("input.txt", 329)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 34)]
       [TestCase("input.txt", 1190)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}