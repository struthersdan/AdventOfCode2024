using NUnit.Framework;

namespace AdventOfCode2024.Puzzle5
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 18)]
        [TestCase("input.txt", 2543)]
        public void PartA(string inputName, int answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 9)]
        [TestCase("input.txt", 1930)]
        public void PartB(string inputName, int answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}