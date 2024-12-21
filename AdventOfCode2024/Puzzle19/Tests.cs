using NUnit.Framework;

namespace AdventOfCode2024.Puzzle19
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 6)]
        [TestCase("input.txt", 272)]
    
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 16)]
        [TestCase("input.txt", 1041529704688380)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}