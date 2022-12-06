using System.Text.RegularExpressions;

namespace PuzzleSolver.Year2022.Day04;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/4.
/// </summary>
[PuzzleDescription("Day 4: Camp Cleanup", 2022, 4)]
public sealed class Solver : PuzzleSolver
{
    private readonly List<string> _puzzleInput = new();

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne() => _puzzleInput.Count(input => RangesFullyOverlap(GetGroupRanges(input)));

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo() => _puzzleInput.Count(input => RangesPartiallyOverlap(GetGroupRanges(input)));

    /// <inheritdoc/>
    public override void ProcessInput(List<string> lines) => _puzzleInput.AddRange(lines);

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();
        AddPartOneAnswer("Total overlapping assignments.", partOne);
        AddPartTwoAnswer("Total partially overlapping assignments.", partTwo);
    }

    private static GroupRanges GetGroupRanges(string inputLine)
    {
        const string pattern = @"(\d+)-(\d+),(\d+)-(\d+)";
        Regex reg = new Regex(pattern, RegexOptions.Compiled);
        Match match = reg.Match(inputLine);

        return new GroupRanges
        {
            OneStart = int.Parse(match.Groups[1].Value),
            OneEnd = int.Parse(match.Groups[2].Value),
            TwoStart = int.Parse(match.Groups[3].Value),
            TwoEnd = int.Parse(match.Groups[4].Value),
        };
    }

    private static bool RangesFullyOverlap(GroupRanges group) =>
        (group.OneStart <= group.TwoStart && group.OneEnd >= group.TwoEnd) || // one encapsulates two
        (group.OneStart >= group.TwoStart && group.OneEnd <= group.TwoEnd); // two encapsulates one

    private static bool RangesPartiallyOverlap(GroupRanges group) =>
        (group.OneStart <= group.TwoStart &&
         group.OneStart <= group.TwoEnd &&
         group.OneEnd >= group.TwoStart) || // one overlaps start of two
        (group.TwoStart <= group.OneStart &&
         group.TwoStart <= group.OneEnd &&
         group.TwoEnd >= group.OneStart); // two overlaps start of one

    private struct GroupRanges
    {
        public int OneStart { get; init; }

        public int OneEnd { get; init; }

        public int TwoStart { get; init; }

        public int TwoEnd { get; init; }
    }
}
