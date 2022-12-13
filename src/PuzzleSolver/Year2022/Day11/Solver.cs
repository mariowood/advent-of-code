namespace PuzzleSolver.Year2022.Day11;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/11.
/// </summary>
[PuzzleDescription(description: "Day 10: Cathode-Ray Tube", 2022, 11)]
public class Solver : PuzzleSolver
{
    private readonly List<string> _puzzleInput = new();

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne()
    {
        int result = -1;
        return result;
    }

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo()
    {
        int result = -1;
        return result;
    }

    /// <inheritdoc/>
    public override void ProcessInput(List<string> lines) => _puzzleInput.AddRange(lines);

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();

        AddPartOneAnswer("Not Implemented.", partOne);
        AddPartTwoAnswer("Not Implemented.", partTwo);
    }
}
