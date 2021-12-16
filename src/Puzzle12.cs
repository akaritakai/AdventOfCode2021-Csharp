namespace AdventOfCode2021;

public class Puzzle12 : AbstractPuzzle
{
    private readonly IDictionary<string, ISet<string>> _edges = new Dictionary<string, ISet<string>>();
        
    public Puzzle12(string input) : base(input)
    {
        foreach (var line in Input.Trim().Split('\n'))
        {
            var parts = line.Split('-');
            var from = parts[0];
            var to = parts[1];
            if (!_edges.ContainsKey(from))
            {
                _edges[from] = new HashSet<string>();
            }
            _edges[from].Add(to);
            if (!_edges.ContainsKey(to))
            {
                _edges[to] = new HashSet<string>();
            }
            _edges[to].Add(from);
        }
    }

    public override int Day()
    {
        return 12;
    }

    public override string SolvePart1()
    {
        return CountPaths("start", new LinkedList<string>(), true).ToString();
    }

    public override string SolvePart2()
    {
        return CountPaths("start", new LinkedList<string>(), false).ToString();
    }

    private int CountPaths(string cave, LinkedList<string> path, bool seenTwice)
    {
        if (cave == "end")
        {
            return 1;
        }
        if (IsSmallCave(cave) && path.Contains(cave))
        {
            if (seenTwice || cave == "start")
            {
                return 0;
            }

            seenTwice = true;
        }

        path.AddLast(cave);
        var count = _edges[cave].Sum(next => CountPaths(next, path, seenTwice));
        path.RemoveLast();
        return count;
    }

    private static bool IsSmallCave(string cave)
    {
        return cave[0] >= 'a';
    }
}