namespace AdventOfCode2021.Solutions {
    public class Day12Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Stage1() {
            Solve("Day12Stage01.txt", false);
        }

        private void Stage2() {
            Solve("Day12Stage02.txt", true);
        }

        private void Solve(string inputFile, bool extendedExploration) {
            List<string> input = ReadInputFile(inputFile);
            List<Cave> caves = new List<Cave>();
            input.ForEach(i => {
                string[] split = i.Split("-");
                Cave? cave1 = caves.FirstOrDefault(c => c.Name == split[0]);
                Cave? cave2 = caves.FirstOrDefault(c => c.Name == split[1]);
                if (cave1 == null) {
                    cave1 = new Cave(split[0]);
                    caves.Add(cave1);
                }
                if (cave2 == null) {
                    cave2 = new Cave(split[1]);
                    caves.Add(cave2);
                }
                cave1.Connected.Add(cave2);
                cave2.Connected.Add(cave1);
            });

            Cave start = caves.Single(c => c.IsStart);
            List<string> paths = Explore(start, extendedExploration);
            Console.WriteLine($"There are {paths.Count} paths.");
        }

        private List<string> Explore(Cave c, bool extendedExploration) {
            List<Cave> currentPath = new List<Cave>();
            return Explore(c, currentPath, extendedExploration);
        }

        private List<string> Explore(Cave c, List<Cave> currentPath, bool extendedExploration) {
            List<string> paths = new List<string>();
            currentPath.Add(c);
            if (c.IsEnd) {
                paths.Add(currentPath.Aggregate("", (s, next) => $"{s},{next.Name}").Remove(0, 1));
                return paths;
            }

            foreach (Cave next in c.Connected) {
                List<IGrouping<string, Cave>> visitedSmallCaves = currentPath.Where(n => !n.IsBigCave).GroupBy(n => n.Name).ToList();
                if (!extendedExploration
                    && !next.IsBigCave
                    && visitedSmallCaves.Any(n => n.Key == next.Name)) {
                    continue;
                }

                if (extendedExploration
                    && !next.IsBigCave
                    && visitedSmallCaves.Any(visited => visited.Count() > 1)
                    && visitedSmallCaves.Any(n => n.Key == next.Name)
                    || next.IsStart) {
                    continue;
                }
                paths = paths.Concat(Explore(next, new List<Cave>(currentPath), extendedExploration)).ToList();
            }

            return paths;
        }

        private class Cave {
            public Cave(string name) {
                Name = name;
            }

            public List<Cave> Connected { get; } = new List<Cave>();

            public bool IsBigCave => Name.All(c => Char.IsUpper(c));

            public bool IsEnd => Name == "end";

            public bool IsStart => Name == "start";

            public string Name { get; }
        }
    }
}