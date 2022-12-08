namespace PuzzleSolver.Tests.Year2022Tests.Day07;

public class SolverTests
{
    private const string SampleData = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

    [Fact]
    public void Solves_With_Sample_Data()
    {
        // Arrange
        Year2022.Day07.Solver puzzleSolver = new();
        List<string> input = SampleData.Split("\r\n").ToList();

        // Act
        puzzleSolver.ProcessInput(input);
        int partOne = puzzleSolver.SolvePartOne();
        int partTwo = puzzleSolver.SolvePartTwo();

        // Assert
        partOne.ShouldBe(95437);

        // partTwo.ShouldBe(4);
    }
}
