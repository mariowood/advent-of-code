namespace PuzzleSolver;

[AttributeUsage(AttributeTargets.Class)]
public class PuzzleDescriptionAttribute : Attribute
{
    public string Description { get; }
    
    public int Year { get; }
    
    public int Day { get; }

    public PuzzleDescriptionAttribute(string description, int year, int day)
    {
        Description = description;
        Year = year;
        Day = day;
    }
}