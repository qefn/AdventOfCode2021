namespace AdventOfCode2021.Solutions {
    public class Day07Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Solve(string inputFile, bool constantConsumption) {
            List<string> input = ReadInputFile(inputFile);
            List<int> crabs = input[0].Split(",").Select(str => int.Parse(str)).ToList();
            Tuple<int, int> bestPosition = new Tuple<int, int>(0, 0);
            for (int i = crabs.Min(); i <= crabs.Max(); i++) {
                int fuelConsumption = constantConsumption
                                        ? crabs.Sum(crab => Math.Abs(crab - i))
                                        : crabs.Sum(crab => {
                                            int distance = Math.Abs(crab - i);
                                            return (distance * (distance + 1)) / 2;
                                        });
                if (bestPosition.Item2 == 0 || bestPosition.Item2 > fuelConsumption) {
                    bestPosition = new Tuple<int, int>(i, fuelConsumption);
                }
            }
            Console.WriteLine($"The best position is {bestPosition.Item1} with a fuelConsumption of {bestPosition.Item2}");
        }

        private void Stage1() {
            Solve("Day07Stage01.txt", true);
        }

        private void Stage2() {
            Solve("Day07Stage02.txt", false);
        }
    }
}