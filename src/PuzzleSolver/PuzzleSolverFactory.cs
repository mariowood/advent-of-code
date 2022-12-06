namespace PuzzleSolver;

public static class PuzzleSolverFactory
{
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