using NUnit.Framework;

namespace AdventOfCode2024.Puzzle16
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 11048)]
        [TestCase("input.txt", 111480)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 64)]
        [TestCase("input.txt", 529)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}