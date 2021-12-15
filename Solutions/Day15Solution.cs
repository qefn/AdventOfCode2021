using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace AdventOfCode2021.Solutions {
    public class Day15Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };
        private void Stage1() {
            Solve("Day15Stage01.txt", false);
        }
        private void Stage2() {
            Solve("Day15Stage02.txt", true);
        }
        private void Solve(string inputFile, bool inflateInput) {
            List<string> input = ReadInputFile(inputFile);
            Graph<Node, string> graph = new Graph<Node, string>();
            int gridWidth = input.First().Length;
            int gridHeight = input.Count;
            Node[,] grid = new Node[gridHeight, gridWidth];
            Enumerable.Range(0, gridHeight)
                .ForEach(y => Enumerable.Range(0, gridWidth)
                                .ForEach(x => grid[y, x] = new Node(int.Parse(input[y][x].ToString()))));

            if (inflateInput) {
                int inflateRate = 5;
                grid = InflateArray(grid, inflateRate);
                gridHeight *= inflateRate;
                gridWidth *= inflateRate;
            }

            Enumerable.Range(0, gridHeight)
                .ForEach(y => Enumerable.Range(0, gridWidth)
                    .ForEach(x => grid[y, x].Id = graph.AddNode(grid[y, x])));

            Enumerable.Range(0, gridHeight)
                .ForEach(y => Enumerable.Range(0, gridWidth)
                                .ForEach(x => {
                                    Connect(grid[y, x], graph, grid, x - 1, y);
                                    Connect(grid[y, x], graph, grid, x + 1, y);
                                    Connect(grid[y, x], graph, grid, x, y - 1);
                                    Connect(grid[y, x], graph, grid, x, y + 1);
                                }));
            ShortestPathResult result = graph.Dijkstra(grid[0, 0].Id, grid[gridHeight - 1, gridWidth - 1].Id);
            Console.WriteLine($"The lowest risk path has a risk of {result.Distance}");
        }

        private Node[,] InflateArray(Node[,] grid, int inflateRate) {
            int gridHeight = grid.GetLength(0);
            int gridWidth = grid.GetLength(1);
            Dictionary<int, Node[,]> grids = new Dictionary<int, Node[,]>();
            grids[0] = grid;
            for (int i = 1; i <= 2 * inflateRate - 2; i++) {
                Node[,] increasedValueGrid = new Node[gridHeight, gridWidth];
                grid.CopyTo(increasedValueGrid, 0, 0, from => {
                    int cost = from.Cost + i;
                    cost = cost > 9 ? cost - 9 : cost;
                    return new Node(cost);
                });
                grids[i] = increasedValueGrid;
            }
            Node[,] inflatedGrid = new Node[5 * gridHeight, 5 * gridWidth];
            for (int y = 0; y < inflateRate; y++) {
                for (int x = 0; x < inflateRate; x++) {
                    grids[y + x].CopyTo(inflatedGrid, y * gridHeight, x * gridWidth, from => from);
                }
            }
            return inflatedGrid;
        }

        private void Connect(Node from, Graph<Node, string> graph, Node[,] grid, int x, int y) {
            if (!grid.IsInBounds(y, x)) {
                return;
            }

            Node to = grid[y, x];
            graph.Connect(from.Id, to.Id, to.Cost, string.Empty);
        }

        private class Node {
            public Node(int cost) {
                Cost = cost;
            }

            public int Cost { get; set; }

            public uint Id { get; set; }
        }
    }
}