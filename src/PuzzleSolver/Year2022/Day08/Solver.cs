namespace PuzzleSolver.Year2022.Day08;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/8.
/// </summary>
[PuzzleDescription(description: "Day 8: Treetop Tree House", 2022, 8)]
public sealed class Solver : PuzzleSolver
{
    private Tree[][] _trees = null!;

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne()
    {
        int[] highestInColTopDown = new int[_trees[0].Length];
        int[] highestInRowFromLeft = new int[_trees.Length];
        int[] highestInColBottomUp = new int[_trees[0].Length];
        int[] highestInRowFromRight = new int[_trees.Length];

        // Check top row down.
        for (int row = 0; row < _trees.Length; row++)
        {
            for (int col = 0; col < _trees[row].Length; col++)
            {
                // row 0 always visible from top.
                if (row == 0)
                {
                    highestInColTopDown[col] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                    continue;
                }

                // Check if visible from top.
                if (_trees[row][col].Height > highestInColTopDown[col])
                {
                    highestInColTopDown[col] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                }
            }
        }

        // check from left to right.
        for (int row = 0; row < _trees.Length; row++)
        {
            for (int col = 0; col < _trees[row].Length; col++)
            {
                // col 0 always visible.
                if (col == 0)
                {
                    highestInRowFromLeft[row] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                    continue;
                }

                // check if visible from the left.
                if (_trees[row][col].Height > highestInRowFromLeft[row])
                {
                    highestInRowFromLeft[row] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                }
            }
        }

        // check from bottom up.
        for (int row = _trees.Length - 1; row >= 0; row--)
        {
            for (int col = _trees[row].Length - 1; col >= 0; col--)
            {
                // last row always visible.
                if (row == _trees.Length - 1)
                {
                    highestInColBottomUp[col] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                    continue;
                }

                // check if visible from the bottom.
                if (_trees[row][col].Height > highestInColBottomUp[col])
                {
                    highestInColBottomUp[col] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                }
            }
        }

        // check from right to left.
        for (int row = _trees.Length - 1; row >= 0; row--)
        {
            for (int col = _trees[row].Length - 1; col >= 0; col--)
            {
                // right column always visible
                if (col == _trees[row].Length - 1)
                {
                    highestInRowFromRight[row] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                    continue;
                }

                // check if visible from the right.
                if (_trees[row][col].Height > highestInRowFromRight[row])
                {
                    highestInRowFromRight[row] = _trees[row][col].Height;
                    _trees[row][col].Visible = true;
                }
            }
        }

        var result = _trees
            .Select(row => row.Where(tree => tree.Visible))
            .SelectMany(t => t);
        return result.Count();
    }

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo()
    {
        for (int row = 0; row < _trees.Length; row++)
        {
            for (int col = 0; col < _trees[row].Length; col++)
            {
                int treesVisibleAbove = GetTreesVisibleAbove(row, col);
                int treesVisibleBelow = GetTreesVisibleBelow(row, col);
                int treesVisibleRight = GetTreesVisibleRight(row, col);
                int treesVisibleLeft = GetTreesVisibleLeft(row, col);

                if (row == 0 || col == 0 || row == _trees.Length - 1 || col == _trees[row].Length - 1)
                {
                    _trees[row][col].ScenicScore = 0;
                    continue;
                }

                int scenicScore = treesVisibleAbove * treesVisibleBelow * treesVisibleRight * treesVisibleLeft;

                _trees[row][col].ScenicScore = scenicScore;
            }
        }

        return _trees
            .SelectMany(t => t)
            .Max(t => t.ScenicScore);
    }

    /// <inheritdoc/>
    public override void ProcessInput(List<string> lines)
    {
        _trees = new Tree[lines.Count][];

        for (int line = 0; line < lines.Count; line++)
        {
            _trees[line] = lines[line].Select(c => new Tree { Height = c - 48 }).ToArray();
        }
    }

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();

        AddPartOneAnswer("The number of trees visible from any edge.", partOne);
        AddPartTwoAnswer("The highest scenic score possible for any tree.", partTwo);
    }

    private int GetTreesVisibleAbove(int row, int col)
    {
        int treeHeight = _trees[row][col].Height;
        int treesVisible = 0;

        for (int i = row - 1; i >= 0; i--)
        {
            treesVisible++;

            if (_trees[i][col].Height >= treeHeight)
            {
                break;
            }
        }

        return treesVisible;
    }

    private int GetTreesVisibleBelow(int row, int col)
    {
        int treeHeight = _trees[row][col].Height;
        int treesVisible = 0;

        for (int i = row + 1; i < _trees.Length; i++)
        {
            treesVisible++;

            if (_trees[i][col].Height >= treeHeight)
            {
                break;
            }
        }

        return treesVisible;
    }

    private int GetTreesVisibleLeft(int row, int col)
    {
        int treeHeight = _trees[row][col].Height;
        int treesVisible = 0;

        for (int i = col - 1; i >= 0; i--)
        {
            treesVisible++;

            if (_trees[row][i].Height >= treeHeight)
            {
                break;
            }
        }

        return treesVisible;
    }

    private int GetTreesVisibleRight(int row, int col)
    {
        int treeHeight = _trees[row][col].Height;
        int treesVisible = 0;

        for (int i = col + 1; i < _trees[row].Length; i++)
        {
            treesVisible++;

            if (_trees[row][i].Height >= treeHeight)
            {
                break;
            }
        }

        return treesVisible;
    }

    private class Tree
    {
        public bool Visible { get; set; }

        public int Height { get; init; }

        public int ScenicScore { get; set; }
    }
}
