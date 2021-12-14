using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle14 : AbstractPuzzle
    {
        private readonly string _template;
        private readonly Dictionary<string, string> _rules = new();

        public Puzzle14(string input) : base(input)
        {
            var parts = Input.Trim().Split("\n\n");
            _template = parts[0];
            foreach (var part in parts[1].Split("\n"))
            {
                var ruleParts = part.Split(" -> ");
                _rules[ruleParts[0]] = ruleParts[1];
            }
        }

        public override int Day()
        {
            return 14;
        }

        public override string SolvePart1()
        {
            var counter = MakeCounter();
            for (var step = 0; step < 10; step++)
            {
                counter = DoStep(counter);
            }
            return Score(counter).ToString();
        }

        public override string SolvePart2()
        {
            var counter = MakeCounter();
            for (var step = 0; step < 40; step++)
            {
                counter = DoStep(counter);
            }
            return Score(counter).ToString();
        }

        private Dictionary<string, long> MakeCounter()
        {
            var counter = new Dictionary<string, long>();
            for (var i = 0; i < _template.Length - 1; i++)
            {
                var key = _template.Substring(i, 2);
                counter[key] = counter.ContainsKey(key) ? counter[key] + 1 : 1;
            }
            return counter;
        }

        private Dictionary<string, long> DoStep(Dictionary<string, long> counter)
        {
            var next = new Dictionary<string, long>();
            foreach (var (pair, value) in counter)
            {
                if (!_rules.ContainsKey(pair)) continue;
                var middle = _rules[pair];
                var key = pair[0] + middle;
                next[key] = next.ContainsKey(key) ? next[key] + value : value;
                key = middle + pair[1];
                next[key] = next.ContainsKey(key) ? next[key] + value : value;
            }
            return next;
        }

        private long Score(Dictionary<string, long> counter)
        {
            var map = new long[26];
            foreach (var (pair, value) in counter)
            {
                map[pair[0] - 'A'] += value;
            }
            map[_template[^1] - 'A'] += 1;
            var max = map.Where(i => i > 0).Max();
            var min = map.Where(i => i > 0).Min();
            return max - min;
        }
    }
}
