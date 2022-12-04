using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace PuzzleSolver;

internal sealed class PuzzleCommand : Command<PuzzleCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("The advent of code year to solve puzzles for.")]
        [CommandArgument(0, "[year]")]
        public int? Year { get; init; }
        
        [Description("The advent of code day to solve puzzles for.")]
        [CommandArgument(2, "[day]")]
        public int? Day { get; init; }
    }

    public override int Execute(
        [NotNull] CommandContext context, 
        [NotNull] Settings settings)
    {
        int year = settings.Year ?? DateTime.UtcNow.Year;
        int day = settings.Day ?? DateTime.UtcNow.Day;

        try
        {
            AnsiConsole.MarkupLine($":christmas_tree::star::santa_claus: Solving puzzle for {day:00} December {year} :santa_claus::star::christmas_tree:");
            PuzzleSolver puzzleSolver = PuzzleSolverFactory.GetPuzzleSolver(year, day);
            Type puzzleType = puzzleSolver.GetType();
            string puzzleDescription = puzzleType.GetCustomAttribute<PuzzleDescriptionAttribute>() 
                is { } puzzleDescriptionAttribute ?
                puzzleDescriptionAttribute.Description.Trim('"') :
                "Unknown";
            AnsiConsole.MarkupLine($"{Constants.Puzzle} [green]{puzzleDescription}[/]");
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            List<string> puzzleInput = File.ReadLines($"{currentDirectory}/Year{year}/Day{day:00}/input.txt").ToList();

            puzzleSolver.ProcessInput(puzzleInput);
            puzzleSolver.SolvePartOne();
            puzzleSolver.SolvePartTwo();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An exception was thrown attempting to solve puzzle for year: {year}, day: {day}.[/]");
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
            AnsiConsole.MarkupLine($"[red]Stack Trace: {ex.StackTrace}[/]");
            return -1;
        }

        return 0;
    }
}