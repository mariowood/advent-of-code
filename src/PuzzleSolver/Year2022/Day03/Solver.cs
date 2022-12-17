namespace PuzzleSolver.Year2022.Day03;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/3.
/// </summary>
[PuzzleDescription("Day 3: Rucksack Reorganization", 2022, 3)]
public sealed class Solver : SolverBase
{
    private readonly List<string> _puzzleInput = new();

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne() => _puzzleInput.Select(GetDuplicateItemForPack).Sum();

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo()
    {
        if (_puzzleInput.Count % 3 != 0)
        {
            throw new ArgumentException();
        }

        int badgePriorityTotal = 0;

        for (int i = 0; i < _puzzleInput.Count; i += 3)
        {
            List<string> group = _puzzleInput.GetRange(i, 3);
            int badge = GetDuplicateBadgeForGroup(group);
            badgePriorityTotal += GetItemPriority(badge);
        }

        return badgePriorityTotal;
    }

    /// <inheritdoc/>
    public override void ProcessInput(List<string> input) => _puzzleInput.AddRange(input);

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();

        AddPartOneAnswer("The total item priority.", partOne);
        AddPartTwoAnswer("The total badge priority.", partTwo);
    }

    private static int GetDuplicateItemForPack(string line)
    {
        string one = line[..(line.Length / 2)];
        string two = line[(line.Length / 2)..];

        return GetItemPriority(one.First(two.Contains));
    }

    private static int GetDuplicateBadgeForGroup(List<string> group) =>
        group[0].First(i =>
            group[1].Contains(i, StringComparison.Ordinal) &&
            group[2].Contains(i, StringComparison.Ordinal));

    private static int GetItemPriority(int item)
    {
        if (item <= 90)
        {
            return item - 38;
        }

        return item - 96;
    }
}
