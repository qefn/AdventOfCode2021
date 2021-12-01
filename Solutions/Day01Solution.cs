namespace AdventOfCode2021.Solutions {
    public class Day01Solution : Solution {
        public override int Id => 1;

        public override List<Action> Stages => new List<Action> { Stage1 };

        private void Stage1() {
            List<string> lines = ReadInputFile("Day01Stage01.txt");
            List<int> measurements = lines.Select(line => int.Parse(line)).ToList();
            int increases = 0;
            for (int i = 1; i < measurements.Count; i++) {
                int previousMeasurement = measurements[i - 1];
                int currentMeasurement = measurements[i];
                increases += previousMeasurement < currentMeasurement ? 1 : 0;
            }
            Console.WriteLine($"The depth increased {increases} times.");
        }
    }
}