using Spectre.Console;

namespace PuzzleSolver.Year2022.Day01;

[PuzzleDescription("Day 1: Calorie Counting", 2022, 1)]
public class Solver : PuzzleSolver
{
    private readonly List<int> _caloriesPerElf = new();

    public override void ProcessInput(List<string> input)
    {
        int currentElfCalories = 0;

        for (int i = 0; i < input.Count; i++)
        {
            if (!string.IsNullOrWhiteSpace(input[i]))
            {
                currentElfCalories += int.Parse(input[i]);
            }
            else
            {
                _caloriesPerElf.Add(currentElfCalories);
                currentElfCalories = 0;
            }
        }

        if (currentElfCalories > 0)
        {
            _caloriesPerElf.Add(currentElfCalories);
        }
    }

    public override void SolvePartOne()
    {
        int maxCalories = GetMaxCalories();
        AnsiConsole.MarkupLine($"{Constants.PartOne} The elf with the most calories has [green]{maxCalories} calories[/]");
    }

    public override void SolvePartTwo()
    {
        int topThreeElvesTotalCalories = GetTopThreeElvesTotalCalories();
        AnsiConsole.MarkupLine($"{Constants.PartTwo} The top 3 elves have a total of [green]{topThreeElvesTotalCalories} calories.[/]");
    }

    private int GetTopThreeElvesTotalCalories() => _caloriesPerElf.OrderDescending().TakeLast(3).Sum();

    private int GetMaxCalories() => _caloriesPerElf.Max();
}
