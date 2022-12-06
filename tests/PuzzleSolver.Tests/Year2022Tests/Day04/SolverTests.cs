namespace PuzzleSolver.Tests.Year2022Tests.Day04;

public class SolverTests
{
    private const string SampleData = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day04.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partOne = puzzleSolver.SolvePartOne();
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(2);
        partTwo.ShouldBe(4);
    }
}
