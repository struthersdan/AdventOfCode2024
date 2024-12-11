using NUnit.Framework;

namespace AdventOfCode2024.Puzzle11
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 55312)]
        [TestCase("input.txt", 203228)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 65601038650482)]
        [TestCase("input.txt", 240884656550923)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}