using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzle06
    {
        [TestMethod]
        public void TestPart1Example1()
        {
            var puzzle = new Puzzle06("3,4,3,1,2");
            Assert.AreEqual("5934", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestSolvePart1()
        {
            var input = BasePuzzleTest.PuzzleInput(6);
            var puzzle = new Puzzle06(input);
            Assert.AreEqual("349549", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestPart2Example1()
        {
            var puzzle = new Puzzle06("3,4,3,1,2");
            Assert.AreEqual("26984457539", puzzle.SolvePart2());
        }

        [TestMethod]
        public void TestSolvePart2()
        {
            var input = BasePuzzleTest.PuzzleInput(6);
            var puzzle = new Puzzle06(input);
            Assert.AreEqual("1589590444365", puzzle.SolvePart2());
        }
    }
}
