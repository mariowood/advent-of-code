namespace PuzzleSolver;

/// <summary>
/// A class to find the <see cref="PuzzleSolver"/> for a given year and day.
/// </summary>
public static class PuzzleSolverFactory
{
    /// <summary>
    /// Gets the <see cref="PuzzleSolver"/> for a specified year and day.
    /// Throws an exception if the puzzle cannot be found.
    /// </summary>
    /// <param name="year">The puzzle year to find the <see cref="PuzzleSolver"/> for.</param>
    /// <param name="day">The puzzle day to find the <see cref="PuzzleSolver"/> for.</param>
    /// <returns>A <see cref="PuzzleSolver"/> for the specified year and day.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a <see cref="PuzzleSolver"/> cannot be
    /// found for the specified year and day.</exception>
    public static PuzzleSolver GetPuzzleSolver(int year, int day)
    {
        var puzzleSolverTypes = typeof(Program).Assembly.GetTypes();
        var puzzleSolverType = puzzleSolverTypes
            .SingleOrDefault(t => t.FullName == $"PuzzleSolver.Year{year}.Day{day:00}.Solver");

        if (puzzleSolverType != null && Activator.CreateInstance(puzzleSolverType) is PuzzleSolver solver)
        {
            return solver;
        }

        throw new InvalidOperationException("Unable to find Puzzle Solver for year: {year}, day: {day}");
    }
}
