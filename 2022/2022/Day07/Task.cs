using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2022.Day07
{
    internal class Task : BaseTask<int>
    {
        private const string slash = @"\";
        public Task() : base(95437, 24933642) { }

        public override int SolvePart1(List<string> input)
        {
            Dictionary<string, Folder> fileSystem = GetFileSystem(input);
            return fileSystem.Where(p => p.Value.GetFolderSize(fileSystem) <= 100000)
                .Sum(p => p.Value.GetFolderSize(fileSystem));
        }

        public override int SolvePart2(List<string> input)
        {
            Dictionary<string, Folder> fileSystem = GetFileSystem(input);
            var totalSpace = 70000000;
            var requiredUnused = 30000000;
            var totalUsedSpace = fileSystem[slash].GetFolderSize(fileSystem);
            var requiredSpaceToFreedUp = requiredUnused - (totalSpace - totalUsedSpace);
            return fileSystem.Select(p => p.Value.GetFolderSize(fileSystem))
                .Where(p => p >= requiredSpaceToFreedUp)
                .OrderBy(p => p)
                .First();
        }

        private static Dictionary<string, Folder> GetFileSystem(List<string> input)
        {
            string currentPath = null;
            var fileSystem = new Dictionary<string, Folder>();
            foreach (var item in input)
            {
                if (item.StartsWith("$ cd"))
                {
                    var path = item.Split(' ').Last();
                    if (currentPath == null)
                    {
                        currentPath = slash; // runtime Windows;
                    }
                    else if (path.Contains(".."))
                    {
                        currentPath = Path.GetFullPath(Path.Combine(currentPath, ".."))
                            .Split(':')[1];
                    }
                    else
                    {
                        if (!currentPath.EndsWith(slash))
                        {
                            currentPath += slash;
                        }
                        currentPath += path;
                        currentPath += slash;
                    }
                }
                else if (item.StartsWith("$ ls"))
                {
                    // skip
                }
                else if (item.StartsWith("dir "))
                {
                    var name = item.Split(' ')[1];
                    if (!fileSystem.ContainsKey(currentPath))
                    {
                        fileSystem.Add(currentPath, new Folder() { CurrentPath = currentPath });
                    }

                    fileSystem[currentPath].SubFolders.Add(name);
                }
                else
                {
                    var size = int.Parse(item.Split(' ')[0]);
                    //var name= item.Split(' ')[1];
                    if (!fileSystem.ContainsKey(currentPath))
                    {
                        fileSystem.Add(currentPath, new Folder() { CurrentPath = currentPath });
                    }

                    fileSystem[currentPath].Files.Add(size);
                }
            }

            return fileSystem;
        }

        internal class Folder
        {
            public string CurrentPath { get; set; }
            public List<int> Files { get; set; } = new List<int>();

            public List<string> SubFolders { get; set; } = new List<string>();

            public int FolderSize { get; set; }

            public int GetFolderSize(Dictionary<string, Folder> fileSystem)
            {
                if (FolderSize == 0)
                {
                    FolderSize = Files.Sum()
                        + SubFolders.Select(p => fileSystem[CurrentPath + p + @"\"].GetFolderSize(fileSystem)).Sum();
                }

                return FolderSize;

            }
        }
    }
}
