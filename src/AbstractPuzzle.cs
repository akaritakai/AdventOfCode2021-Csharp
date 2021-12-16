namespace AdventOfCode2021;

public abstract class AbstractPuzzle
{
    protected AbstractPuzzle(string input)
    {
        Input = input;
    }

    protected string Input { get; }

    public abstract int Day();

    public abstract string SolvePart1();

    public abstract string SolvePart2();
}