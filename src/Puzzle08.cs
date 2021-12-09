using System;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle08 : AbstractPuzzle
    {
        public Puzzle08(string input) : base(input)
        {
        }

        public override int Day()
        {
            return 8;
        }

        public override string SolvePart1()
        {
            return Input
                .Trim()
                .Split('\n')
                .Select(line => line
                    .Split(" | ")[1]
                    .Split(' ')
                    .Count(token => token.Length is 2 or 3 or 4 or 7))
                .Sum()
                .ToString();
        }

        public override string SolvePart2()
        {
            return Input
                .Trim()
                .Split('\n')
                .Select(line =>
                {
                    var parts = line.Split(" | ");
                    var patterns = parts[0].Split(' ');
                    var outputs = parts[1].Split(' ');
                    var solver = Solver(patterns);
                    return outputs
                        .Select(output => solver(output))
                        .Aggregate((a, b) => 10 * a + b);
                })
                .Sum()
                .ToString();
        }

        private static Func<string, int> Solver(string[] patterns)
        {
            var digits = new string[10];
            // We can deduce '1', '4', '7', and '8' by their length
            digits[1] = patterns.First(p => p.Length is 2);
            digits[4] = patterns.First(p => p.Length is 4);
            digits[7] = patterns.First(p => p.Length is 3);
            digits[8] = patterns.First(p => p.Length is 7);
            // We can deduce '6' as it is the only number to have length 6 and share 1 value in common with '1'
            digits[6] = patterns.First(p => p.Length is 6 && p.Intersect(digits[1]).Count() is 1);
            // We can deduce f as the intersection of '6' and '1'
            var f = digits[1].Intersect(digits[6]).First();
            // We can deduce c as '1' set minus f
            var c = digits[1].First(x => x != f);
            // We can deduce '3' as it is the only number to have length 5 and contain both c and f
            digits[3] = patterns.First(p => p.Length is 5 && p.Contains(c) && p.Contains(f));
            // We can deduce '2' as it is the only number to have length 5 and share 2 values in common with '4'
            digits[2] = patterns.First(p => p.Length is 5 && p.Intersect(digits[4]).Count() is 2);
            // We can deduce b as '4' set minus '3'
            var b = digits[4].First(x => !digits[3].Contains(x));
            // We can deduce '5' as it is the only number to have length 5 and contain b
            digits[5] = patterns.First(p => p.Length is 5 && p.Contains(b));
            // We can deduce d as '4' set minus '1' set minus 'b'
            var d = digits[4].First(x => !digits[1].Contains(x) && x != b);
            // We can deduce '0' as it is the only number to have length 6 and not contain d
            digits[0] = patterns.First(p => p.Length is 6 && !p.Contains(d));
            // We can deduce '9' as it is the only number to have length 6 and contain both c and d
            digits[9] = patterns.First(p => p.Length is 6 && p.Contains(c) && p.Contains(d));
            // Sort the digits at the end (normalizing the order)
            for (var i = 0; i < 10; i++)
            {
                digits[i] = new string(digits[i].OrderBy(x => x).ToArray());
            }
            return pattern =>
            {
                var sorted = new string(pattern.OrderBy(x => x).ToArray());
                for (var i = 0; i < 10; i++)
                {
                    if (sorted == digits[i])
                    {
                        return i;
                    }
                }
                throw new Exception("No digit found for pattern: " + pattern);
            };
        }
    }
}
