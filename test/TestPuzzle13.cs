using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzle13
    {
        [TestMethod]
        public void TestPart1Example1()
        {
            var puzzle = new Puzzle13(string.Join("\n",
                "6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
                "",
                "fold along y=7",
                "fold along x=5"));
            Assert.AreEqual("17", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestSolvePart1()
        {
            var input = BasePuzzleTest.PuzzleInput(13);
            var puzzle = new Puzzle13(input);
            Assert.AreEqual("655", puzzle.SolvePart1());
        }

        [TestMethod]
        public void TestSolvePart2()
        {
            var input = BasePuzzleTest.PuzzleInput(13);
            var puzzle = new Puzzle13(input);
            Assert.AreEqual("JPZCUAUR", puzzle.SolvePart2());
        }
    }
}
