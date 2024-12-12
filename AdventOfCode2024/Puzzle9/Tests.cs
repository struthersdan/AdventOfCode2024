using NUnit.Framework;

namespace AdventOfCode2024.Puzzle9
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample2.txt", 60)]
        [TestCase("sample.txt", 1928)]
        [TestCase("input.txt", 6331212425418)]
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        [TestCase("sample.txt", 2858)]
        [TestCase("input.txt", 6363268339304)]
        //[Ignore("slow")]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}