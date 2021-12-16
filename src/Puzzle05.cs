using System.Text.RegularExpressions;

namespace AdventOfCode2021;

public class Puzzle05 : AbstractPuzzle
{
    private readonly LineSegment[] _segments;
        
    public Puzzle05(string input) : base(input)
    {
        _segments = Input.Trim().Split('\n').Select(line => new LineSegment(line)).ToArray();
    }

    public override int Day()
    {
        return 5;
    }

    public override string SolvePart1()
    {
        return _segments
            .Where(segment => segment.IsVerticalOrHorizontal())
            .SelectMany(segment => segment.Points())
            .GroupBy(point => point)
            .ToDictionary(group => @group.Key, group => @group.Count())
            .Count(pair => pair.Value >= 2)
            .ToString();
    }

    public override string SolvePart2()
    {
        return _segments
            .SelectMany(segment => segment.Points())
            .GroupBy(point => point)
            .ToDictionary(group => @group.Key, group => @group.Count())
            .Count(pair => pair.Value >= 2)
            .ToString();
    }

    private class LineSegment
    {
        private readonly int x1;
        private readonly int y1;
        private readonly int x2;
        private readonly int y2;

        public LineSegment(string s)
        {
            var match = Regex.Match(s, @"(\d+),(\d+) -> (\d+),(\d+)");
            x1 = int.Parse(match.Groups[1].Value);
            y1 = int.Parse(match.Groups[2].Value);
            x2 = int.Parse(match.Groups[3].Value);
            y2 = int.Parse(match.Groups[4].Value);
        }

        public bool IsVerticalOrHorizontal()
        {
            return x1 == x2 || y1 == y2;
        }

        public IEnumerable<(int, int)> Points()
        {
            var dx = Math.Sign(x2 - x1);
            var dy = Math.Sign(y2 - y1);
            var x = x1;
            var y = y1;
            while (x != x2 + dx || y != y2 + dy)
            {
                yield return (x, y);
                x += dx;
                y += dy;
            }
        }
    }
}