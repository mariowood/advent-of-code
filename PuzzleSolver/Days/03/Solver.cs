namespace PuzzleSolver.Days._03;

public static class Solver
{
    public static int GetItemPriorityTotals(List<string> input)
    {
        return input.Select(GetDuplicateItemForPack).Sum();
    }

    public static int GetBadgePriorityTotals(List<string> input)
    {
        if (input.Count % 3 != 0)
        {
            throw new ArgumentException();
        }

        var priorityTotal = 0;
        
        for (int i = 0; i < input.Count; i+=3)
        {
            var group = input.GetRange(i, 3);
            var badge = GetDuplicateBadgeForGroup(group);
            priorityTotal += GetItemPriority(badge);
        }

        return priorityTotal;
    }

    private static int GetDuplicateItemForPack(string line)
    {
        string one = line[..(line.Length / 2)];
        string two = line[(line.Length / 2)..];
        
        return GetItemPriority(one.First(two.Contains));
    }

    private static int GetDuplicateBadgeForGroup(List<string> group)
    {
        return group[0].First(i => 
            group[1].Contains(i) && 
            group[2].Contains(i));
    }
    
    private static int GetItemPriority(int item)
    {
        if (item <= 90)
        {
            return item - 38;
        }

        return item - 96;
    }
}