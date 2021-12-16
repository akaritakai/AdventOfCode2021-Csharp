using System.Drawing;

namespace AdventOfCode2021;

public class Puzzle11 : AbstractPuzzle
{
    private int _height;
    private int _width;

    public Puzzle11(string input) : base(input)
    {
    }

    public override int Day()
    {
        return 11;
    }

    public override string SolvePart1()
    {
        var grid = InputToGrid();
        var sum = 0;
        for (var step = 0; step < 100; step++)
        {
            sum += DoStep(grid);
        }
        return sum.ToString();
    }

    public override string SolvePart2()
    {
        var grid = InputToGrid();
        var step = 1;
        while (true)
        {
            var count = DoStep(grid);
            if (count == _height * _width)
            {
                return step.ToString();
            }

            step++;
        }
    }

    private int DoStep(int[,] grid)
    {
        var numFlashed = 0;

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                grid[y, x]++;
            }
        }
        var flashed = new bool[_height, _width];
        var anyFlashed = false;
        do
        {
            anyFlashed = false;
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (grid[y, x] <= 9 || flashed[y, x]) continue;
                    flashed[y, x] = true;
                    anyFlashed = true;
                    numFlashed++;
                    foreach (var point in Adjacent(x, y))
                    {
                        grid[point.Y, point.X]++;
                    }
                }
            }
        } while (anyFlashed);
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (flashed[y, x])
                {
                    grid[y, x] = 0;
                }
            }
        }
        return numFlashed;
    }

    private IEnumerable<Point> Adjacent(int x, int y)
    {
        for (var dx = -1; dx <= 1; dx++)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                if ((dx != 0 || dy != 0) && x + dx >= 0 && x + dx < _width && y + dy >= 0 && y + dy < _height)
                {
                    yield return new Point(x + dx, y + dy);
                }
            }
        }
    }

    private int[,] InputToGrid()
    {
        var lines = Input.Trim().Split('\n');
        _height = lines.Length;
        _width = lines[0].Length;
        var grid = new int[_height, _width];
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                grid[y, x] = lines[y][x] - '0';
            }
        }
        return grid;
    }
}