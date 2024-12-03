using NUnit.Framework;

namespace AdventOfCode2024.Puzzle1
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 11)]
        
        [TestCase("a.txt", 1646452)]
        public void PartA(string inputName, int answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 31)]
        
        [TestCase("a.txt", 23609874)]
        public void PartB(string inputName, int answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}