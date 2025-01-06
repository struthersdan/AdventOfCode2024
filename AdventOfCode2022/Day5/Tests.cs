using NUnit.Framework;

namespace AdventOfCode2022.Day5
{
    [TestFixture]
    internal class Tests
    {

        [TestCase("sample.txt", "CMZ")]
        [TestCase("input.txt", "CNSZFDVLJ")]
        public void PartA(string inputName, string answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", "MCD")]
        [TestCase("input.txt", "QNDWLMGNS")]
        public void PartB(string inputName, string answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}