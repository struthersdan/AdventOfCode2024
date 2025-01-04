// See https://aka.ms/new-console-template for more information

using AdventOfCode2022.Day1;

Console.WriteLine("Hello, World!");

var inputName = "input.txt";
var rows = File.ReadAllLines($"{typeof(Tests).Namespace?.Split(".")[1]}/{inputName}");
new Puzzle(rows).SolveB();
