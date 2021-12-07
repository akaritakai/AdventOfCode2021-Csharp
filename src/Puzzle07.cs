using System;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle07 : AbstractPuzzle
    {
        private readonly int[] _crabPositions;
        
        public Puzzle07(string input) : base(input)
        {
            _crabPositions = input.Split(',').Select(int.Parse).OrderBy(x => x).ToArray();
        }

        public override int Day()
        {
            return 7;
        }

        public override string SolvePart1()
        {
            var median = _crabPositions[_crabPositions.Length / 2];
            return _crabPositions.Sum(position => Math.Abs(position - median)).ToString();
        }

        public override string SolvePart2()
        {
            var mean = _crabPositions.Sum() / (double) _crabPositions.Length;
            var floorCost = _crabPositions.Select(x => (int) Math.Abs(x - Math.Floor(mean)))
                .Sum(x => x * (x + 1) / 2);
            var ceilCost = _crabPositions.Select(x => (int) Math.Abs(x - Math.Ceiling(mean)))
                .Sum(x => x * (x + 1) / 2);
            return Math.Min(floorCost, ceilCost).ToString();
        }
    }
}
