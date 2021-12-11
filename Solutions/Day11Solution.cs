namespace AdventOfCode2021.Solutions {
    public class Day11Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Stage1() {
            List<string> input = ReadInputFile("Day11Stage01.txt");
            List<Octopus> octupuses = Enumerable.Range(0, 10).SelectMany(i => Enumerable.Range(0, 10).Select(j => new Octopus(j, i, int.Parse(input[i][j].ToString()))).ToList()).ToList();

            int flashes = 0;
            for (int i = 0; i < 100; i++) {
                octupuses.ForEach(o => o.ApplyNewStep());
                List<Octopus> flashing = octupuses.Where(o => o.WillFlash).ToList();
                while (flashing.Any()) {
                    flashes += flashing.Count;
                    flashing.ForEach(f => f.Flash(octupuses));
                    flashing = octupuses.Where(o => o.WillFlash).ToList();
                }

            }
            Console.WriteLine($"There were {flashes} flashes.");
        }

        private void Stage2() {
            List<string> input = ReadInputFile("Day11Stage02.txt");
            List<Octopus> octupuses = Enumerable.Range(0, 10).SelectMany(i => Enumerable.Range(0, 10).Select(j => new Octopus(j, i, int.Parse(input[i][j].ToString()))).ToList()).ToList();

            int stepCount = 0;
            while (octupuses.Any(o => !o.HasFlashed)) {
                stepCount++;
                octupuses.ForEach(o => o.ApplyNewStep());
                List<Octopus> flashing = octupuses.Where(o => o.WillFlash).ToList();
                while (flashing.Any()) {
                    flashing.ForEach(f => f.Flash(octupuses));
                    flashing = octupuses.Where(o => o.WillFlash).ToList();
                }
            }
            Console.WriteLine($"Simultaneous flash at step {stepCount}.");
        }

        private class Octopus {
            public Octopus(int x, int y, int energy) {
                X = x;
                Y = y;
                Energy = energy;
            }

            public int Energy { get; set; }

            public bool HasFlashed { get; set; }

            public bool WillFlash => !HasFlashed && Energy > 9;

            public int X { get; }

            public int Y { get; }

            public void ApplyNewStep() {
                Energy = HasFlashed ? 1 : Energy + 1;
                HasFlashed = false;
            }

            public void Flash(List<Octopus> octupuses) {
                HasFlashed = true;
                octupuses.Where(IsAdjacent).ForEach(o => o.Energy++);
            }

            private bool IsAdjacent(Octopus other) {
                bool adjacentX = other.X == X - 1 || other.X == X || other.X == X + 1;
                bool adjacentY = other.Y == Y - 1 || other.Y == Y || other.Y == Y + 1;
                return other != this && adjacentX && adjacentY;
            }
        }
    }
}