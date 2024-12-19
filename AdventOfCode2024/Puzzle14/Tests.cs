using NUnit.Framework;

namespace AdventOfCode2024.Puzzle14
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 12)]
        [TestCase("input.txt", 231019008)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


       // [TestCase("sample.txt", 1206)]
        [TestCase("input.txt", 0)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}