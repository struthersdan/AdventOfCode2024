using NUnit.Framework;

namespace AdventOfCode2024.Puzzle18
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 7, 7, 12, 22)]
        [TestCase("input.txt", 71, 71, 1024, 276)]
        public void PartA(string inputName,int height, int width, int count, long answer)
        {
            var result = new Puzzle(inputName, height, width, count).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 7, 7, 12, 6, 1)]
        [TestCase("input.txt", 71, 71, 1024, 60, 37)]
        public void PartB(string inputName,int height, int width, int count,int answerx, int answery)
        {
            var result = new Puzzle(inputName, height, width, count).SolveB();
            Assert.That(result, Is.EqualTo((answery, answerx)));
            Console.WriteLine(result);
        }
    }
}