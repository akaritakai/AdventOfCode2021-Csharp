using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

[TestClass]
public class TestPuzzle10
{
    [TestMethod]
    public void TestPart1Example1()
    {
        var puzzle = new Puzzle10(string.Join("\n",
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]"));
        Assert.AreEqual("26397", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestSolvePart1()
    {
        var input = BasePuzzleTest.PuzzleInput(10);
        var puzzle = new Puzzle10(input);
        Assert.AreEqual("271245", puzzle.SolvePart1());
    }

    [TestMethod]
    public void TestPart2Example1()
    {
        var puzzle = new Puzzle10(string.Join("\n",
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]"));
        Assert.AreEqual("288957", puzzle.SolvePart2());
    }

    [TestMethod]
    public void TestSolvePart2()
    {
        var input = BasePuzzleTest.PuzzleInput(10);
        var puzzle = new Puzzle10(input);
        Assert.AreEqual("1685293086", puzzle.SolvePart2());
    }
}