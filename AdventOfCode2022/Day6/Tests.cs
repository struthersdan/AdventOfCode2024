using NUnit.Framework;

namespace AdventOfCode2022.Day6
{
    [TestFixture]
    internal class Tests
    {

        [TestCase("sample.txt", 11)]
        [TestCase("input.txt", 1282)]
        public void PartA(string inputName, long answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 26)]
        [TestCase("input.txt", 3513)]
        public void PartB(string inputName, int answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}