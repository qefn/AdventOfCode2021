namespace AdventOfCode2021.Solutions {
    public class Day06Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Solve(string inputFile, int days) {
            List<string> input = ReadInputFile(inputFile);
            Dictionary<int, long> reproductionState = new Dictionary<int, long> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 } };
            List<int> fishes = input.First().Split(",").Select(str => int.Parse(str)).ToList();
            fishes.ForEach(fish => reproductionState[fish]++);

            for (int i = 0; i < days; i++) {
                long newBorn = reproductionState[0];
                reproductionState[7] += newBorn;
                for (int j = 1; j < 9; j++) {
                    reproductionState[j - 1] = reproductionState[j];
                }
                reproductionState[8] = newBorn;
            }

            long count = reproductionState.Values.Sum();
            Console.WriteLine($"There are {count} lanternfishes after {days} days.");
        }

        private void Stage1() {
            Solve("Day06Stage01.txt", 80);
        }

        private void Stage2() {
            Solve("Day06Stage02.txt", 256);
        }
    }
}