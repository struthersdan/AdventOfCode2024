using NUnit.Framework;

namespace AdventOfCode2024.Puzzle7
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 3749)]
        [TestCase("input.txt", 945512582195)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 11387)]
        [TestCase("input.txt", 271691107779347)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}