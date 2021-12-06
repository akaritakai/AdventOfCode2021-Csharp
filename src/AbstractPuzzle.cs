namespace AdventOfCode2021
{
    public abstract class AbstractPuzzle
    {
        private readonly string input;

        public AbstractPuzzle(string input)
        {
            this.input = input;
        }

        public string Input
        {
            get { return input; }
        }

        public abstract int Day();

        public abstract string SolvePart1();

        public abstract string SolvePart2();
    }
}
