namespace PuzzleSolver.Tests;

public class PuzzleSolverFactoryTests
{
    [Theory]
    [InlineData(2022, 1, typeof(Year2022.Day01.Solver))]
    [InlineData(2022, 2, typeof(Year2022.Day02.Solver))]
    [InlineData(2022, 3, typeof(Year2022.Day03.Solver))]
    [InlineData(2022, 4, typeof(Year2022.Day04.Solver))]
    [InlineData(2022, 5, typeof(Year2022.Day05.Solver))]
    [InlineData(2022, 6, typeof(Year2022.Day06.Solver))]
    [InlineData(2022, 7, typeof(Year2022.Day07.Solver))]
    public void Can_Get_Puzzle_Solver(int year, int day, Type expectedType)
    {
        // Arrange && Act
        SolverBase puzzleSolver = PuzzleSolverFactory.GetPuzzleSolver(year, day);

        // Assert
        puzzleSolver.ShouldBeOfType(expectedType);
    }

    [Theory]
    [InlineData(2022, 0)]
    [InlineData(2021, 1)]
    [InlineData(2022, 12)]
    [InlineData(int.MaxValue, int.MaxValue)]
    [InlineData(int.MinValue, int.MinValue)]
    public void Throws_InvalidOperationException_For_Invalid_Year_Day(int year, int day) =>

        // Arrange, Act, and Assert
        Assert.Throws<InvalidOperationException>(() => PuzzleSolverFactory.GetPuzzleSolver(year, day));
}
