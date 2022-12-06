namespace PuzzleSolver.Tests.Year2022Tests.Day02;

public class SolverTests
{
    private const string SampleData = @"A Y
B X
C Z";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day02.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partOne = puzzleSolver.SolvePartOne();
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(15);
        partTwo.ShouldBe(12);
    }
}
