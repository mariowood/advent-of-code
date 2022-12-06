namespace PuzzleSolver.Tests.Year2022Tests.Day06;

public class SolverTests
{
    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7, 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5, 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6, 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10, 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11, 26)]
    public void Solves_With_Sample_Data(string input, int answerOne, int answerTwo)
    {
        // Arrange
        Year2022.Day06.Solver puzzleSolver = new();

        // Act
        puzzleSolver.ProcessInput(new List<string> { input });
        int partOne = puzzleSolver.SolvePartOne();
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(answerOne);
        partTwo.ShouldBe(answerTwo);
    }
}
