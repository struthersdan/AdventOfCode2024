using NUnit.Framework;

namespace AdventOfCode2024.Puzzle17
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", "4,6,3,5,6,3,5,2,1,0")]
        [TestCase("input.txt", "7,1,3,4,1,2,6,7,1")]
        [TestCase("input2.txt", "2,4,1,5,7,5,0,3,4,0,1,6,5,5,3,0")]
        public void PartA(string inputName, string answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        //[TestCase("sample2.txt", 117440)]
        [TestCase("input.txt", 109019476330651)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}