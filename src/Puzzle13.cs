using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle13 : AbstractPuzzle
    {
        private readonly ISet<Point> _points = new HashSet<Point>();
        private readonly IList<(char, int)> _instructions = new List<(char, int)>();
        
        public Puzzle13(string input) : base(input)
        {
            var parts = Input.Trim().Split("\n\n");
            foreach (var line in parts[0].Split('\n'))
            {
                var x = int.Parse(line.Split(',')[0]);
                var y = int.Parse(line.Split(',')[1]);
                _points.Add(new Point(x, y));
            }
            foreach (var line in parts[1].Split('\n'))
            {
                var split = line.Replace("fold along ", "").Split('=');
                var axis = split[0][0];
                var value = int.Parse(split[1]);
                _instructions.Add((axis, value));
            }
        }

        public override int Day()
        {
            return 13;
        }

        public override string SolvePart1()
        {
            var (axis, value) = _instructions[0];
            var points = new HashSet<Point>(_points);
            if (axis == 'x')
            {
                FoldX(points, value);
            }
            else
            {
                FoldY(points, value);
            }
            return points.Count.ToString();
        }

        public override string SolvePart2()
        {
            var points = new HashSet<Point>(_points);
            foreach (var (axis, value) in _instructions)
            {
                if (axis == 'x')
                {
                    FoldX(points, value);
                }
                else
                {
                    FoldY(points, value);
                }
            }
            return LetterOcr.Parse(ToImage(points));
        }

        private static void FoldX(ISet<Point> points, int x)
        {
            var collection = points.ToList();
            foreach (var point in collection.Where(point => point.X > x))
            {
                points.Remove(point);
                points.Add(new Point(2 * x - point.X, point.Y));
            }
        }
        
        private static void FoldY(ISet<Point> points, int y)
        {
            var collection = points.ToList();
            foreach (var point in collection.Where(point => point.Y > y))
            {
                points.Remove(point);
                points.Add(new Point(point.X, 2 * y - point.Y));
            }
        }

        private static bool[,] ToImage(IEnumerable<Point> points)
        {
            var collection = points.ToList();
            var minX = collection.Min(point => point.X);
            var minY = collection.Min(point => point.Y);
            var maxX = collection.Max(point => point.X);
            var maxY = collection.Max(point => point.Y);
            var image = new bool[maxY - minY + 1, maxX - minX + 1];
            foreach (var point in collection)
            {
                image[point.Y - minY, point.X - minX] = true;
            }
            return image;
        }
    }
}
