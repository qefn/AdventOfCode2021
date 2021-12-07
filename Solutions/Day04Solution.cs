namespace AdventOfCode2021.Solutions {
    public class Day04Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Stage1() {
            List<string> lines = ReadInputFile("Day04Stage01.txt");
            Tuple<List<int>, List<Board>> parsed = ParseInput(lines);
            List<int> drawnNumbers = parsed.Item1;
            List<Board> boards = parsed.Item2;

            foreach (int number in drawnNumbers) {
                Board? winningBoard = boards.FirstOrDefault(board => board.ApplyNumber(number));
                if (winningBoard != null) {
                    int result = winningBoard.UnMarkedSum * number;
                    Console.WriteLine($"Winner Ummarked Sum: {winningBoard.UnMarkedSum}; Current numver: {number}; Result: {result}");
                    break;
                }
            }
        }

        private void Stage2() {
            List<string> lines = ReadInputFile("Day04Stage02.txt");
            Tuple<List<int>, List<Board>> parsed = ParseInput(lines);
            List<int> drawnNumbers = parsed.Item1;
            List<Board> boards = parsed.Item2;

            foreach (int number in drawnNumbers) {
                if (boards.Count > 1) {
                    boards.RemoveAll(board => board.ApplyNumber(number));
                } else {
                    Board losingBoard = boards.First();
                    bool won = losingBoard.ApplyNumber(number);
                    if (won) {
                        int result = losingBoard.UnMarkedSum * number;
                        Console.WriteLine($"Loser Ummarked Sum: {losingBoard.UnMarkedSum}; Current numver: {number}; Result: {result}");
                        break;
                    }
                }
            }
        }

        private Tuple<List<int>, List<Board>> ParseInput(List<string> lines) {
            List<int> drawnNumbers = lines[0].Split(",").Select(numStr => int.Parse(numStr)).ToList();
            List<Board> boards = new List<Board>();
            foreach (string line in lines.Skip(1)) {
                if (line == string.Empty) {
                    boards.Add(new Board());
                    continue;
                }
                boards.Last().AddRow(line);
            }

            return new Tuple<List<int>, List<Board>>(drawnNumbers, boards);
        }

        private class Board {
            private bool AnyColumnWins => Columns.Any(column => column.All(cell => cell.Marked));

            private bool AnyRowWins => Rows.Any(row => row.All(cell => cell.Marked));

            public List<List<Cell>> Columns { get; } = new List<List<Cell>>();

            public bool IsWinner => AnyRowWins || AnyColumnWins;

            public List<List<Cell>> Rows { get; } = new List<List<Cell>>();

            public int UnMarkedSum => Rows.Sum(row => row.Sum(cell => cell.Marked ? 0 : cell.Number));

            public void AddRow(string rowString) {
                List<Cell> row = rowString.Split(" ").Where(str => !string.IsNullOrEmpty(str)).Select(numStr => new Cell(int.Parse(numStr))).ToList();
                for (int i = 0; i < row.Count; i++) {
                    if (Columns.Count <= i) {
                        Columns.Add(new List<Cell>());
                    }
                    Columns[i].Add(row[i]);
                }
                Rows.Add(row);
            }

            public bool ApplyNumber(int number) {
                Rows.ForEach(row => row.ForEach(cell => cell.Marked |= cell.Number == number));
                return IsWinner;
            }
        }

        public class Cell {
            public Cell(int number) {
                Number = number;
            }

            public bool Marked { get; set; } = false;

            public int Number { get; set; }
        }
    }


}