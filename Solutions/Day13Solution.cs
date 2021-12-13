using System.Drawing;

namespace AdventOfCode2021.Solutions {
    public class Day13Solution : Solution {
        public override List<Action> Stages => new List<Action> { Solve };

        private void Solve() {
            List<string> input = ReadInputFile("Day13Stage01.txt");
            List<Point> points = input.TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => new Point(int.Parse(l.Split(",")[0]), int.Parse(l.Split(",")[1]))).ToList();
            bool[,] paper = new bool[points.Max(p => p.X) + 1, points.Max(p => p.Y) + 1];
            points.ForEach(p => paper[p.X, p.Y] = true);
            List<Tuple<Direction, int>> folds = input.SkipWhile(l => !string.IsNullOrEmpty(l)).Skip(1).Select(l => {
                string[] lSplit = l.Remove(0, "fold along ".Length).Split("=");
                return new Tuple<Direction, int>(lSplit[0] == "y" ? Direction.Horizontal : Direction.Vertical, int.Parse(lSplit[1]));
            }).ToList();

            foreach (Tuple<Direction, int> fold in folds) {
                if (fold.Item1 == Direction.Horizontal) {
                    paper = FoldHorizontal(paper, fold.Item2);
                } else {
                    paper = FoldVertical(paper, fold.Item2);
                }

                int result = paper.Count(p => p);
                Console.WriteLine($"Folded! There are now {result} points visible.");
            }

            Console.WriteLine();
            for (int y = 0; y < paper.GetLength(1); y++) {
                for (int x = 0; x < paper.GetLength(0); x++) {
                    Console.Write(paper[x, y] ? "#" : " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private bool[,] FoldVertical(bool[,] paper, int foldLine) {
            bool[,] foldedPaper = new bool[foldLine, paper.GetLength(1)];
            for (int x = 0; x < paper.GetLength(0); x++) {
                for (int y = 0; y < paper.GetLength(1); y++) {
                    if (x < foldLine) {
                        foldedPaper[x, y] |= paper[x, y];
                    } else if (x > foldLine) {
                        int xf = x - (x - foldLine) * 2;
                        foldedPaper[xf, y] |= paper[x, y];
                    }
                }

            }
            return foldedPaper;
        }

        private bool[,] FoldHorizontal(bool[,] paper, int foldLine) {
            bool[,] foldedPaper = new bool[paper.GetLength(0), foldLine];
            for (int x = 0; x < paper.GetLength(0); x++) {
                for (int y = 0; y < paper.GetLength(1); y++) {
                    if (y < foldLine) {
                        foldedPaper[x, y] |= paper[x, y];
                    } else if (y > foldLine) {
                        int yf = y - (y - foldLine) * 2;
                        foldedPaper[x, yf] |= paper[x, y];
                    }
                }

            }
            return foldedPaper;
        }

        private enum Direction {
            Horizontal,
            Vertical
        }
    }
}