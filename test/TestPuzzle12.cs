using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle12
{
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle12(string.Join("\n",
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"));
        Assert.AreEqual("10", puzzle.SolvePart1());
    }
        
    [TestMethod]
    public void TestPart1Example2()
    {
        var puzzle = new Puzzle12(string.Join("\n",
            "dc-end",
            "HN-start",
            "start-kj",
            "dc-start",
            "dc-HN",
            "LN-dc",
            "HN-end",
            "kj-sa",
            "kj-HN",
            "kj-dc"));
        Assert.AreEqual("19", puzzle.SolvePart1());
    }
        
    [TestMethod]
    public void TestPart1Example3()
    {
        var puzzle = new Puzzle12(string.Join("\n",
            "fs-end",
            "he-DX",
            "fs-he",
            "start-DX",
            "pj-DX",
            "end-zg",
            "zg-sl",
            "zg-pj",
            "pj-he",
            "RW-he",
            "fs-DX",
            "pj-RW",
            "zg-RW",
            "start-pj",
            "he-WI",
            "zg-he",
            "pj-fs",
            "start-RW"));
        Assert.AreEqual("226", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(12);
        var puzzle = new Puzzle12(input);
        Assert.AreEqual("4338", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle12(string.Join("\n",
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"));
        Assert.AreEqual("36", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example2()
    {
        var puzzle = new Puzzle12(string.Join("\n",
            "dc-end",
            "HN-start",
            "start-kj",
            "dc-start",
            "dc-HN",
            "LN-dc",
            "HN-end",
            "kj-sa",
            "kj-HN",
            "kj-dc"));
        Assert.AreEqual("103", puzzle.SolvePart2());
    }
        
    [TestMethod]
    public void TestPart2Example3()
    {
        var puzzle = new Puzzle12(string.Join("\n",
            "fs-end",
            "he-DX",
            "fs-he",
            "start-DX",
            "pj-DX",
            "end-zg",
            "zg-sl",
            "zg-pj",
            "pj-he",
            "RW-he",
            "fs-DX",
            "pj-RW",
            "zg-RW",
            "start-pj",
            "he-WI",
            "zg-he",
            "pj-fs",
            "start-RW"));
        Assert.AreEqual("3509", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(12);
        var puzzle = new Puzzle12(input);
        Assert.AreEqual("114189", puzzle.SolvePart2());
    }
}