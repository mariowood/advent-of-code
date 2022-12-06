using Spectre.Console;

namespace PuzzleSolver.Year2022.Day01;

[PuzzleDescription("Day 1: Calorie Counting", 2022, 1)]
public class Solver : PuzzleSolver
{
    private readonly List<int> _caloriesPerElf = new();

    protected override void ProcessInput(List<string> input)
    {
        int currentElfCalories = 0;

        foreach (string line in input)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                currentElfCalories += int.Parse(line);
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

    protected override void SolvePartOne()
    {
        int maxCalories = GetMaxCalories();
        AddPartOneAnswer("The most calories that an elf hold.", maxCalories);
    }

    protected override void SolvePartTwo()
    {
        int topThreeElvesTotalCalories = GetTopThreeElvesTotalCalories();
        AddPartTwoAnswer("The total calories held by the top three elves.", topThreeElvesTotalCalories);
    }

    private int GetTopThreeElvesTotalCalories() => _caloriesPerElf.OrderDescending().TakeLast(3).Sum();

    private int GetMaxCalories() => _caloriesPerElf.Max();
}
