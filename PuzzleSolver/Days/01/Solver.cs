namespace PuzzleSolver.Days._01;

public class Solver
{
    private readonly Dictionary<int, int> _caloriesPerElf = new();

    public Solver(List<string> input)
    {
        ProcessInput(input, 0);
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

    private void ProcessInput(List<string> input, int index)
    {
        int calories = 0;

        while (index < input.Count && !string.IsNullOrWhiteSpace(input[index]))
        {
            if (int.TryParse(input[index], out int lineCalories))
            {
                calories += lineCalories;
            }

            index++;
        }

        index++;

        int numberOfElves = _caloriesPerElf.Count;

        _caloriesPerElf[numberOfElves + 1] = calories;

        if (index < input.Count)
        {
            ProcessInput(input, index);
        }
    }
}