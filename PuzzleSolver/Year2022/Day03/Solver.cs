using Spectre.Console;

namespace PuzzleSolver.Year2022.Day03;

[PuzzleDescription("Day 3: Rucksack Reorganization", 2022, 3)]
public class Solver : PuzzleSolver
{
    private readonly List<string> _puzzleInput = new();

    public override void ProcessInput(List<string> lines) => _puzzleInput.AddRange(lines);

    public override void SolvePartOne()
    {
        int itemPriorityTotals = _puzzleInput.Select(GetDuplicateItemForPack).Sum();
        AnsiConsole.MarkupLine($"{Constants.PartOne} The total item priority is [green]{itemPriorityTotals}[/]");
    }

    public override void SolvePartTwo()
    {
        if (_puzzleInput.Count % 3 != 0)
        {
            throw new ArgumentException();
        }

        int badgePriorityTotal = 0;

        for (int i = 0; i < _puzzleInput.Count; i+=3)
        {
            List<string> group = _puzzleInput.GetRange(i, 3);
            int badge = GetDuplicateBadgeForGroup(group);
            badgePriorityTotal += GetItemPriority(badge);
        }

        AnsiConsole.MarkupLine($"{Constants.PartTwo} The total badge priority is [green]{badgePriorityTotal}[/]");
    }

    private static int GetDuplicateItemForPack(string line)
    {
        string one = line[..(line.Length / 2)];
        string two = line[(line.Length / 2)..];

        return GetItemPriority(one.First(two.Contains));
    }

    private static int GetDuplicateBadgeForGroup(List<string> group) =>
        group[0].First(i =>
            group[1].Contains(i) &&
            group[2].Contains(i));

    private static int GetItemPriority(int item)
    {
        if (item <= 90)
        {
            return item - 38;
        }

        return item - 96;
    }
}
