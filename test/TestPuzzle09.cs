using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle09
{
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle09(string.Join("\n",
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678"));
        Assert.AreEqual("15", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(9);
        var puzzle = new Puzzle09(input);
        Assert.AreEqual("550", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle09(string.Join("\n",
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678"));
        Assert.AreEqual("1134", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(9);
        var puzzle = new Puzzle09(input);
        Assert.AreEqual("1100682", puzzle.SolvePart2());
    }
}