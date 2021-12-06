using AdventOfCode2021;

var fetcher = new PuzzleInputFetcher();
var puzzles = new AbstractPuzzle[] {
    new Puzzle01(fetcher.FetchPuzzleInput(1))
};
foreach (var puzzle in puzzles)
{
    var day = puzzle.Day().ToString("00");
    Console.WriteLine("Day " + day + " Part 1: " + puzzle.SolvePart1());
    Console.WriteLine("Day " + day + " Part 2: " + puzzle.SolvePart2());
}
