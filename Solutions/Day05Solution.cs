using System.Drawing;

namespace AdventOfCode2021.Solutions {
    public class Day05Solution : Solution {
        public override int Id => 5;

        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Stage1() {
            Solve("Day05Stage01.txt", false);
        }

        private void Stage2() {
            Solve("Day05Stage02.txt", true);
        }

        private Point GetPointFromString(string pointStr) {
            string[] pointCoords = pointStr.Split(",");
            return new Point(int.Parse(pointCoords[0]), int.Parse(pointCoords[1]));
        }

        private void Solve(string inputFile, bool considerDiagonals) {
            List<string> input = ReadInputFile(inputFile);
            List<Tuple<Point, Point>> lines = input.Select(line => line.Split(" -> ")).Select(line => new Tuple<Point, Point>(GetPointFromString(line[0]), GetPointFromString(line[1]))).ToList();
            int[,] grid = new int[lines.Max(line => line.Item1.X > line.Item2.X ? line.Item1.X : line.Item2.X) + 1, lines.Max(line => line.Item1.Y > line.Item2.Y ? line.Item1.Y : line.Item2.Y) + 1];

            foreach (var line in lines) {
                bool horizontalLine = line.Item1.X != line.Item2.X;
                bool verticalLine = line.Item1.Y != line.Item2.Y;
                if (horizontalLine && verticalLine) {
                    if (!considerDiagonals) {
                        continue;
                    }

                    int x = line.Item1.X;
                    int y = line.Item1.Y;
                    for (int i = 0; i <= Math.Abs(line.Item1.X - line.Item2.X); i++) {
                        grid[x, y] += 1;
                        x = x < line.Item2.X ? x + 1 : x - 1;
                        y = y < line.Item2.Y ? y + 1 : y - 1;
                    }
                    continue;
                }
                if (horizontalLine || (!horizontalLine && !verticalLine)) {
                    for (int x = Math.Min(line.Item1.X, line.Item2.X); x <= Math.Max(line.Item1.X, line.Item2.X); x++) {
                        grid[x, line.Item1.Y] += 1;
                    }
                }
                if (verticalLine) {
                    for (int y = Math.Min(line.Item1.Y, line.Item2.Y); y <= Math.Max(line.Item1.Y, line.Item2.Y); y++) {
                        grid[line.Item1.X, y] += 1;
                    }
                }
            }

            int overlappingPointVount = (from int point in grid select point).Count(point => point > 1);
            Console.WriteLine($"{overlappingPointVount} Point are overlapping.");
        }
    }
}