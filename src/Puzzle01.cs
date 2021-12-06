using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle01 : AbstractPuzzle
    {
        public Puzzle01(string input) : base(input)
        {
        }

        public override int Day()
        {
            return 1;
        }

        public override string SolvePart1()
        {
            var depths = Input.Trim().Split('\n').Select(long.Parse).ToArray();
            var count = 0;
            for (var i = 1; i < depths.Length; i++)
            {
                if (depths[i] > depths[i - 1])
                {
                    count++;
                }
            }
            return count.ToString();
        }

        public override string SolvePart2()
        {
            var depths = Input.Trim().Split('\n').Select(long.Parse).ToArray();
            var count = 0;
            var prevSum = depths[0] + depths[1] + depths[2];
            for (var i = 3; i < depths.Length; i++)
            {
                var sum = depths[i - 2] + depths[i - 1] + depths[i];
                if (sum > prevSum)
                {
                    count++;
                }
                prevSum = sum;
            }
            return count.ToString();
        }
    }
}
