using Spectre.Console;

namespace PuzzleSolver.Year2022.Day01;

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
        AnsiConsole.MarkupLine($"[bold blue]Pt. 1:[/] The elf with the most calories has [green]{maxCalories} calories[/]");
    }

    public override void SolvePartTwo()
    {
        int topThreeElvesTotalCalories = GetTopThreeElvesTotalCalories();
        AnsiConsole.MarkupLine($"[bold purple]Pt. 2:[/] The top 3 elves have a total of [green]{topThreeElvesTotalCalories} calories.[/]");
    }
    
    private int GetTopThreeElvesTotalCalories()
    {
        return _caloriesPerElf.OrderDescending().TakeLast(3).Sum();
    }
    
    private int GetMaxCalories()
    {
        return _caloriesPerElf.Max();
    }
}