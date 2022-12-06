namespace PuzzleSolver;

/// <summary>
/// An attribute describing an advent of code puzzle being solved.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class PuzzleDescriptionAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PuzzleDescriptionAttribute"/> class.
    /// </summary>
    /// <param name="description">The advent of code puzzle description.</param>
    /// <param name="year">The year the puzzle is from.</param>
    /// <param name="day">The day the puzzle is from.</param>
    public PuzzleDescriptionAttribute(string description, int year, int day)
    {
        Description = description;
        Year = year;
        Day = day;
    }

    /// <summary>
    /// Gets the description of the Advent of Code puzzle.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the year the puzzle is from.
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Gets the day the puzzle is from.
    /// </summary>
    public int Day { get; }
}
