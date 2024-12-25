using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzle23;

internal class Puzzle
{
    private string[] Rows { get; set; }


    public Puzzle(string inputName)
    {
        Rows = File.ReadAllLines($"{GetInputNameInFolder(inputName)}");

        HashSet<(string first, string second)> _connections = new HashSet<(string, string)>();
        foreach (var row in Rows)
        {
            var match = r.Match(row);

            _connections.Add((match.Groups["first"].Value, match.Groups["second"].Value));
        }

        foreach (var (first, second) in _connections)
        {
            if (_graph.TryGetValue(first, out var node))
            {
                node.Add(second);
            }
            else
            {
                _graph[first] = [second];
            }

            if (_graph.TryGetValue(second, out var node1))
            {
                node1.Add(first);
            }
            else
            {
                _graph[second] = [first];
            }
        }
    }

    private readonly Dictionary<string, List<string>> _graph = new Dictionary<string, List<string>>();

    private readonly Regex r = new Regex(@"(?'first'[a-z]*)\-(?'second'[a-z]*)");

    private static string GetInputNameInFolder(string inputName)
    {
        return $"{typeof(Puzzle).Namespace?.Split(".")[1]}/{inputName}";
    }


    public long Solve()
    {
        var triangles = new HashSet<(string, string, string)>();

        foreach (var u in _graph.Keys)
        {
            foreach (var v in _graph[u].Where(x=>string.CompareOrdinal(u, x) > 0))
            {
                foreach (var w in _graph[v].Where(x=> string.CompareOrdinal(v, x) > 0))
                {
                    if (!_graph.ContainsKey(w) || !_graph[w].Contains(u)) continue;
                    if (!u.StartsWith('t') && !v.StartsWith('t') && !w.StartsWith('t')) continue;
                    triangles.Add((u, v, w));
                }
            }

        }

        return triangles.Count;
    }


    public string SolveB()
    {
        var largestClique = FindLargestClique(_graph).OrderBy(x=>x);
        return string.Join(",", largestClique).TrimEnd(',');
    }

    List<string> FindLargestClique(Dictionary<string, List<string>> graph)
    {
        return BronKerbosch([], [..graph.Keys], [], graph);
    }

    List<string> BronKerbosch(
        List<string> currentClique, 
        List<string> candidateVertices, 
        List<string> excludedVertices, 
        Dictionary<string, List<string>> graph)
    {
        if (candidateVertices.Count == 0 && excludedVertices.Count == 0)
            return [..currentClique];

        List<string> largestClique = [];
        List<string> candidatesCopy = [..candidateVertices];

        foreach (var vertex in candidatesCopy)
        {
            var newCandidates = candidateVertices.Intersect(graph[vertex]).ToList();
            var newExcluded = excludedVertices.Intersect(graph[vertex]).ToList();
            var cliqueFromRecursion = BronKerbosch([..currentClique, vertex], newCandidates, newExcluded, graph);

            if (cliqueFromRecursion.Count > largestClique.Count)
            {
                largestClique = cliqueFromRecursion;
            }

            candidateVertices.Remove(vertex);
            excludedVertices.Add(vertex);
        }

        return largestClique;
    }
}