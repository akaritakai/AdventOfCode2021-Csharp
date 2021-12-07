using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzle07
    {
        [TestMethod]
        public void TestPart1Example1()
        {
            var puzzle = new Puzzle07("16,1,2,0,4,2,7,1,2,14");
            Assert.AreEqual("37", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestSolvePart1()
        {
            var input = BasePuzzleTest.PuzzleInput(7);
            var puzzle = new Puzzle07(input);
            Assert.AreEqual("356922", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestPart2Example1()
        {
            var puzzle = new Puzzle07("16,1,2,0,4,2,7,1,2,14");
            Assert.AreEqual("168", puzzle.SolvePart2());
        }

        [TestMethod]
        public void TestSolvePart2()
        {
            var input = BasePuzzleTest.PuzzleInput(7);
            var puzzle = new Puzzle07(input);
            Assert.AreEqual("100347031", puzzle.SolvePart2());
        }
    }
}
