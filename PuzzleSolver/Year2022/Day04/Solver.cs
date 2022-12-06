using System.Text.RegularExpressions;
using Spectre.Console;

namespace PuzzleSolver.Year2022.Day04;

[PuzzleDescription("Day 4: Camp Cleanup", 2022, 4)]
public class Solver : PuzzleSolver
{
    private readonly List<string> _puzzleInput = new();

    protected override void ProcessInput(List<string> lines) => _puzzleInput.AddRange(lines);

    protected override void SolvePartOne()
    {
        int overlappingAssignments = _puzzleInput.Count(input => RangesFullyOverlap(GetGroupRanges(input)));
        AddPartOneAnswer("Total overlapping assignments.", overlappingAssignments);
    }

    protected override void SolvePartTwo()
    {
        int partiallyOverlappingAssignments = _puzzleInput.Count(input => RangesPartiallyOverlap(GetGroupRanges(input)));
        AddPartTwoAnswer("Total partially overlapping assignments.", partiallyOverlappingAssignments);
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
