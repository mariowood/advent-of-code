namespace PuzzleSolver.Tests.Year2022Tests.Day08;

public class SolverTests
{
    private const string SampleData = @"30373
25512
65332
33549
35390";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day08.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partOne = puzzleSolver.SolvePartOne();
        string partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(21);
        partTwo.ShouldBe("N/A");
    }
}
