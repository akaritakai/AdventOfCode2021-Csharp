using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle01
{
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle01(string.Join("\n", "199", "200", "208", "210", "200", "207", "240", "269",
            "260", "263"));
        Assert.AreEqual("7", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(1);
        var puzzle = new Puzzle01(input);
        Assert.AreEqual("1532", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle01(string.Join("\n", "199", "200", "208", "210", "200", "207", "240", "269",
            "260", "263"));
        Assert.AreEqual("5", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(1);
        var puzzle = new Puzzle01(input);
        Assert.AreEqual("1571", puzzle.SolvePart2());
    }
}