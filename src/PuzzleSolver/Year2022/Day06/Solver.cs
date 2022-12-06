namespace PuzzleSolver.Year2022.Day06;

/// <inheritdoc />
[PuzzleDescription(description: "Day 6: Tuning Trouble", 2022, 6)]
public class Solver : PuzzleSolver
{
    private string _dataStreamBuffer = null!;

    /// <inheritdoc/>
    protected override void ProcessInput(List<string> lines) => _dataStreamBuffer = lines[0];

    /// <inheritdoc/>
    protected override void SolvePartOne()
    {
        int startOfPacketMarker = GetPacketMarker(4);
        AddPartOneAnswer("Start-of-packet marker ends at character.", startOfPacketMarker);
    }

    /// <inheritdoc/>
    protected override void SolvePartTwo()
    {
        int startOfMessageMarker = GetPacketMarker(14);
        AddPartTwoAnswer("Start-of-message marker ends at character.", startOfMessageMarker);
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
