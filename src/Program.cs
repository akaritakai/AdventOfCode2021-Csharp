using AdventOfCode2021;

var fetcher = new PuzzleInputFetcher();
var puzzles = new AbstractPuzzle[] {
    new Puzzle01(fetcher.FetchPuzzleInput(1)),
    new Puzzle02(fetcher.FetchPuzzleInput(2)),
    new Puzzle03(fetcher.FetchPuzzleInput(3)),
    new Puzzle04(fetcher.FetchPuzzleInput(4)),
    new Puzzle05(fetcher.FetchPuzzleInput(5)),
    new Puzzle06(fetcher.FetchPuzzleInput(6)),
    new Puzzle07(fetcher.FetchPuzzleInput(7)),
    new Puzzle08(fetcher.FetchPuzzleInput(8)),
    new Puzzle09(fetcher.FetchPuzzleInput(9)),
    new Puzzle10(fetcher.FetchPuzzleInput(10)),
    new Puzzle11(fetcher.FetchPuzzleInput(11)),
    new Puzzle12(fetcher.FetchPuzzleInput(12)),
    new Puzzle13(fetcher.FetchPuzzleInput(13)),
    new Puzzle14(fetcher.FetchPuzzleInput(14)),
    new Puzzle15(fetcher.FetchPuzzleInput(15)),
    new Puzzle16(fetcher.FetchPuzzleInput(16)),
};
foreach (var puzzle in puzzles)
{
    var day = puzzle.Day().ToString("00");
    Console.WriteLine("Day " + day + " Part 1: " + puzzle.SolvePart1());
    Console.WriteLine("Day " + day + " Part 2: " + puzzle.SolvePart2());
}
