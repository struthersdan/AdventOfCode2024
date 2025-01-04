namespace AdventOfCode2024.Puzzle23
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 7)]
        [TestCase("input.txt", 1173)]
    
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


       
        [TestCase("sample.txt", "co,de,ka,ta")]
        [TestCase("input.txt", "cm,de,ez,gv,hg,iy,or,pw,qu,rs,sn,uc,wq")]
        public void PartB(string inputName, string answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}