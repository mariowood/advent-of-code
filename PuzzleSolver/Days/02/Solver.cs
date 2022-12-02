namespace PuzzleSolver.Days._02;

public static class Solver
{
    public static int GetTotalScore(List<string> input)
    {
        return input.Sum(line => ProcessGame(line[0], line[2]));
    }

    public static int GetTotalScoreForTargetResult(List<string> input)
    {
        return input.Sum(line => ProcessGameForResult(line[0], line[2]));
    }

    private static int ProcessGame(char opponent, char player)
    {
        var opponentChoice = opponent switch
        {
            'A' => Move.Rock,
            'B' => Move.Paper,
            'C' => Move.Scissors,
            _ => throw new InvalidOperationException($"Opponent choice not valid: {opponent}")
        };

        var playerChoice = player switch
        {
            'X' => Move.Rock,
            'Y' => Move.Paper,
            'Z' => Move.Scissors,
            _ => throw new InvalidOperationException($"Player choice not valid: {player}")
        };

        return GetGameResult(opponentChoice, playerChoice);
    }

    private static int ProcessGameForResult(char opponent, char targetResult)
    {
        var opponentChoice = opponent switch
        {
            'A' => Move.Rock,
            'B' => Move.Paper,
            'C' => Move.Scissors,
            _ => throw new InvalidOperationException($"Opponent choice not valid: {opponent}")
        };

        Move playerChoice = targetResult switch
        {
            'X' => opponentChoice switch
            {
                Move.Rock => Move.Scissors,
                Move.Paper => Move.Rock,
                Move.Scissors => Move.Paper,
                _ => throw new InvalidOperationException($"Move not allowed: {opponentChoice}")
            },
            'Y' => opponentChoice switch
            {
                Move.Rock => Move.Rock,
                Move.Paper => Move.Paper,
                Move.Scissors => Move.Scissors,
                _ => throw new InvalidOperationException($"Move not allowed: {opponentChoice}")
            },
            'Z' => opponentChoice switch
            {
                Move.Rock => Move.Paper,
                Move.Paper => Move.Scissors,
                Move.Scissors => Move.Rock,
                _ => throw new InvalidOperationException($"Move not allowed: {opponentChoice}")
            },
            _ => throw new InvalidOperationException($"Target result not valid: {targetResult}")
        };

        return GetGameResult(opponentChoice, playerChoice);
    }

    private static int GetGameResult(Move opponent, Move player)
    {
        const int rock = 1;
        const int paper = 2;
        const int scissors = 3;
        
        int result = 0;

        switch (player)
        {
            case Move.Rock when opponent == Move.Rock:
                result += rock;
                result += ResultConstants.Draw;
                break;
            case Move.Rock when opponent == Move.Paper:
                result += rock;
                result += ResultConstants.Lose;
                break;
            case Move.Rock when opponent == Move.Scissors:
                result += rock;
                result += ResultConstants.Win;
                break;
            case Move.Paper when opponent == Move.Rock:
                result += paper;
                result += ResultConstants.Win;
                break;
            case Move.Paper when opponent == Move.Paper:
                result += paper;
                result += ResultConstants.Draw;
                break;
            case Move.Paper when opponent == Move.Scissors:
                result += paper;
                result += ResultConstants.Lose;
                break;
            case Move.Scissors when opponent == Move.Rock:
                result += scissors;
                result += ResultConstants.Lose;
                break;
            case Move.Scissors when opponent == Move.Paper:
                result += scissors;
                result += ResultConstants.Win;
                break;
            case Move.Scissors when opponent == Move.Scissors:
                result += scissors;
                result += ResultConstants.Draw;
                break;
        }

        return result;
    }

    private static class ResultConstants
    {
        public const int Win = 6;
        public const int Draw = 3;
        public const int Lose = 0;
    }
    
    private enum Move
    {
        Rock,
        Paper,
        Scissors
    }
}