﻿using Spectre.Console;

namespace PuzzleSolver.Year2022.Day10;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/10.
/// </summary>
[PuzzleDescription(description: "Day 10: Cathode-Ray Tube", 2022, 10)]
public class Solver : PuzzleSolver
{
    private readonly List<string> _puzzleInput = new();
    private readonly string[,] _screen = new string[6, 40];

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne()
    {
        int register = 1;
        int cycles = 1;
        List<int> signalStrengths = new();

        foreach (string instruction in _puzzleInput)
        {
            string[] parts = instruction.Split(' ');
            switch (parts[0])
            {
                case "noop":
                    signalStrengths.Add(CalculateSignalStrength(cycles, register));
                    cycles++;
                    break;
                case "addx":
                    signalStrengths.Add(CalculateSignalStrength(cycles, register));
                    cycles++;
                    signalStrengths.Add(CalculateSignalStrength(cycles, register));
                    cycles++;
                    register += int.Parse(parts[1]);
                    break;
                default:
                    throw new InvalidOperationException($"Unrecognized command: {parts[0]}");
            }
        }

        List<int> nonZeroSignalStrengths = signalStrengths.Where(s => s != 0).ToList();
        return nonZeroSignalStrengths.Sum();
    }

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public string SolvePartTwo()
    {
        int register = 1;
        int cycles = 1;

        foreach (string[] parts in _puzzleInput.Select(instruction => instruction.Split(' ')))
        {
            switch (parts[0])
            {
                case "noop":
                    _screen[(cycles - 1) / 40, (cycles - 1) % 40] = GetPixel(cycles, register);
                    cycles++;
                    break;
                case "addx":
                    _screen[(cycles - 1) / 40, (cycles - 1) % 40] = GetPixel(cycles, register);
                    cycles++;
                    _screen[(cycles - 1) / 40, (cycles - 1) % 40] = GetPixel(cycles, register);
                    cycles++;
                    register += int.Parse(parts[1]);
                    break;
                default:
                    throw new InvalidOperationException($"Unrecognized command: {parts[0]}");
            }
        }

        return "N/A";
    }

    /// <inheritdoc/>
    public override void ProcessInput(List<string> lines) => _puzzleInput.AddRange(lines);

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        string partTwo = SolvePartTwo();

        AddPartOneAnswer("Sum of signal strengths.", partOne);
        AddPartTwoAnswer("See output below table.", partTwo);
    }

    /// <inheritdoc/>
    protected override void ShowAdditionalOutput()
    {
        AnsiConsole.WriteLine();

        for (int i = 0; i < 6; i++)
        {
            string line = string.Empty;
            for (int j = 0; j < 40; j++)
            {
                line += _screen[i, j];
            }

            AnsiConsole.WriteLine(line);
        }
    }

    private static int CalculateSignalStrength(int cycle, int register)
    {
        if ((cycle - 20) % 40 == 0)
        {
            return register * cycle;
        }

        return 0;
    }

    private static string GetPixel(int cycle, int register)
    {
        int drawPixel = (cycle - 1) % 40;
        int spriteMin = register - 1;
        int spriteMax = register + 1;

        if (spriteMin == drawPixel || register == drawPixel || spriteMax == drawPixel)
        {
            return "#";
        }

        return " ";
    }
}
