namespace PuzzleSolver.Tests.Year2022Tests.Day05;

public class SolverTests
{
    private const string SampleData = "    [D]    \r\n" +
                                      "[N] [C]    \r\n" +
                                      "[Z] [M] [P]\r\n" +
                                      " 1   2   3 \r\n" +
                                      "\r\n" +
                                      "move 1 from 2 to 1\r\n" +
                                      "move 3 from 1 to 3\r\n" +
                                      "move 2 from 2 to 1\r\n" +
                                      "move 1 from 1 to 2";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day05.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        string partOne = puzzleSolver.SolvePartOne();
        string partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe("CMZ");
        partTwo.ShouldBe("MCD");
    }
}
