using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzle14
    {
        [TestMethod]
        public void TestPart1Example1()
        {
            var puzzle = new Puzzle14(string.Join("\n", 
                "NNCB",
                "",
                "CH -> B",
                "HH -> N",
                "CB -> H",
                "NH -> C",
                "HB -> C",
                "HC -> B",
                "HN -> C",
                "NN -> C",
                "BH -> H",
                "NC -> B",
                "NB -> B",
                "BN -> B",
                "BB -> N",
                "BC -> B",
                "CC -> N",
                "CN -> C"));
            Assert.AreEqual("1588", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestSolvePart1()
        {
            var input = BasePuzzleTest.PuzzleInput(14);
            var puzzle = new Puzzle14(input);
            Assert.AreEqual("3247", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestPart2Example1()
        {
            var puzzle = new Puzzle14(string.Join("\n", 
                "NNCB",
                "",
                "CH -> B",
                "HH -> N",
                "CB -> H",
                "NH -> C",
                "HB -> C",
                "HC -> B",
                "HN -> C",
                "NN -> C",
                "BH -> H",
                "NC -> B",
                "NB -> B",
                "BN -> B",
                "BB -> N",
                "BC -> B",
                "CC -> N",
                "CN -> C"));
            Assert.AreEqual("2188189693529", puzzle.SolvePart2());
        }

        [TestMethod]
        public void TestSolvePart2()
        {
            var input = BasePuzzleTest.PuzzleInput(14);
            var puzzle = new Puzzle14(input);
            Assert.AreEqual("4110568157153", puzzle.SolvePart2());
        }
    }
}
