using NUnit.Framework;

namespace AdventOfCode2024.Puzzle6
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 41)]
       [TestCase("input.txt", 4663)]
        public void PartA(string inputName, int answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 6)]
        [TestCase("input.txt", 1530)]
        public void PartB(string inputName, int answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}