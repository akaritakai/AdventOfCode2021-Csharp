using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzle11
    {
        [TestMethod]
        public void TestPart1Example1()
        {
            var puzzle = new Puzzle11(string.Join("\n",
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526"));
            Assert.AreEqual("1656", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestSolvePart1()
        {
            var input = BasePuzzleTest.PuzzleInput(11);
            var puzzle = new Puzzle11(input);
            Assert.AreEqual("1634", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestPart2Example1()
        {
            var puzzle = new Puzzle11(string.Join("\n",
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526"));
            Assert.AreEqual("195", puzzle.SolvePart2());
        }

        [TestMethod]
        public void TestSolvePart2()
        {
            var input = BasePuzzleTest.PuzzleInput(11);
            var puzzle = new Puzzle11(input);
            Assert.AreEqual("210", puzzle.SolvePart2());
        }
    }
}
