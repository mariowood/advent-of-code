using Solver = PuzzleSolver.Days._01.Solver;

Console.WriteLine("Please enter the advent of code day (a number between 1 and 24 inclusive).");
string? day = Console.ReadLine();

if (int.TryParse(day, out int puzzleDay))
{
    string inputFile = $"Days/{puzzleDay:D2}/input.txt";
    List<string> lines = File.ReadLines(inputFile).ToList();
    
    switch (puzzleDay)
    {
        case 1:
            var solver = new Solver(lines);
            KeyValuePair<int, int> elf = solver.GetElfWithMostCalories();
            int topThreeElvesTotalCalories = solver.GetTopThreeElvesTotalCalories();
            Console.WriteLine($"The elf with the most calories is {elf.Key} with {elf.Value} calories.");
            Console.WriteLine($"The top 3 elves have a total of {topThreeElvesTotalCalories} calories.");
            break;
        case 2:
            int totalScore = PuzzleSolver.Days._02.Solver.GetTotalScore(lines);
            int totalScoreForTargetResult = PuzzleSolver.Days._02.Solver.GetTotalScoreForTargetResult(lines);
            Console.WriteLine($"The total score is {totalScore}");
            Console.WriteLine($"The total score using target result strategy is {totalScoreForTargetResult}");
            break;
        case 3:
            int totalItemPriority = PuzzleSolver.Days._03.Solver.GetItemPriorityTotals(lines);
            int totalBadgePriority = PuzzleSolver.Days._03.Solver.GetBadgePriorityTotals(lines);
            Console.WriteLine($"The total item priority is {totalItemPriority}");
            Console.WriteLine($"The total badge priority is {totalBadgePriority}");
            break;
        default:
            Console.WriteLine("Unrecognized puzzle day.");
            break;
    }
}
else
{
    Console.WriteLine("Invalid advent of code day, please enter a number between 1 and 24 inclusive.");
}

Console.WriteLine("Press any key to continue...");
Console.ReadKey();