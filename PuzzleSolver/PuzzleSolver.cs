namespace PuzzleSolver;

public abstract class PuzzleSolver
{
    public abstract void ProcessInput(List<string> lines);
    
    public abstract void SolvePartOne();

    public abstract void SolvePartTwo();
}