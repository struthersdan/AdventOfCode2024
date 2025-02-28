﻿namespace AdventOfCode2024.Puzzle21
{
    [TestFixture]
    internal class Tests
    {
        [TestCase("sample.txt", 126384)]
        [TestCase("input.txt", 197560)]
    
        public void PartA(string inputName, long answer)
        {
            var result = new Puzzle(inputName).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }


       
        [TestCase("sample.txt", 154115708116294)]
        [TestCase("input.txt", 242337182910752)]
        public void PartB(string inputName, long answer)
        {
            var result = new Puzzle(inputName).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}