namespace PuzzleSolver.Year2022.Day01;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/1.
/// </summary>
[PuzzleDescription("Day 1: Calorie Counting", 2022, 1)]
public sealed class Solver : SolverBase
{
    private readonly List<int> _caloriesPerElf = new();

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne() => _caloriesPerElf.Max();

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo() => _caloriesPerElf.OrderDescending().Take(3).Sum();

    /// <inheritdoc/>
    public override void ProcessInput(List<string> input)
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

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();

        AddPartOneAnswer("The most calories that an elf hold.", partOne);
        AddPartTwoAnswer("The total calories held by the top three elves.", partTwo);
    }
}
