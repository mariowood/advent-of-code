namespace PuzzleSolver.Tests.Year2022Tests.Day01;

public class SolverTests
{
    private const string SampleData = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day01.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.SolveForInput(input);
        int partOne = puzzleSolver.SolvePartOne();
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(24000);
        partTwo.ShouldBe(45000);
    }
}
