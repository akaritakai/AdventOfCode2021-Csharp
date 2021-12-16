using System.Drawing;

namespace AdventOfCode2021;

public class Puzzle15 : AbstractPuzzle
{
    public Puzzle15(string input) : base(input)
    {
    }

    public override int Day()
    {
        return 15;
    }

    public override string SolvePart1()
    {
        return new Graph(Input, false).FindMinRisk().ToString();
    }

    public override string SolvePart2()
    {
        return new Graph(Input, true).FindMinRisk().ToString();
    }

    private class Graph
    {
        private readonly int[,] _risk;
        private readonly int[,] _dist;
        private readonly int _height;
        private readonly int _width;

        public Graph(string input, bool part2)
        {
            var lines = input.Trim().Split('\n');
            var height = lines.Length;
            var width = lines[0].Length;
            var maxY = part2 ? height * 5 : height;
            var maxX = part2 ? width * 5 : width;
            _risk = new int[maxY, maxX];
            _dist = new int[maxY, maxX];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    _risk[y, x] = lines[y][x] - '0';
                }
            }
            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    if (x == 0 && y == 0)
                    {
                        _dist[0, 0] = 0;
                    }
                    else
                    {
                        _dist[y, x] = 10 * maxX * maxY;
                    }
                    _risk[y, x] = (_risk[y % height, x % width] + x / width + y / height - 1) % 9 + 1;
                }
            }
            _height = maxY;
            _width = maxX;
        }

        public int FindMinRisk()
        {
            var queue = new PriorityQueue<Point, int>();
            queue.Enqueue(new Point(0, 0), 0);
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                if (point.X == _width - 1 && point.Y == _height - 1)
                {
                    return _dist[point.Y, point.X];
                }
                foreach (var adjacent in Adjacent(point))
                {
                    var newDist = _dist[point.Y, point.X] + _risk[adjacent.Y, adjacent.X];
                    if (newDist >= _dist[adjacent.Y, adjacent.X]) continue;
                    _dist[adjacent.Y, adjacent.X] = newDist;
                    queue.Enqueue(adjacent, newDist);
                }
            }
            throw new Exception("No path found");
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
    }
}