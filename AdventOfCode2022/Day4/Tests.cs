﻿using NUnit.Framework;

namespace AdventOfCode2022.Day4
{
    [TestFixture]
    internal class Tests
    {

        [TestCase("sample.txt", 2)]
        [TestCase("input.txt", 576)]
        public void PartA(string inputName, long answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).Solve();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }

        [TestCase("sample.txt", 4)]
        [TestCase("input.txt", 905)]
        public void PartB(string inputName, int answer)
        {
            var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
            var result = new Puzzle(rows).SolveB();
            Assert.That(result, Is.EqualTo(answer));
            Console.WriteLine(result);
        }
    }
}