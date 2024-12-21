using NUnit.Framework;

namespace AdventOfCode2024.Puzzle21
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 44)]
        [TestCase("input.txt", 1409)]
    
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


       
        [TestCase("sample.txt", 44)]
        [TestCase("input.txt", 1409)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}