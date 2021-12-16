using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle05
{
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle05(string.Join("\n",
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2"));
        Assert.AreEqual("5", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(5);
        var puzzle = new Puzzle05(input);
        Assert.AreEqual("6113", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle05(string.Join("\n",
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2"));
        Assert.AreEqual("12", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(5);
        var puzzle = new Puzzle05(input);
        Assert.AreEqual("20373", puzzle.SolvePart2());
    }
}