using System.Text.RegularExpressions;
using Spectre.Console;

namespace PuzzleSolver.Year2022.Day05;

[PuzzleDescription("Day 5: Supply Stacks", 2022, 5)]
public class Solver : PuzzleSolver
{
    private List<List<char>> _crates = new();
    private List<List<int>> _moves = new();
    private readonly List<string> _crateLines = new();
    private readonly List<string> _moveLines = new();
    
    public override void ProcessInput(List<string> lines)
    {
        foreach (var line in lines)
        {
            if (line.StartsWith("move"))
            {
                _moveLines.Add(line);
            }
            else if (!string.IsNullOrWhiteSpace(line))
            {
                _crateLines.Add(line);
            }
        }

        ProcessMoveLines();        
        ProcessCrateLines();
    }

    public override void SolvePartOne()
    {
        ProcessMoves();
        string topCrates = string.Join("", _crates.Select(c => c[^1]));
        AnsiConsole.MarkupLine($"{Constants.PartOne} The top crates are [green]{topCrates}[/].");
    }

    public override void SolvePartTwo()
    {
        AnsiConsole.MarkupLine($"{Constants.PartTwo} Not implemented yet.");   
    }

    private void ProcessCrateLines()
    {
        _crateLines.Reverse();
        int numberOfColumns = _crateLines[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Length;

        for (int column = 0; column < numberOfColumns; column++)
        {
            _crates.Add(new List<char>());
        }

        for (int crateLine = 1; crateLine < _crateLines.Count; crateLine++)
        {
            for (int crateColumn = 0; crateColumn < numberOfColumns; crateColumn++)
            {
                char crate = _crateLines[crateLine][crateColumn * 3 + crateColumn + 1];
                if (crate != ' ')
                {
                    _crates[crateColumn].Add(crate);
                }
            }
        }
    }

    private void ProcessMoveLines()
    {
        const string pattern = @"\w+\s(\d+)\s\w+\s(\d+)\s\w+\s(\d+)";
        Regex reg = new Regex(pattern, RegexOptions.Compiled);
        
        for (int i = 0; i < _moveLines.Count; i++)
        {
            Match match = reg.Match(_moveLines[i]);
            
            _moves.Add(new List<int>());
            _moves[i].Add(int.Parse(match.Groups[1].Value));
            _moves[i].Add(int.Parse(match.Groups[2].Value));
            _moves[i].Add(int.Parse(match.Groups[3].Value));
        }
    }

    private void ProcessMoves()
    {
        for (int moveInstruction = 0; moveInstruction < _moves.Count; moveInstruction++)
        {
            for (int moveCount = 0; moveCount < _moves[moveInstruction][0]; moveCount++)
            {
                int moveFrom = _moves[moveInstruction][1] - 1;
                int moveTo = _moves[moveInstruction][2] - 1;

                char movingCrate = _crates[moveFrom][_crates[moveFrom].Count - 1];
                _crates[moveFrom].RemoveAt(_crates[moveFrom].Count - 1);
                _crates[moveTo].Add(movingCrate);
            }
        }
    }
}