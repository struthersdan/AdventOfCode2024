using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using NUnit.Framework.Constraints;

namespace AdventOfCode2024.Puzzle1
{
    internal class Puzzle(string inputName)
    {
        private string[] Rows { get; set; } = File.ReadAllLines($"{nameof(Puzzle1)}/{inputName}");

        public long Solve()
        {
            var columns = Rows.Select(x =>
            {
                var items = x.Split(" ");
                return items.Where(i => !string.IsNullOrEmpty(i)).ToArray();
            });
            var left = new List<long>();
            var right = new List<long>();
            foreach (var column in columns)
            {
                left.Add(long.Parse(column[0]));
                right.Add(long.Parse(column[1]));
            }

            left = left.OrderBy(x => x).ToList();
            right = right.OrderBy(x => x).ToList();


            long distance = 0;
            for (var i = 0; i < left.Count(); i++)
            {
                distance += Math.Abs(right[i] - left[i]);
            }

            return distance;
        }

        public long SolveB()
        {
            var columns = Rows.Select(x =>
            {
                var items = x.Split(" ");
                return items.Where(i => !string.IsNullOrEmpty(i)).ToArray();
            });
            var left = new List<long>();
            var right = new List<long>();
            foreach (var column in columns)
            {
                left.Add(long.Parse(column[0]));
                right.Add(long.Parse(column[1]));
            }

            var counts = new Dictionary<long, long>();
            foreach (long l in right)
            {
                var exists = counts.TryGetValue(l, out var existing);
                if (exists) counts[l]++;
                else
                {
                    counts[l]  = 1;
                }
            }

            long similarity = 0;
            foreach (var l in left)
            {
                counts.TryGetValue(l, out var score);

                similarity += l * score;
            }

            return similarity;
        }
    }
}
