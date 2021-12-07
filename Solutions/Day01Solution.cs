namespace AdventOfCode2021.Solutions {
    public class Day01Solution : Solution {
        public override List<Action> Stages => new List<Action> { () => Stage1(), Stage2 };

        private void Stage1(List<int>? measurements = null) {
            if (measurements == null) {
                List<string> lines = ReadInputFile("Day01Stage01.txt");
                measurements = lines.Select(line => int.Parse(line)).ToList();
            }

            int increases = 0;
            for (int i = 1; i < measurements.Count; i++) {
                int previousMeasurement = measurements[i - 1];
                int currentMeasurement = measurements[i];
                increases += previousMeasurement < currentMeasurement ? 1 : 0;
            }
            Console.WriteLine($"The depth increased {increases} times.");
        }

        private void Stage2() {
            List<string> lines = ReadInputFile("Day01Stage02.txt");
            List<int> measurements = lines.Select(line => int.Parse(line)).ToList();
            List<int> measurementSums = new List<int>();
            for (int i = 0; i < measurements.Count - 2; i++) {
                measurementSums.Add(measurements[i] + measurements[i + 1] + measurements[i + 2]);
            }
            Stage1(measurementSums);
        }
    }
}