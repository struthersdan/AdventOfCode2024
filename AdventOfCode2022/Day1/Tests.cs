using NUnit.Framework;

namespace AdventOfCode2022.Day1
{
    [TestFixture]
    internal class Tests
    {

        [TestCase("sample.txt", 24000)]
        [TestCase("input.txt", 69912)]
        public void PartA(string inputName, long answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 45000)]
        [TestCase("input.txt", 208180)]
        public void PartB(string inputName, long answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}