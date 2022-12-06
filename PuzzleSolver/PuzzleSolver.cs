using Spectre.Console;

namespace PuzzleSolver;

public abstract class PuzzleSolver
{
    private const string Part = "Part";
    private const string Description = "Desciption";
    private const string Answer = "Answer";
    protected const string PartOne = "[bold blue]One[/]";
    protected const string PartTwo = "[bold purple]Two[/]";
    protected Table Table = new();

    public void SolveForInput(List<string> lines)
    {
        Table.AddColumn($"[gold3_1]{Part}[/]");
        Table.AddColumn(new TableColumn($"[gold3_1]{Description}[/]").Centered());
        Table.AddColumn($"[gold3_1]{Answer}[/]");

        ProcessInput(lines);
        SolvePartOne();
        SolvePartTwo();

        AnsiConsole.Write(Table);
    }

    protected abstract void ProcessInput(List<string> lines);

    protected abstract void SolvePartOne();

    protected abstract void SolvePartTwo();

    protected void AddPartOneAnswer<T>(string description, T answer) =>
        Table.AddRow($"{PartOne}", description, $"[bold green]{answer}[/]");

    protected void AddPartTwoAnswer<T>(string description, T answer) =>
        Table.AddRow($"{PartTwo}", description, $"[bold green]{answer}[/]");
}
