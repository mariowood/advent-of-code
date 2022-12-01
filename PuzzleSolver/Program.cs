using PuzzleSolver.Days;

Console.WriteLine("Please enter the advent of code day (a number between 1 and 24 inclusive).");
string? day = Console.ReadLine();
List<string> lines = File.ReadLines("input.txt").ToList();

if (int.TryParse(day, out int puzzle))
{
    switch (puzzle)
    {
        case 1:
            var solver = new December1st(lines);
            var elf = solver.GetElfWithMostCalories();
            var topThreeElvesTotalCalories = solver.GetTopThreeElvesTotalCalories();
            Console.WriteLine($"The elf with the most calories is {elf.Key} with {elf.Value} calories.");
            Console.WriteLine($"The top 3 elves have a total of {topThreeElvesTotalCalories} calories.");
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