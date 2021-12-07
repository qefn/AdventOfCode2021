namespace AdventOfCode2021.Solutions {
    public class Day02Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private List<Tuple<Direction, int>> GetCourse(string inputFileName) {
            List<string> lines = ReadInputFile(inputFileName);
            return lines.Select(line => {
                string[] parameters = line.Split(" ");
                return new Tuple<Direction, int>(Enum.Parse<Direction>(parameters[0]), int.Parse(parameters[1]));
            }).ToList();
        }

        private void Stage1() {
            List<Tuple<Direction, int>> course = GetCourse("Day02Stage01.txt");
            int x = 0, z = 0;
            foreach (Tuple<Direction, int> step in course) {
                switch (step.Item1) {
                    case Direction.forward:
                        x += step.Item2;
                        break;
                    case Direction.up:
                        z -= step.Item2;
                        break;
                    case Direction.down:
                        z += step.Item2;
                        break;
                }
            }
            int result = x * z;
            Console.WriteLine($"Result: {result}");
        }

        private void Stage2() {
            List<Tuple<Direction, int>> course = GetCourse("Day02Stage02.txt");
            int x = 0, z = 0, aim = 0;
            foreach (Tuple<Direction, int> step in course) {
                switch (step.Item1) {
                    case Direction.forward:
                        x += step.Item2;
                        z += aim * step.Item2;
                        break;
                    case Direction.up:
                        aim -= step.Item2;
                        break;
                    case Direction.down:
                        aim += step.Item2;
                        break;
                }
            }
            int result = x * z;
            Console.WriteLine($"Result: {result}");
        }

        private enum Direction {
            forward,
            down,
            up
        }
    }
}