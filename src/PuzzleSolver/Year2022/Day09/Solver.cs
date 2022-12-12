using System.Drawing;

namespace PuzzleSolver.Year2022.Day09;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/9.
/// </summary>
[PuzzleDescription(description: "Day 9: Rope Bridge", 2022, 9)]
public sealed class Solver : PuzzleSolver
{
    private readonly List<string> _puzzleInput = new();

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne()
    {
        List<List<Point>> pointsVisited = new();

        ProcessMoves(2, pointsVisited);

        int uniqueTailPointsVisited = pointsVisited.Select(p => p.Last()).Distinct().Count();
        return uniqueTailPointsVisited;
    }

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo()
    {
        List<List<Point>> pointsVisited = new();

        ProcessMoves(10, pointsVisited);

        int uniqueTailPointsVisited = pointsVisited.Select(p => p.Last()).Distinct().Count();
        return uniqueTailPointsVisited;
    }

    /// <inheritdoc/>
    public override void ProcessInput(List<string> lines) => _puzzleInput.AddRange(lines);

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();

        AddPartOneAnswer("Positions visited by the tail at least once when rope length is two.", partOne);
        AddPartTwoAnswer("Positions visited by the tail at least once when rope length is ten.", partTwo);
    }

    private void ProcessMoves(int ropeLength, List<List<Point>> pointsVisited)
    {
        pointsVisited.Add(new List<Point>());

        for (int i = 0; i < ropeLength; i++)
        {
            pointsVisited[0].Add(new Point { X = 0, Y = 0 });
        }

        foreach (string move in _puzzleInput)
        {
            string[] moveParts = move.Split(' ');
            string direction = moveParts[0];
            int moves = int.Parse(moveParts[1]);

            for (int i = 1; i <= moves; i++)
            {
                List<Point> lastMoves = pointsVisited.Last();
                List<Point> newMoves = new();

                switch (direction)
                {
                    case "U":
                        for (int knot = 0; knot < ropeLength - 1; knot++)
                        {
                            Point headUp;
                            if (newMoves.Count == 0)
                            {
                                headUp = new Point { X = lastMoves[knot].X, Y = lastMoves[knot].Y + 1 };
                                newMoves.Add(headUp);
                            }
                            else
                            {
                                headUp = newMoves.Last();
                            }

                            newMoves.Add(GetNewTail(headUp, lastMoves[knot + 1]));
                        }

                        break;
                    case "D":
                        for (int knot = 0; knot < ropeLength - 1; knot++)
                        {
                            Point headDown;
                            if (newMoves.Count == 0)
                            {
                                headDown = new Point { X = lastMoves[knot].X, Y = lastMoves[knot].Y - 1 };
                                newMoves.Add(headDown);
                            }
                            else
                            {
                                headDown = newMoves.Last();
                            }

                            newMoves.Add(GetNewTail(headDown, lastMoves[knot + 1]));
                        }

                        break;
                    case "L":
                        for (int knot = 0; knot < ropeLength - 1; knot++)
                        {
                            Point headLeft;
                            if (newMoves.Count == 0)
                            {
                                headLeft = new Point { X = lastMoves[knot].X - 1, Y = lastMoves[knot].Y };
                                newMoves.Add(headLeft);
                            }
                            else
                            {
                                headLeft = newMoves.Last();
                            }

                            newMoves.Add(GetNewTail(headLeft, lastMoves[knot + 1]));
                        }

                        break;
                    case "R":
                        for (int knot = 0; knot < ropeLength - 1; knot++)
                        {
                            Point headRight;
                            if (newMoves.Count == 0)
                            {
                                headRight = new Point { X = lastMoves[knot].X + 1, Y = lastMoves[knot].Y };
                                newMoves.Add(headRight);
                            }
                            else
                            {
                                headRight = newMoves.Last();
                            }

                            newMoves.Add(GetNewTail(headRight, lastMoves[knot + 1]));
                        }

                        break;
                    default:
                        throw new InvalidOperationException($"Invalid direction: {direction}");
                }

                pointsVisited.Add(newMoves);
            }
        }
    }

    private Point GetNewTail(Point head, Point oldTail)
    {
        int newX = 0;
        int newY = 0;

        if (oldTail.X + 2 == head.X && head.Y - 1 <= oldTail.Y && oldTail.Y <= head.Y + 1)
        {
            // tail is two to the left of head and tail is within one up or down of head
            newX = head.X - 1;
            newY = head.Y;
        }
        else if (oldTail.X + 2 == head.X && oldTail.Y + 2 == head.Y)
        {
            // tail is two to the left of head and tail is two below head
            newX = head.X - 1;
            newY = head.Y - 1;
        }
        else if (oldTail.X + 2 == head.X && oldTail.Y - 2 == head.Y)
        {
            // tail is two to the left of head and tail is two above head
            newX = head.X - 1;
            newY = head.Y + 1;
        }
        else if (oldTail.X - 2 == head.X && head.Y - 1 <= oldTail.Y && oldTail.Y <= head.Y + 1)
        {
            // tail is two to the right of head and tail is within one up or down of head
            newX = head.X + 1;
            newY = head.Y;
        }
        else if (oldTail.X - 2 == head.X && oldTail.Y + 2 == head.Y)
        {
            // tail is two to the right of head and tail is two below head
            newX = head.X + 1;
            newY = head.Y - 1;
        }
        else if (oldTail.X - 1 > head.X && oldTail.Y - 2 == head.Y)
        {
            // tail is two to the right of head and tail is two below head
            newX = head.X + 1;
            newY = head.Y + 1;
        }
        else if (oldTail.Y + 2 == head.Y && head.X - 1 <= oldTail.X && oldTail.X <= head.X + 1)
        {
            // tail is two below the head and tail is within one left of right of head
            newX = head.X;
            newY = head.Y - 1;
        }
        else if (oldTail.Y - 2 == head.Y && head.X - 1 <= oldTail.X && oldTail.X <= head.X + 1)
        {
            // tail is two above the head and tail is within one left of right of head
            newX = head.X;
            newY = head.Y + 1;
        }
        else
        {
            newX = oldTail.X;
            newY = oldTail.Y;
        }

        return new Point { X = newX, Y = newY };
    }

    private struct Point
    {
        public int X { get; init; }

        public int Y { get; init; }
    }
}
