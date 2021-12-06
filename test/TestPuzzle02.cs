using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzle02
    {
        [TestMethod]
        public void TestPart1Example1()
        {
            var puzzle = new Puzzle02(string.Join("\n", "forward 5", "down 5", "forward 8", "up 3", "down 8",
                "forward 2"));
            Assert.AreEqual("150", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestSolvePart1()
        {
            var input = BasePuzzleTest.PuzzleInput(2);
            var puzzle = new Puzzle02(input);
            Assert.AreEqual("1604850", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestPart2Example1()
        {
            var puzzle = new Puzzle02(string.Join("\n", "forward 5", "down 5", "forward 8", "up 3", "down 8",
                "forward 2"));
            Assert.AreEqual("900", puzzle.SolvePart2());
        }

        [TestMethod]
        public void TestSolvePart2()
        {
            var input = BasePuzzleTest.PuzzleInput(2);
            var puzzle = new Puzzle02(input);
            Assert.AreEqual("1685186100", puzzle.SolvePart2());
        }
    }
}
