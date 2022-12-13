namespace PuzzleSolver.Year2022.Day11;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/11.
/// </summary>
[PuzzleDescription(description: "Day 11: Monkey in the Middle", 2022, 11)]
public class Solver : PuzzleSolver
{
    private readonly List<string> _puzzleInput = new();
    private readonly List<Monkey> _monkeys = new();

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne()
    {
        PopulateMonkeys();
        ProcessRounds(20);
        int monkeyBusiness = _monkeys
            .Select(m => m.InspectedItems)
            .OrderBy(i => i)
            .TakeLast(2)
            .Aggregate(1, (a, b) => a * b);
        return monkeyBusiness;
    }

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public long SolvePartTwo()
    {
        PopulateMonkeys(constantWorry: false);
        ProcessRounds(10000);
        long monkeyBusiness = _monkeys
            .Select(m => m.InspectedItems)
            .OrderBy(i => i)
            .TakeLast(2)
            .Aggregate(1L, (a, b) => a * b);
        return monkeyBusiness;
    }

    /// <inheritdoc/>
    public override void ProcessInput(List<string> input) => _puzzleInput.AddRange(input);

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        long partTwo = SolvePartTwo();

        AddPartOneAnswer("The level of monkey business after 20 rounds of stuff-slinging simian shenanigans.", partOne);
        AddPartTwoAnswer("The level of monkey business after 10000 rounds.", partTwo);
    }

    private void PopulateMonkeys(bool constantWorry = true)
    {
        _monkeys.Clear();

        int commonDivisor = 1;

        for (int i = 0; i < _puzzleInput.Count; i += 7)
        {
            List<long> startingItems = _puzzleInput[i + 1]
                .Replace("  Starting items: ", string.Empty)
                .Split(',')
                .Select(item => long.Parse(item.Trim()))
                .ToList();
            string[] operationParts = _puzzleInput[i + 2]
                .Replace("  Operation: new = ", string.Empty)
                .Split(' ');

            long Operation(long x) =>
                operationParts[1] switch
                {
                    "+" => (operationParts[0] == "old" ? x : int.Parse(operationParts[0])) +
                           (operationParts[2] == "old" ? x : int.Parse(operationParts[2])),
                    "*" => (operationParts[0] == "old" ? x : int.Parse(operationParts[0])) *
                           (operationParts[2] == "old" ? x : int.Parse(operationParts[2])),
                    _ => throw new InvalidOperationException($"Unexpected operand: {operationParts[1]}"),
                };

            int divisibleBy = int.Parse(
                _puzzleInput[i + 3].Replace("  Test: divisible by ", string.Empty));
            int trueMonkey = int.Parse(_puzzleInput[i + 4].Replace("    If true: throw to monkey ", string.Empty));
            int falseMonkey = int.Parse(_puzzleInput[i + 5].Replace("    If false: throw to monkey ", string.Empty));
            int Test(long x)
            {
                return x % divisibleBy == 0 ? trueMonkey : falseMonkey;
            }

            commonDivisor *= divisibleBy;

            _monkeys.Add(new Monkey(
                startingItems,
                Operation,
                Test));
        }

        foreach (Monkey monkey in _monkeys)
        {
            if (constantWorry)
            {
                monkey.WorryReducer = x => x / 3;
            }
            else
            {
                monkey.WorryReducer = x => x % commonDivisor;
            }
        }
    }

    private void ProcessRounds(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ProcessRound();
        }
    }

    private void ProcessRound()
    {
        foreach (Monkey monkey in _monkeys)
        {
            List<long> startingItems = new List<long>();
            startingItems.AddRange(monkey.StartingItems);
            monkey.StartingItems.Clear();

            foreach (int startingItem in startingItems)
            {
                long worryLevel = monkey.Operation(startingItem);
                worryLevel = monkey.WorryReducer(worryLevel);
                int throwTo = monkey.Test(worryLevel);
                _monkeys[throwTo].StartingItems.Add(worryLevel);
                monkey.InspectedItems++;
            }
        }
    }

    private class Monkey
    {
        public Monkey(
            List<long> startingItems,
            Func<long, long> operation,
            Func<long, int> test)
        {
            StartingItems = startingItems;
            Operation = operation;
            Test = test;
        }

        public int InspectedItems { get; set; }

        public List<long> StartingItems { get; }

        public Func<long, long> Operation { get; }

        public Func<long, int> Test { get; }

        public Func<long, long> WorryReducer { get; set; } = null!;
    }
}
