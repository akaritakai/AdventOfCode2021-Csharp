using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle01 : AbstractPuzzle
    {
        private readonly int[] _depths; 
        
        public Puzzle01(string input) : base(input)
        {
            _depths = Input.Trim().Split('\n').Select(int.Parse).ToArray();
        }

        public override int Day()
        {
            return 1;
        }

        public override string SolvePart1()
        {
            var count = 0;
            for (var i = 1; i < _depths.Length; i++)
            {
                if (_depths[i] > _depths[i - 1])
                {
                    count++;
                }
            }
            return count.ToString();
        }

        public override string SolvePart2()
        {
            var count = 0;
            var prevSum = _depths[0] + _depths[1] + _depths[2];
            for (var i = 3; i < _depths.Length; i++)
            {
                var sum = _depths[i - 2] + _depths[i - 1] + _depths[i];
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
