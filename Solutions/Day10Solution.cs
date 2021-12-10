namespace AdventOfCode2021.Solutions {
    public class Day10Solution : Solution {
        public override List<Action> Stages => new List<Action> { Solve };

        private Dictionary<char, char> BracketMap = new Dictionary<char, char>{
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'}
        };

        private Dictionary<char, int> ScoreTableCorrupted = new Dictionary<char, int>{
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137}
        };

        private Dictionary<char, int> ScoreTableIncomplete = new Dictionary<char, int>{
            {'(', 1},
            {'[', 2},
            {'{', 3},
            {'<', 4}
        };

        private void Solve() {
            List<string> input = ReadInputFile("Day10Stage02.txt");
            List<char> corruptedCharacters = new List<char>();
            List<long> incompleteLineScores = new List<long>();
            foreach (string line in input) {
                Stack<char> chunks = new Stack<char>();
                bool complete = true;
                foreach (char c in line) {
                    if (BracketMap.ContainsKey(c)) {
                        chunks.Push(c);
                    } else {
                        char opening = chunks.Peek();
                        if (c == BracketMap[opening]) {
                            chunks.Pop();
                        } else {
                            corruptedCharacters.Add(c);
                            complete = false;
                            break;
                        }
                    }
                }
                if (!complete) {
                    continue;
                }

                long lineScore = 0;
                while (chunks.Any()) {
                    char incompleteChunk = chunks.Pop();
                    lineScore = lineScore * 5 + ScoreTableIncomplete[incompleteChunk];
                }
                incompleteLineScores.Add(lineScore);
            }

            int result = corruptedCharacters.Sum(c => ScoreTableCorrupted[c]);
            Console.WriteLine($"There were {corruptedCharacters.Count} corrupted lines with a total score of {result} points");

            long middleScore = incompleteLineScores.OrderBy(i => i).Skip(Convert.ToInt32(Math.Round(incompleteLineScores.Count / 2d, 0, MidpointRounding.ToZero))).First();
            Console.WriteLine($"There were {incompleteLineScores.Count} incomplete lines with a middle score of {middleScore} points");
        }
    }
}