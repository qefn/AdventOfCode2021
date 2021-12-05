using System;
using AdventOfCode2021.Solutions;

namespace AdventOfCode2021 {
    public class Program {
        private static readonly List<Solution> Puzzles = new List<Solution> { new Day01Solution(), new Day02Solution(), new Day03Solution(), new Day04Solution(), new Day05Solution() };

        public static void Main() {
            try {
                ExecuteSolution();
            } catch (Exception e) {
                Console.WriteLine("Exception while executing solution!");
                Console.WriteLine(e);
            }
        }

        private static void ExecuteSolution() {
            Console.WriteLine($"There are cuttently {Puzzles.Count} puzzle solutions registered.");
            if (!Puzzles.Any()) {
                Console.WriteLine("No puzzle solutions yet. Exiting.");
                return;
            }

            Console.WriteLine($"Select a puzzle solution by entering a number between 1 and {Puzzles.Count} to execute the corresponding puzzle solution");
            Console.WriteLine("Enter an empty value to execute the last available puzzle solution.");
            string? puzzleId = Console.ReadLine();
            Solution puzzleSolution;
            if (puzzleId == null || puzzleId == "") {
                puzzleSolution = Puzzles.Last();
            } else if (int.TryParse(puzzleId, out int puzzleIdInt) && puzzleIdInt > 0 && puzzleIdInt <= Puzzles.Count) {
                puzzleSolution = Puzzles[puzzleIdInt - 1];
            } else {
                Console.WriteLine($"'{puzzleId}' is not a valid option!");
                return;
            }

            Console.WriteLine($"The selected puzzle solution has {puzzleSolution.Stages.Count} stage solutions defined.");
            if (!puzzleSolution.Stages.Any()) {
                Console.WriteLine("No stage solutions yet. Exiting.");
                return;
            }

            Console.WriteLine($"Select a stage solution by entering a number between 1 and {puzzleSolution.Stages.Count} to execute the corresponding stage solution");
            Console.WriteLine("Enter an empty value to execute the last available stage solution.");
            string? stageId = Console.ReadLine();
            Action stageSolution;
            if (stageId == null || stageId == "") {
                stageSolution = puzzleSolution.Stages.Last();
            } else if (int.TryParse(stageId, out int stageIdInt) && stageIdInt > 0 && stageIdInt <= puzzleSolution.Stages.Count) {
                stageSolution = puzzleSolution.Stages[stageIdInt - 1];
            } else {
                Console.WriteLine($"'{stageId}' is not a valid option!");
                return;
            }

            stageSolution();
        }
    }
}