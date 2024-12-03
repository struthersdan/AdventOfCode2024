using NUnit.Framework;

namespace AdventOfCode2024.Puzzle3
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 161)]

        [TestCase("a.txt", 183788984)]
        public void PartA(string inputName, int answer)
        {
            var result = new Puzzle3.Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 48)] 
        [TestCase("a.txt", 62098619)]
        public void PartB(string inputName, int answer)
        {
            var result = new Puzzle3.Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}