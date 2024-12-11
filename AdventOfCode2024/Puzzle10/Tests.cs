using NUnit.Framework;

namespace AdventOfCode2024.Puzzle10
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 36)]
        [TestCase("input.txt", 667)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 81)]
        [TestCase("input.txt", 1344)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}