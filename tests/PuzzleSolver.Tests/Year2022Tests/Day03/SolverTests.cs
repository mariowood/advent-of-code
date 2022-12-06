namespace PuzzleSolver.Tests.Year2022Tests.Day03;

public class SolverTests
{
    private const string SampleData = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day03.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partOne = puzzleSolver.SolvePartOne();
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(157);
        partTwo.ShouldBe(70);
    }
}
