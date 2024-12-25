using NUnit.Framework;

namespace AdventOfCode2024.Puzzle25
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 3)]
        [TestCase("input.txt", 3525)]
    
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


       
        [TestCase("sample.txt", 0)]
        [TestCase("input.txt", 0)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}