using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace PuzzleSolver;

/// <inheritdoc />
internal sealed class PuzzleCommand : Command<PuzzleCommand.Settings>
{
    /// <inheritdoc />
    public sealed class Settings : CommandSettings
    {
        /// <summary>
        /// Gets the advent of code year to solve puzzles for.
        /// </summary>
        [Description("The advent of code year to solve puzzles for.")]
        [CommandArgument(0, "[year]")]
        public int? Year { get; init; }

        /// <summary>
        /// Gets The advent of code day to solve puzzles for.
        /// </summary>
        [Description("The advent of code day to solve puzzles for.")]
        [CommandArgument(2, "[day]")]
        public int? Day { get; init; }
    }

    /// <inheritdoc/>
    public override int Execute(
        [NotNull] CommandContext context,
        [NotNull] Settings settings)
    {
        int year = settings.Year ?? DateTime.UtcNow.Year;
        int day = settings.Day ?? DateTime.UtcNow.Day;

        try
        {
            SolverBase puzzleSolver = PuzzleSolverFactory.GetPuzzleSolver(year, day);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold gold3_1]Puzzle:[/] [green]{GetPuzzleDescription(puzzleSolver)}[/]");
            AnsiConsole.WriteLine();
            puzzleSolver.SolveForInput(GetPuzzleInput(year, day));
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return -1;
        }

        return 0;
    }

    private static string GetPuzzleDescription(SolverBase puzzleSolver) =>
        puzzleSolver.GetType().GetCustomAttribute<PuzzleDescriptionAttribute>()
            is { } puzzleDescriptionAttribute ?
            puzzleDescriptionAttribute.Description.Trim('"') :
            "Unknown";

    private static List<string> GetPuzzleInput(int year, int day) =>
        File.ReadLines($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}/Year{year}/Day{day:00}/input.txt")
            .ToList();
}
