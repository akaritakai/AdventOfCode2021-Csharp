using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle03 : AbstractPuzzle
    {
        private readonly IReadOnlyCollection<string> _report;

        public Puzzle03(string input) : base(input)
        {
            _report = Input.Trim().Split('\n').ToArray();
        }

        public override int Day()
        {
            return 3;
        }

        public override string SolvePart1()
        {
            var length = _report.First().Length;
            var gamma = 0;
            var epsilon = 0;
            for (var i = 0; i < length; i++)
            {
                if (MoreZeroes(_report, i))
                {
                    gamma <<= 1;
                    epsilon = (epsilon << 1) | 1;
                }
                else
                {
                    gamma = (gamma << 1) | 1;
                    epsilon <<= 1;
                }
            }

            return (gamma * epsilon).ToString();
        }

        public override string SolvePart2()
        {
            var length = _report.First().Length;
            var oxygenValues = new LinkedList<string>(_report);
            for (var i = 0; i < length && oxygenValues.Count > 1; i++)
            {
                var j = i;
                oxygenValues = MoreZeroes(oxygenValues, i)
                    ? new LinkedList<string>(oxygenValues.Where(x => x[j] == '1'))
                    : new LinkedList<string>(oxygenValues.Where(x => x[j] == '0'));
            }

            var co2Values = new LinkedList<string>(_report);
            for (var i = 0; i < length && co2Values.Count > 1; i++)
            {
                var j = i;
                co2Values = MoreZeroes(co2Values, i)
                    ? new LinkedList<string>(co2Values.Where(x => x[j] == '0'))
                    : new LinkedList<string>(co2Values.Where(x => x[j] == '1'));
            }
            var oxygenRating = Convert.ToUInt32(oxygenValues.First!.Value, 2);
            var co2Rating = Convert.ToUInt32(co2Values.First!.Value, 2);
            return (oxygenRating * co2Rating).ToString();
        }

        private static bool MoreZeroes(IReadOnlyCollection<string> report, int position)
        {
            return report.Count(s => s[position] == '0') > (report.Count / 2);
        }
    }
}
