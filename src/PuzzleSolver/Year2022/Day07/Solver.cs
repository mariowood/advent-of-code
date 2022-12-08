using System.Text.RegularExpressions;

namespace PuzzleSolver.Year2022.Day07;

/// <summary>
/// A class which will solve the puzzle from https://adventofcode.com/2022/day/7.
/// </summary>
[PuzzleDescription(description: "Day 7: No Space Left On Device", 2022, 7)]
public sealed class Solver : PuzzleSolver
{
    private readonly List<FileSystemObject> _rootFileSystem = new();

    /// <summary>
    /// Solves the first part of the puzzle.
    /// </summary>
    /// <returns>The answer for part one.</returns>
    public int SolvePartOne()
    {
        Dictionary<string, int> results = new();
        GetDirectorySize((Directory)_rootFileSystem[0], string.Empty, results);
        IEnumerable<KeyValuePair<string, int>> directoriesWithinRange =
            results.Where(kv => kv.Value <= 100000);
        return directoriesWithinRange.Sum(kv => kv.Value);
    }

    /// <summary>
    /// Solves the second part of the puzzle.
    /// </summary>
    /// <returns>The answer for part two.</returns>
    public int SolvePartTwo()
    {
        const int rootSpace = 70000000;
        const int freeSpaceNeeded = 30000000;
        Dictionary<string, int> results = new();
        GetDirectorySize((Directory)_rootFileSystem[0], string.Empty, results);

        int rootVolumeSpaceUsed = results["/"];
        int freeSpace = rootSpace - rootVolumeSpaceUsed;
        int needToFree = freeSpaceNeeded - freeSpace;

        var directoryToDelete = results
            .Where(kv =>
                kv.Key != "/" &&
                kv.Value >= needToFree)
            .MinBy(kv => kv.Value);
        return directoryToDelete.Value;
    }

    /// <inheritdoc/>
    public override void ProcessInput(List<string> lines)
    {
        const string commandPattern = @"^\$\s(\w+)\s?(\S+)?$";
        const string dirPattern = @"^dir\s(\w+)$";
        const string filePattern = @"^(\d+)\s(.+)$";
        Regex commandRegex = new Regex(commandPattern, RegexOptions.Compiled);
        Regex dirRegex = new Regex(dirPattern, RegexOptions.Compiled);
        Regex fileRegex = new Regex(filePattern, RegexOptions.Compiled);

        Directory root = new Directory("/", new List<FileSystemObject>());
        Directory workingDirectory = root;
        foreach (string line in lines)
        {
            if (commandRegex.IsMatch(line))
            {
                Match commandMatch = commandRegex.Match(line);
                string command = commandMatch.Groups[1].Value;

                switch (command)
                {
                    case "cd":
                    {
                        string targetDir = commandMatch.Groups[2].Value;

                        if (targetDir == "/")
                        {
                            workingDirectory = root;
                        }
                        else
                        {
                            workingDirectory = targetDir == ".."
                                ? workingDirectory.Parent
                                : FindDirectory(workingDirectory, targetDir);
                        }

                        break;
                    }

                    case "ls":
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown command: {command}");
                }
            }
            else if (dirRegex.IsMatch(line))
            {
                Match dirMatch = dirRegex.Match(line);
                string dirName = dirMatch.Groups[1].Value;
                if (!workingDirectory.Children.Any(child =>
                        child.ObjectType == ObjectType.Directory &&
                        child.Name == dirName))
                {
                    workingDirectory.Children.Add(
                        new Directory(dirName, workingDirectory, new List<FileSystemObject>()));
                }
            }
            else if (fileRegex.IsMatch(line))
            {
                Match fileMatch = fileRegex.Match(line);
                int fileSize = int.Parse(fileMatch.Groups[1].Value);
                string fileName = fileMatch.Groups[2].Value;

                if (!workingDirectory.Children.Any(child =>
                        child.ObjectType == ObjectType.File &&
                        child.Name == fileName))
                {
                    workingDirectory.Children.Add(
                        new File(fileName, fileSize));
                }
            }
        }

        _rootFileSystem.Add(root);
    }

    /// <inheritdoc/>
    protected override void SolvePuzzles()
    {
        int partOne = SolvePartOne();
        int partTwo = SolvePartTwo();

        AddPartOneAnswer("Sum of directories with a total size less than 100000.", partOne);
        AddPartTwoAnswer("Size of smallest directory to delete to free enough space.", partTwo);
    }

    private Directory FindDirectory(Directory current, string name)
    {
        if (current.Children.Any(child =>
                child.ObjectType == ObjectType.Directory &&
                child.Name == name))
        {
            return (Directory)current.Children.Single(child =>
                child.ObjectType == ObjectType.Directory &&
                child.Name == name);
        }

        throw new InvalidOperationException($"Current directory contains no child called: {name}");
    }

    private void GetDirectorySize(Directory root, string parentFullPath, Dictionary<string, int> results)
    {
        string currentDirFullPath = string.IsNullOrWhiteSpace(parentFullPath)
            ? root.Name
            : string.Join('/', parentFullPath, root.Name);

        if (!results.ContainsKey(currentDirFullPath))
        {
            results[currentDirFullPath] = 0;
        }

        results[currentDirFullPath] += root.Children
            .Where(child => child.ObjectType == ObjectType.File)
            .Sum(child => ((File)child).Size);

        foreach (FileSystemObject dir in root.Children
            .Where(child => child.ObjectType == ObjectType.Directory))
        {
            string childFullPath = string.Join('/', currentDirFullPath, dir.Name);
            GetDirectorySize((Directory)dir, currentDirFullPath, results);
            results[currentDirFullPath] += results[childFullPath];
        }
    }

    private class FileSystemObject
    {
        public ObjectType ObjectType { get; }

        public string Name { get; }

        protected FileSystemObject(ObjectType objectType, string name)
        {
            ObjectType = objectType;
            Name = name;
        }
    }

    private class File : FileSystemObject
    {
        public int Size { get; }

        public File(string name, int size)
            : base(ObjectType.File, name)
        {
            Size = size;
        }
    }

    private class Directory : FileSystemObject
    {
        public Directory Parent { get; }

        public List<FileSystemObject> Children { get; } = new();

        public Directory(string name, List<FileSystemObject> children)
            : base(ObjectType.Directory, name)
        {
            Parent = this;
            Children.AddRange(children);
        }

        public Directory(string name, Directory parent, List<FileSystemObject> children)
            : base(ObjectType.Directory, name)
        {
            Parent = parent;
            Children.AddRange(children);
        }
    }

    private enum ObjectType
    {
        File,
        Directory,
    }
}
