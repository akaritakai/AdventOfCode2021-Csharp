using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle02 : AbstractPuzzle
    {
        private readonly string[] _instructions;
        
        public Puzzle02(string input) : base(input)
        {
            _instructions = Input.Trim().Split('\n').ToArray();
        }

        public override int Day()
        {
            return 2;
        }

        public override string SolvePart1()
        {
            var x = 0;
            var y = 0;
            foreach (var instruction in _instructions)
            {
                var command = instruction.Split(' ')[0];
                var value = int.Parse(instruction.Split(' ')[1]);
                switch (command)
                {
                    case "forward":
                        x += value;
                        break;
                    case "down":
                        y += value;
                        break;
                    case "up":
                        y -= value;
                        break;
                }
            }
            return (x * y).ToString();
        }

        public override string SolvePart2()
        {
            var x = 0;
            var y = 0;
            var aim = 0;
            foreach (var instruction in _instructions)
            {
                var command = instruction.Split(' ')[0];
                var value = int.Parse(instruction.Split(' ')[1]);
                switch (command)
                {
                    case "forward":
                        x += value;
                        y += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }
            return (x * y).ToString();
        }
    }
}
