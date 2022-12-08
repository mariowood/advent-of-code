namespace PuzzleSolver.Tests.Year2022Tests.Day08;

public class SolverTests
{
    private const string SampleData = @"";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day08.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        string partOne = puzzleSolver.SolvePartOne();
        string partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe("N/A");
        partTwo.ShouldBe("N/A");
    }
}
