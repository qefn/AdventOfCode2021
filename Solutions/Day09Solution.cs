namespace AdventOfCode2021.Solutions {
    public class Day09Solution : Solution {
        public override List<Action> Stages => new List<Action> { Solve };

        private void Solve() {
            List<string> input = ReadInputFile("Day09Stage02.txt");
            int[,] map = new int[input.Count, input.First().Length];
            for (int i = 0; i < input.Count; i++) {
                string line = input[i];
                for (int j = 0; j < input.First().Length; j++) {
                    map[i, j] = int.Parse(line[j].ToString());
                }
            }

            int totalRiskLevel = 0;
            List<Basin> basins = new List<Basin>();
            for (int y = 0; y < map.GetLength(0); y++) {
                for (int x = 0; x < map.GetLength(1); x++) {
                    if (IsLowPoint(map, x, y, out Basin? basin)) {
                        totalRiskLevel += map[y, x] + 1;
                    }
                    if (basin != null) {
                        basins.Add(basin);
                    }
                }
            }
            Console.WriteLine($"Total risk level: {totalRiskLevel}");
            int topThreeBasinSizeSum = basins.OrderByDescending(b => b.Size).Take(3).Aggregate(1, (x, y) => x * y.Size);
            Console.WriteLine($"Total size of top 3 basins: {topThreeBasinSizeSum}");
        }

        private bool IsLowPoint(int[,] map, int x, int y, out Basin? basin) {
            int height = map[y, x];
            basin = null;
            bool isLowpoint = AdjacentIsHigher(map, x - 1, y, height)
                                && AdjacentIsHigher(map, x + 1, y, height)
                                && AdjacentIsHigher(map, x, y - 1, height)
                                && AdjacentIsHigher(map, x, y + 1, height);
            if (isLowpoint) {
                basin = new Basin(x, y, height);
                basin.Fill(map);
            }
            return isLowpoint;
        }

        private bool AdjacentIsHigher(int[,] map, int x, int y, int height) {
            try {
                return map[y, x] > height;
            } catch (System.Exception) {

                return true;
            }
        }

        private class Basin {
            public Basin(int x, int y, int height) {
                Add(new BasinPoint(x, y, height));
            }

            private List<BasinPoint> Content { get; } = new List<BasinPoint>();

            public int Size => Content.Count;
            public void Fill(int[,] map) {
                if (Content.Count > 1) {
                    return;
                }
                Fill(map, Content[0]);
            }

            private void Fill(int[,] map, BasinPoint point) {
                Fill(map, point.X - 1, point.Y);
                Fill(map, point.X + 1, point.Y);
                Fill(map, point.X, point.Y - 1);
                Fill(map, point.X, point.Y + 1);
            }

            private void Fill(int[,] map, int x, int y) {
                if (IsBasinPoint(map, x, y)) {
                    BasinPoint newPoint = new BasinPoint(x, y, map[y, x]);
                    if (Add(newPoint)) {
                        Fill(map, newPoint);
                    }
                }
            }

            private bool IsBasinPoint(int[,] map, int x, int y) {
                try {
                    return map[y, x] < 9;
                } catch (System.Exception) {

                    return false;
                }
            }

            private bool Add(BasinPoint point) {
                if (Content.All(p => p.X != point.X || p.Y != point.Y)) {
                    Content.Add(point);
                    return true;
                }
                return false;
            }
        }

        private class BasinPoint {
            public BasinPoint(int x, int y, int height) {
                X = x;
                Y = y;
                Height = height;
            }

            public int X { get; }

            public int Y { get; }

            public int Height { get; }
        }
    }
}