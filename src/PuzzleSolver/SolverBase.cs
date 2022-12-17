using Spectre.Console;

namespace PuzzleSolver;

/// <summary>
/// An abstract representation of a class which solves puzzles.
/// </summary>
public abstract class SolverBase
{
    private const string PartOne = "[bold blue]One[/]";
    private const string PartTwo = "[bold purple]Two[/]";
    private const string Part = "Part";
    private const string Description = "Desciption";
    private const string Answer = "Answer";
    private readonly Table _table = new();

    /// <summary>
    /// Solves a puzzle for the given input.
    /// </summary>
    /// <param name="input">The lines read from the puzzle input.txt file.</param>
    public void SolveForInput(List<string> input)
    {
        _table.AddColumn($"[gold3_1]{Part}[/]");
        _table.AddColumn(new TableColumn($"[gold3_1]{Description}[/]").Centered());
        _table.AddColumn($"[gold3_1]{Answer}[/]");

        ProcessInput(input);
        SolvePuzzles();

        AnsiConsole.Write(_table);

        ShowAdditionalOutput();
    }

    /// <summary>
    /// Process the given input.
    /// </summary>
    /// <param name="input">The lines read from the puzzle input.txt file.</param>
    public abstract void ProcessInput(List<string> input);

    /// <summary>
    /// Solve the puzzles for the day.
    /// </summary>
    protected abstract void SolvePuzzles();

    /// <summary>
    /// Adds the part one puzzle solution description and answer to the output table.
    /// </summary>
    /// <param name="description">The description of the part one solution.</param>
    /// <param name="answer">The answer for part one.</param>
    /// <typeparam name="T">The type of the answer.</typeparam>
    protected void AddPartOneAnswer<T>(string description, T answer) =>
        _table.AddRow($"{PartOne}", description, $"[bold green]{answer}[/]");

    /// <summary>
    /// Adds the part two puzzle solution description and answer to the output table.
    /// </summary>
    /// <param name="description">The description of the part two solution.</param>
    /// <param name="answer">The answer for part two.</param>
    /// /// <typeparam name="T">The type of the answer.</typeparam>
    protected void AddPartTwoAnswer<T>(string description, T answer) =>
        _table.AddRow($"{PartTwo}", description, $"[bold green]{answer}[/]");

    /// <summary>
    /// Can be used to show additional output from a puzzle after the table.
    /// </summary>
    protected virtual void ShowAdditionalOutput()
    {
    }
}
