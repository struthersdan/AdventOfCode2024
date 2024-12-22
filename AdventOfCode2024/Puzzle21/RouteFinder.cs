using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Puzzle21
{
    internal static class RouteFinder
    {
        public static string FindShortestRoute((int i, int j) start, (int i, int j) finish, char[][] buttons)
        {
            List<string> shortestRoutes = new List<string>();
            var maxLength = int.MaxValue;

            var priorityQueue = new PriorityQueue<State, int>();


            EnqueueNextSteps(start, finish, priorityQueue, "", [start], 0);


            while (priorityQueue.TryDequeue(out var state, out var distance))
            {
                var (vector, inputs, visited) = state;
                var (i, j, direction) = vector;
                var curr = (i, j);

                if (!visited.Add(curr)) continue;
                if (!buttons.SafeAccess(i, j, out var c)) continue;
                if (c == ' ') continue;

                distance++;
                inputs += direction;

                if (curr == finish && inputs.Length <= maxLength)
                {
                    shortestRoutes.Add(inputs);
                    maxLength = inputs.Length;
                    continue;
                }

                EnqueueNextSteps(curr, finish, priorityQueue, inputs, visited, distance);
            }

            if (shortestRoutes.Count == 1) return shortestRoutes.First();

            shortestRoutes = shortestRoutes.Where(x => x.Distinct().Count() == 1 || x.Length == 2 || x[0] != x[^1])
                .Where(
                    x =>
                    {
                        var changeCount = 0;
                        var curr = x[0];
                        foreach (var c in x)
                        {
                            if (c != curr) changeCount++;
                            curr = c;
                        }

                        return changeCount < 2;
                    }).ToList();
            if (shortestRoutes.Count == 1) return shortestRoutes.First();

            if (IsGoingRight(start, finish))
            {
                if (IsGoingDown(start, finish))
                {
                    if (start.j == 0 && finish.i == 3 && buttons.Length > 6)
                    {
                        return shortestRoutes.First(x => x.StartsWith('>') && x.EndsWith('v'));
                    }

                    return shortestRoutes.First(x => x.StartsWith('v') && x.EndsWith('>'));
                }
                else if (IsGoingUp(start, finish))
                {
                    if (start.j == 0 && finish.i == 0 && buttons.Length == 6)
                    {
                        return shortestRoutes.First(x => x.StartsWith('>') && x.EndsWith('^'));
                    }

                    return shortestRoutes.First(x => x.StartsWith('^') && x.EndsWith('>'));
                }
                else
                    return shortestRoutes.First();
            }

            if (IsGoingLeft(start, finish))
            {
                if (IsGoingDown(start, finish))
                {
                    if (start.i == 0 && finish.j == 0 && buttons.Length == 6)
                    {
                        return shortestRoutes.First(x => x.StartsWith('v') && x.EndsWith('<'));
                    }

                    return shortestRoutes.First(x => x.StartsWith('<') && x.EndsWith('v'));
                }
                else if (IsGoingUp(start, finish))
                {
                    if (start.i == 3 && finish.j == 0 && buttons.Length > 6)
                    {
                        return shortestRoutes.First(x => x.StartsWith('^') && x.EndsWith('<'));
                    }

                    return shortestRoutes.First(x => x.StartsWith('<') && x.EndsWith('^'));
                }
                else
                    return shortestRoutes.First();
            }

            return shortestRoutes.First();
        }

        private static bool IsGoingUp((int i, int j) start, (int i, int j) finish)
        {
            return finish.i < start.i;
        }

        private static bool IsGoingDown((int i, int j) start, (int i, int j) finish)
        {
            return finish.i > start.i;
        }

        private static bool IsGoingRight((int i, int j) start, (int i, int j) finish)
        {
            return finish.j > start.j;
        }

        private static bool IsGoingLeft((int i, int j) start, (int i, int j) finish)
        {
            return finish.j < start.j;
        }

        private static void EnqueueNextSteps((int i, int j) curr, (int i, int j) finish,
            PriorityQueue<State, int> priorityQueue, string inputs, HashSet<(int i, int j)> visited, int priority)
        {
            var (i, j) = curr;

            if (finish.i < i)
            {
                priorityQueue.Enqueue(new State(new Vector(i - 1, j, '^'), inputs, [..visited]), 2);
            }


            if (finish.i > i)
            {
                priorityQueue.Enqueue(new State(new Vector(i + 1, j, 'v'), inputs, [..visited]), 2);
            }


            if (finish.j < j)
            {
                priorityQueue.Enqueue(new State(new Vector(i, j - 1, '<'), inputs, [..visited]), 1);
            }

            if (finish.j > j)
            {
                priorityQueue.Enqueue(new State(new Vector(i, j + 1, '>'), inputs, [..visited]), 3);
            }
        }


        public record Vector(int i, int j, char direction);

        public record State(Vector v, string inputs, HashSet<(int i, int j)> visited);
    }
}