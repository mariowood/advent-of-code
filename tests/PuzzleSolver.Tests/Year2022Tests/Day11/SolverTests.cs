namespace PuzzleSolver.Tests.Year2022Tests.Day11;

public class SolverTests
{
    private const string SampleData = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day11.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);

        int partOne = puzzleSolver.SolvePartOne();
        long partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(10605);
        partTwo.ShouldBe(2713310158);
    }
}
