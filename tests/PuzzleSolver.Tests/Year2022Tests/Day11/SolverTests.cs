namespace PuzzleSolver.Tests.Year2022Tests.Day11;

public class SolverTests
{
    private const string SampleData = @"";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day11.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partOne = puzzleSolver.SolvePartOne();
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(-1);
        partTwo.ShouldBe(-1);
    }
}
