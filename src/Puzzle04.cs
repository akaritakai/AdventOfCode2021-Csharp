using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle04 : AbstractPuzzle
    {
        public Puzzle04(string input) : base(input)
        {
        }

        public override int Day()
        {
            return 4;
        }

        public override string SolvePart1()
        {
            return "TODO";
        }

        public override string SolvePart2()
        {
            return "TODO";
        }
        
        private class BingoBoard
        {
            private bool won = false;
            private int lastNumber = -1;
            private readonly int[,] board = new int[5, 5];
            private readonly bool[,] marks = new bool[5, 5];
            
            public BingoBoard()
        }
    }
}
