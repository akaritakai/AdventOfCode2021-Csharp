using System;
using AdventOfCode2021;

var fetcher = new PuzzleInputFetcher();
var puzzles = new AbstractPuzzle[] {
    new Puzzle01(fetcher.FetchPuzzleInput(1)),
    new Puzzle02(fetcher.FetchPuzzleInput(2)),
    new Puzzle03(fetcher.FetchPuzzleInput(3)),
    new Puzzle04(fetcher.FetchPuzzleInput(4)),
    new Puzzle05(fetcher.FetchPuzzleInput(5)),
    new Puzzle06(fetcher.FetchPuzzleInput(6)),
    new Puzzle07(fetcher.FetchPuzzleInput(7))
};
foreach (var puzzle in puzzles)
{
    var day = puzzle.Day().ToString("00");
    Console.WriteLine("Day " + day + " Part 1: " + puzzle.SolvePart1());
    Console.WriteLine("Day " + day + " Part 2: " + puzzle.SolvePart2());
}
