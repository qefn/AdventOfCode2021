namespace AdventOfCode2021.Solutions {
    public class Day06Solution : Solution {
        public override int Id => 6;

        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Solve(string inputFile, int days) {
            List<string> input = ReadInputFile(inputFile);
            List<Fish> fishes = new List<Fish>(2000000000);
            fishes.AddRange(input.First().Split(",").Select(str => new Fish(int.Parse(str))));
            for (int i = 0; i < days; i++) {
                Console.WriteLine(i);
                int newBorn = 0;
                fishes.ForEach(fish => {
                    if (fish.LiveAnotherDay()) {
                        newBorn++;
                    }
                });
                for (int j = 0; j < newBorn; j++) {
                    fishes.Add(new Fish());
                }
            }

            Console.WriteLine($"There are {fishes.Count} lanternfishes after 80 days.");
        }

        private void Stage1() {
            Solve("Day06Stage01.txt", 80);
        }

        private void Stage2() {
            Solve("Day06Stage02.txt", 256);
        }

        private class Fish {
            public Fish() {
            }

            public Fish(int timeTillReproduction) {
                ReproductionCountdown = timeTillReproduction;
            }

            private int ReproductionCountdown { get; set; } = 8;

            public bool LiveAnotherDay() {
                if (ReproductionCountdown == 0) {
                    ReproductionCountdown = 6;
                    return true;
                }
                ReproductionCountdown--;
                return false;
            }
        }
    }
}