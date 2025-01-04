using NUnit.Framework;

namespace AdventOfCode2022.Day2
{
    [TestFixture]
    internal class Tests
    {

        [TestCase("sample.txt", 15)]
        [TestCase("input.txt", 13565)]
        public void PartA(string inputName, long answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 12)]
        [TestCase("input.txt", 12424)]
        public void PartB(string inputName, long answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}