namespace PuzzleSolver.Tests.Year2022Tests.Day09;

public class SolverTests
{
    private const string SampleDataPartOne = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

    private const string SampleDataPartTwo = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

    [Fact]
    public void Solves_With_Sample_Data_For_Part_One()
    {
        // Arrange
        Year2022.Day09.Solver puzzleSolver = new();
        List<string> input = SampleDataPartOne.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partOne = puzzleSolver.SolvePartOne();

        // Assert
        partOne.ShouldBe(13);
    }

    [Fact]
    public void Solves_With_Sample_Data_For_Part_Two()
    {
        // Arrange
        Year2022.Day09.Solver puzzleSolver = new();
        List<string> input = SampleDataPartTwo.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partTwo.ShouldBe(36);
    }
}
