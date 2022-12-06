using System.Text.RegularExpressions;

namespace PuzzleSolver.Year2022.Day05;

[PuzzleDescription("Day 5: Supply Stacks", 2022, 5)]
public class Solver : PuzzleSolver
{
    private readonly List<List<char>> _cratesPt1 = new();
    private readonly List<List<char>> _cratesPt2 = new();
    private readonly List<List<int>> _moves = new();
    private readonly List<string> _crateLines = new();
    private readonly List<string> _moveLines = new();

    protected override void ProcessInput(List<string> lines)
    {
        foreach (string line in lines)
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

    protected override void SolvePartOne()
    {
        ProcessMoves();
        string topCrates = string.Join("", _cratesPt1.Select(c => c[^1]));
        AddPartOneAnswer("The top crates after being moved one at a time.",topCrates);
    }

    protected override void SolvePartTwo()
    {
        ProcessMovesInStacks();
        string topCrates = string.Join("", _cratesPt2.Select(c => c[^1]));
        AddPartTwoAnswer("The top crates after being moved in stacks.", topCrates);
    }

    private void ProcessCrateLines()
    {
        _crateLines.Reverse();
        int numberOfColumns = _crateLines[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Length;

        for (int column = 0; column < numberOfColumns; column++)
        {
            _cratesPt1.Add(new List<char>());
            _cratesPt2.Add(new List<char>());
        }

        for (int crateLine = 1; crateLine < _crateLines.Count; crateLine++)
        {
            for (int crateColumn = 0; crateColumn < numberOfColumns; crateColumn++)
            {
                char crate = _crateLines[crateLine][(crateColumn * 3) + crateColumn + 1];
                if (crate != ' ')
                {
                    _cratesPt1[crateColumn].Add(crate);
                    _cratesPt2[crateColumn].Add(crate);
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
        foreach (List<int> move in _moves)
        {
            for (int moveCount = 0; moveCount < move[0]; moveCount++)
            {
                int moveFrom = move[1] - 1;
                int moveTo = move[2] - 1;

                char movingCrate = _cratesPt1[moveFrom][_cratesPt1[moveFrom].Count - 1];
                _cratesPt1[moveFrom].RemoveAt(_cratesPt1[moveFrom].Count - 1);
                _cratesPt1[moveTo].Add(movingCrate);
            }
        }
    }

    private void ProcessMovesInStacks()
    {
        foreach (List<int> move in _moves)
        {
            int take = move[0];
            int moveFrom = move[1] - 1;
            int moveTo = move[2] - 1;
            List<char> movingCrates = _cratesPt2[moveFrom].TakeLast(take).ToList();
            _cratesPt2[moveFrom].RemoveRange(_cratesPt2[moveFrom].Count - take, take);
            _cratesPt2[moveTo].AddRange(movingCrates);
        }
    }
}
