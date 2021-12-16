using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle15
{
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle15(string.Join("\n",
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581"));
        Assert.AreEqual("40", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(15);
        var puzzle = new Puzzle15(input);
        Assert.AreEqual("458", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle15(string.Join("\n",
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581"));
        Assert.AreEqual("315", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(15);
        var puzzle = new Puzzle15(input);
        Assert.AreEqual("2800", puzzle.SolvePart2());
    }
}