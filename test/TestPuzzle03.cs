using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle03
{
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle03(string.Join("\n", "00100", "11110", "10110", "10111", "10101", "01111", 
            "00111", "11100", "10000", "11001", "00010", "01010"));
        Assert.AreEqual("198", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(3);
        var puzzle = new Puzzle03(input);
        Assert.AreEqual("3885894", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle03(string.Join("\n", "00100", "11110", "10110", "10111", "10101", "01111", 
            "00111", "11100", "10000", "11001", "00010", "01010"));
        Assert.AreEqual("230", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(3);
        var puzzle = new Puzzle03(input);
        Assert.AreEqual("4375225", puzzle.SolvePart2());
    }
}