namespace AdventOfCode2021;

public class Puzzle06 : AbstractPuzzle
{
    public Puzzle06(string input) : base(input)
    {
    }

    public override int Day()
    {
        return 6;
    }

    public override string SolvePart1()
    {
        return Simulate(80).ToString();
    }

    public override string SolvePart2()
    {
        return Simulate(256).ToString();
    }

    private long Simulate(int days)
    {
        var fish = new long[9];
        foreach (var i in Input.Trim().Split(',').Select(int.Parse))
        {
            fish[i]++;
        }
        var pointer = 0;
        for (var day = 0; day < days; day++)
        {
            fish[(pointer + 7) % 9] += fish[pointer];
            pointer = (pointer + 1) % 9;
        }
        return fish.Sum();
    }
}