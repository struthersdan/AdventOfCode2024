using NUnit.Framework;

namespace AdventOfCode2024.Puzzle24
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 2024)]
        [TestCase("sample2.txt", 4)]
        [TestCase("input.txt", 52956035802096)]
    
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


        
        [TestCase("input.txt", "hnv,hth,kfm,tqr,vmv,z07,z20,z28")]
        public void PartB(string inputName, string answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}