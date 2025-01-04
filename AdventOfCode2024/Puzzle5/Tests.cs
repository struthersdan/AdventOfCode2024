namespace AdventOfCode2024.Puzzle5
{
    [TestFixture]
    internal class Tests
    {
        //[TestCase("sample.txt", 143)]
        [TestCase("input.txt", 5129)]
        public void PartA(string inputName, int answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        //[TestCase("sample.txt", 123)]
       [TestCase("input.txt", 4077)]
        public void PartB(string inputName, int answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}