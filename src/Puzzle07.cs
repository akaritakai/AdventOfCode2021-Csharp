using System;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle07 : AbstractPuzzle
    {
        private readonly int[] _crabPositions;
        private readonly int _minPosition;
        private readonly int _maxPosition;
        
        public Puzzle07(string input) : base(input)
        {
            _crabPositions = input.Split(',').Select(int.Parse).ToArray();
            _minPosition = _crabPositions.Min();
            _maxPosition = _crabPositions.Max();
        }

        public override int Day()
        {
            return 7;
        }

        public override string SolvePart1()
        {
            var minCost = int.MaxValue;
            for (var i = _minPosition; i <= _maxPosition; i++)
            {
                var cost = _crabPositions.Sum(position => Math.Abs(position - i));
                minCost = Math.Min(cost, minCost);
            }
            return minCost.ToString();
        }

        public override string SolvePart2()
        {
            var minCost = int.MaxValue;
            for (var i = _minPosition; i <= _maxPosition; i++)
            {
                var cost = _crabPositions.Select(position => Math.Abs(position - i))
                    .Select(distance => distance * (distance + 1) / 2)
                    .Sum();
                minCost = Math.Min(cost, minCost);
            }
            return minCost.ToString();
        }
    }
}
