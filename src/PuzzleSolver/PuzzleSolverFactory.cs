namespace PuzzleSolver;

/// <summary>
/// A class to find the <see cref="SolverBase"/> for a given year and day.
/// </summary>
public static class PuzzleSolverFactory
{
    /// <summary>
    /// Gets the <see cref="SolverBase"/> for a specified year and day.
    /// Throws an exception if the puzzle cannot be found.
    /// </summary>
    /// <param name="year">The puzzle year to find the <see cref="SolverBase"/> for.</param>
    /// <param name="day">The puzzle day to find the <see cref="SolverBase"/> for.</param>
    /// <returns>A <see cref="SolverBase"/> for the specified year and day.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a <see cref="SolverBase"/> cannot be
    /// found for the specified year and day.</exception>
    public static SolverBase GetPuzzleSolver(int year, int day)
    {
        var puzzleSolverTypes = typeof(Program).Assembly.GetTypes();
        var puzzleSolverType = puzzleSolverTypes
            .SingleOrDefault(t => t.FullName == $"PuzzleSolver.Year{year}.Day{day:00}.Solver");

        if (puzzleSolverType != null && Activator.CreateInstance(puzzleSolverType) is SolverBase solver)
        {
            return solver;
        }

        throw new InvalidOperationException("Unable to find Puzzle Solver for year: {year}, day: {day}");
    }
}
