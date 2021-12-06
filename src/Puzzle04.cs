using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Puzzle04 : AbstractPuzzle
    {
        private readonly int[] _numbers;
        private readonly List<BingoBoard> _boards = new();
        
        public Puzzle04(string input) : base(input)
        {
            var lines = Input.Split('\n').ToArray();
            _numbers = lines[0].Split(',').Select(int.Parse).ToArray();
            var remainingNumbers = string.Join(" ", lines.Skip(1))
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            for (var i = 0; i < remainingNumbers.Length; i += 25)
            {
                var boardNumbers = remainingNumbers.Skip(i).Take(25).ToArray();
                _boards.Add(new BingoBoard(boardNumbers));
            }
        }

        public override int Day()
        {
            return 4;
        }

        public override string SolvePart1()
        {
            foreach (var number in _numbers)
            {
                foreach (var board in _boards)
                {
                    board.AddNumber(number);
                    if (board.HasWon())
                    {
                        return board.Score().ToString();
                    }
                }
            }
            throw new Exception("No bingo board won with this given input");
        }

        public override string SolvePart2()
        {
            foreach (var number in _numbers)
            {
                foreach (var board in _boards)
                {
                    board.AddNumber(number);
                    if (_boards.All(b => b.HasWon()))
                    {
                        return board.Score().ToString();
                    }
                }
            }
            throw new Exception("Not all bingo board have won with the given input");
        }
        
        private class BingoBoard
        {
            private bool _won;
            private int _lastNumber;
            private readonly int[,] _board = new int[5, 5];
            private readonly bool[,] _marks = new bool[5, 5];

            public BingoBoard(IReadOnlyList<int> numbers)
            {
                var i = 0;
                for (var y = 0; y < 5; y++)
                {
                    for (var x = 0; x < 5; x++)
                    {
                        _board[y, x] = numbers[i++];
                    }
                }
            }

            public bool HasWon()
            {
                return _won;
            }

            public void AddNumber(int n)
            {
                if (HasWon()) return;
                _lastNumber = n;
                for (var y = 0; y < 5; y++)
                {
                    for (var x = 0; x < 5; x++)
                    {
                        if (_board[y, x] == n)
                        {
                            _marks[y, x] = true;
                        }
                    }
                }
                for (var i = 0; i < 5; i++)
                {
                    var row = true;
                    var col = true;
                    for (var j = 0; j < 5; j++)
                    {
                        row &= _marks[i, j];
                        col &= _marks[j, i];
                    }
                    if (!row && !col) continue;
                    _won = true;
                    return;
                }
            }

            public int Score()
            {
                var sum = 0;
                for (var y = 0; y < 5; y++)
                {
                    for (var x = 0; x < 5; x++)
                    {
                        if (!_marks[y, x])
                        {
                            sum += _board[y, x];
                        }
                    }
                }

                return sum * _lastNumber;
            }
        }
    }
}
