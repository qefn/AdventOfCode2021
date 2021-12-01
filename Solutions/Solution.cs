namespace AdventOfCode2021.Solutions {
    public abstract class Solution {
        public abstract int Id { get; }

        public abstract List<Action> Stages { get; }

        protected List<string> ReadInputFile(string inputFileName) {
            string path = @$".\Inputs\{inputFileName}";
            if (!File.Exists(path)) {
                throw new InvalidOperationException($"The input file '{path}' does not exist!");
            }

            return File.ReadAllLines(path).ToList();
        }
    }
}