using NUnit.Framework;

namespace AdventOfCode2024.Puzzle22
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 37327623)]
        [TestCase("input.txt", 13022553808)]
    
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


       
        [TestCase("sample2.txt", 23)]
        [TestCase("input.txt", 1555)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}