using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle03 : AbstractPuzzle
    {
        public Puzzle03(string input) : base(input)
        {
        }

        public override int Day()
        {
            return 3;
        }

        public override string SolvePart1()
        {
            Console.WriteLine("SOF" + Input + "EOF");
            var report = new LinkedList<string>(Input.Trim().Split('\n'));
            var length = report.First!.Value.Length;
            var gamma = 0;
            var epsilon = 0;
            for (var i = 0; i < length; i++)
            {
                if (MoreZeroes(report, i))
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
            var report = Input.Trim().Split('\n');
            var length = report[0].Length;
            var oxygenValues = new LinkedList<string>(report);
            for (var i = 0; i < length && oxygenValues.Count > 1; i++)
            {
                var j = i;
                if (MoreZeroes(oxygenValues, i))
                {
                    oxygenValues = new LinkedList<string>(oxygenValues.Where(x => x[j] == '1'));
                }
                else
                {
                    oxygenValues = new LinkedList<string>(oxygenValues.Where(x => x[j] == '0'));
                }
            }

            var co2Values = new LinkedList<string>(report);
            for (var i = 0; i < length && co2Values.Count > 1; i++)
            {
                var j = i;
                if (MoreZeroes(co2Values, i))
                {
                    co2Values = new LinkedList<string>(co2Values.Where(x => x[j] == '0'));
                }
                else
                {
                    co2Values = new LinkedList<string>(co2Values.Where(x => x[j] == '1'));
                }
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
