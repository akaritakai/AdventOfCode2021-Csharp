using System.Drawing;

namespace AdventOfCode2021;

public class Puzzle09 : AbstractPuzzle
{
    private readonly int[,] _grid;
    private readonly int _height;
    private readonly int _width;
        
    public Puzzle09(string input) : base(input)
    {
        var lines = Input.Trim().Split('\n');
        _height = lines.Length;
        _width = lines[0].Length;
        _grid = new int[_height, _width];
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _grid[y, x] = int.Parse(lines[y][x].ToString());
            }
        }
    }

    public override int Day()
    {
        return 9;
    }

    public override string SolvePart1()
    {
        return LowPoints().Select(point => Height(point) + 1).Sum().ToString();
    }

    public override string SolvePart2()
    {
        var basinSizes = new List<int>();
        var seen = new HashSet<Point>();
        foreach (var point in LowPoints())
        {
            var size = 0;
            var queue = new Queue<Point>();
            queue.Enqueue(point);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (seen.Contains(current))
                {
                    continue;
                }
                seen.Add(current);
                size++;
                foreach (var neighbor in AdjacentRising(current))
                {
                    queue.Enqueue(neighbor);
                }
            }
            basinSizes.Add(size);
        }
        basinSizes.Sort((a, b) => b.CompareTo(a));
        return (basinSizes[0] * basinSizes[1] * basinSizes[2]).ToString();
    }

    private IEnumerable<Point> LowPoints()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var point = new Point(x, y);
                if (Adjacent(point).All(p => Height(point) < Height(p)))
                {
                    yield return point;
                }
            }
        }
    }

    private IEnumerable<Point> AdjacentRising(Point point)
    {
        return Adjacent(point)
            .Where(p => Height(p) > Height(point))
            .Where(p => Height(p) != 9);
    }

    private IEnumerable<Point> Adjacent(Point point)
    {
        if (point.X > 0)
        {
            yield return new Point(point.X - 1, point.Y);
        }
        if (point.X < _width - 1)
        {
            yield return new Point(point.X + 1, point.Y);
        }
        if (point.Y > 0)
        {
            yield return new Point(point.X, point.Y - 1);
        }
        if (point.Y < _height - 1)
        {
            yield return new Point(point.X, point.Y + 1);
        }
    }

    private int Height(Point point)
    {
        return _grid[point.Y, point.X];
    }
}