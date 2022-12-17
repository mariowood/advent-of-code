namespace PuzzleSolver.Year2022.Day06;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/6.
/// </summary>
[PuzzleDescription(description: "Day 6: Tuning Trouble", 2022, 6)]
public sealed class Solver : SolverBase
{
    private string _dataStreamBuffer = null!;

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne() => GetPacketMarker(4);

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo() => GetPacketMarker(14);

    /// <inheritdoc/>
    public override void ProcessInput(List<string> input) => _dataStreamBuffer = input[0];

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();

        AddPartOneAnswer("Start-of-packet marker ends at character.", partOne);
        AddPartTwoAnswer("Start-of-message marker ends at character.", partTwo);
    }

    private int GetPacketMarker(int markerSize)
    {
        List<char> startOfPacketMarker = new();

        for (int streamIndex = 0; streamIndex < _dataStreamBuffer.Length; streamIndex++)
        {
            if (startOfPacketMarker.Count < markerSize)
            {
                startOfPacketMarker.Add(_dataStreamBuffer[streamIndex]);

                if (startOfPacketMarker.Count == markerSize)
                {
                    if (startOfPacketMarker.Distinct().Count() == markerSize)
                    {
                        return streamIndex + 1;
                    }

                    startOfPacketMarker.RemoveAt(0);
                }
            }
        }

        throw new InvalidOperationException(
            $"Packet marker not found in data stream buffer: {_dataStreamBuffer}");
    }
}
