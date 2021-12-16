namespace AdventOfCode2021;

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
            if (MostCommonBit(_report, i) == '0')
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
        var oxygenValues = new List<string>(_report);
        for (var i = 0; i < length && oxygenValues.Count > 1; i++)
        {
            var j = i;
            var leastCommonBit = LeastCommonBit(oxygenValues, j);
            oxygenValues.RemoveAll(x => x[j] != leastCommonBit);
        }

        var co2Values = new List<string>(_report);
        for (var i = 0; i < length && co2Values.Count > 1; i++)
        {
            var j = i;
            var mostCommonBit = MostCommonBit(co2Values, j);
            co2Values.RemoveAll(x => x[j] != mostCommonBit);
        }
        var oxygenRating = Convert.ToUInt32(oxygenValues[0], 2);
        var co2Rating = Convert.ToUInt32(co2Values[0], 2);
        return (oxygenRating * co2Rating).ToString();
    }

    private static char MostCommonBit(IReadOnlyCollection<string> report, int position)
    {
        return report.Count(s => s[position] == '0') > (report.Count / 2) ? '0' : '1';
    }

    private static char LeastCommonBit(IReadOnlyCollection<string> report, int position)
    {
        return MostCommonBit(report, position) == '0' ? '1' : '0';
    }
}