namespace PuzzleSolver.Days;

public class December1st
{
    private readonly Dictionary<int, int> _caloriesPerElf = new();

    public December1st(List<string> lines)
    {
        GetCaloriesForElf(lines, 0);
    }

    public int GetTopThreeElvesTotalCalories()
    {
        var elfList = _caloriesPerElf.ToList();
        elfList.Sort((x, y) => x.Value.CompareTo(y.Value));
        var topThree = elfList.TakeLast(3);
        return topThree.Sum(elf => elf.Value);
    }
    
    public KeyValuePair<int, int> GetElfWithMostCalories()
    {
        int maxCalories = _caloriesPerElf.Values.Max();
        KeyValuePair<int, int> elf = _caloriesPerElf.FirstOrDefault(e => e.Value == maxCalories);
        return elf;
    }

    private void GetCaloriesForElf(List<string> lines, int index)
    {
        int calories = 0;

        while (index < lines.Count && !string.IsNullOrWhiteSpace(lines[index]))
        {
            if (int.TryParse(lines[index], out int lineCalories))
            {
                calories += lineCalories;
            }

            index++;
        }

        index++;

        int numberOfElves = _caloriesPerElf.Count;

        _caloriesPerElf[numberOfElves + 1] = calories;

        if (index < lines.Count)
        {
            GetCaloriesForElf(lines, index);
        }
    }
}