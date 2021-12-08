namespace AdventOfCode2021.Solutions {
    public class Day08Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Stage1() {
            List<string> input = ReadInputFile("Day08Stage01.txt");
            List<string> output = input.SelectMany(line => line.Split(" | ").ElementAt(1).Split(" ")).ToList();
            int result = output.Count(val => val.Length == 2 || val.Length == 3 || val.Length == 4 || val.Length == 7);
            Console.WriteLine($"There are {result} simple numbers");
        }

        private void Stage2() {
            List<string> input = ReadInputFile("Day08Stage02.txt");
            List<Tuple<string[], string[]>> patterns = input.Select(line => new Tuple<string[], string[]>(line.Split(" | ")[0].Split(" "), line.Split(" | ")[1].Split(" "))).ToList();
            List<int> overallResult = new List<int>();

            foreach (Tuple<string[], string[]> pattern in patterns) {
                Dictionary<int, PatternInfo> patternMap = new Dictionary<int, PatternInfo> {
                    { 0, new PatternInfo(new List<int> { 1, 2, 3, 4, 5, 6, 7, 9 }, new List<int> { 1, 2, 3, 5, 6, 7 }) },
                    { 1, new PatternInfo(new List<int> { 2, 5, 6 }, new List<int> { 3, 6 }) },
                    { 2, new PatternInfo(new List<int> { 0, 1, 3, 4, 5, 6, 7, 9 }, new List<int> { 1, 3, 4, 5, 7 }) },
                    { 3, new PatternInfo(new List<int> { 0, 1, 2, 4, 5, 6, 7 }, new List<int> { 1, 3, 4, 6, 7 }) },
                    { 4, new PatternInfo(new List<int> { 0, 1, 2, 3, 5, 6, 7}, new List<int> { 2, 3, 4, 6 }) },
                    { 5, new PatternInfo(new List<int> { 0, 1, 2, 3, 4, 7, 9}, new List<int> { 1, 2, 4, 6, 7 }) },
                    { 6, new PatternInfo(new List<int> { 0, 1, 2, 3, 4, 5, 7, 9 }, new List<int> { 1, 2, 4, 5, 6, 7 }) },
                    { 7, new PatternInfo(new List<int> { 1, 2, 4, 5, 6}, new List<int> { 1, 3, 6 }) },
                    { 8, new PatternInfo(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9 }, new List<int> { 1, 2, 3, 4, 5, 6, 7 }) },
                    { 9, new PatternInfo(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }, new List<int> { 1, 2, 3, 4, 6, 7 }) }
                };
                Dictionary<int, List<char>> segmentMap = new Dictionary<int, List<char>> {
                {1, new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'}},
                {2, new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'}},
                {3, new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'}},
                {4, new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'}},
                {5, new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'}},
                {6, new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'}},
                {7, new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'}}
            };
                List<Tuple<string, List<int>>> possibleNumbers = pattern.Item1.Select(item => {
                    List<int> possible = new List<int>();
                    switch (item.Length) {
                        case 2:
                            possible = new List<int> { 1 };
                            break;
                        case 3:
                            possible = new List<int> { 7 };
                            break;
                        case 4:
                            possible = new List<int> { 4 };
                            break;
                        case 5:
                            possible = new List<int> { 2, 3, 5 };
                            break;
                        case 6:
                            possible = new List<int> { 0, 6, 9 };
                            break;
                        case 7:
                            possible = new List<int> { 8 };
                            break;
                    }
                    return new Tuple<string, List<int>>(item, possible);
                }).ToList();
                string? onePattern = pattern.Item1.FirstOrDefault(item => item.Length == 2);
                string? sevenPattern = pattern.Item1.FirstOrDefault(item => item.Length == 3);
                string? fourPattern = pattern.Item1.FirstOrDefault(item => item.Length == 4);
                string? eightPattern = pattern.Item1.FirstOrDefault(item => item.Length == 7);

                if (onePattern != null) {
                    patternMap[1].Pattern = onePattern;
                    if (sevenPattern != null) {
                        char segmentOne = sevenPattern.Except(onePattern).First();
                        segmentMap[1] = new List<char> { segmentOne };
                        segmentMap.Skip(1).ForEach(kvp => kvp.Value.Remove(segmentOne));
                    }
                    if (fourPattern != null) {
                        List<char> segmentTwoOrFour = fourPattern.Except(onePattern).ToList();
                        segmentMap[2] = new List<char>(segmentTwoOrFour);
                        segmentMap[4] = new List<char>(segmentTwoOrFour);
                        for (int i = 1; i <= 7; i++) {
                            if (i == 2 || i == 4) {
                                segmentMap[i] = new List<char>(segmentTwoOrFour);
                            } else {
                                segmentMap[i].RemoveAll(c => segmentTwoOrFour.Contains(c));
                            }
                        }
                    }
                }
                if (fourPattern != null) {
                    patternMap[4].Pattern = fourPattern;
                }
                if (sevenPattern != null) {
                    patternMap[7].Pattern = sevenPattern;
                }
                if (eightPattern != null) {
                    patternMap[8].Pattern = eightPattern;
                }

                while (!pattern.Item2.All(output => patternMap.Any(mapped => output.Length == mapped.Value.Pattern.Length && output.All(c => mapped.Value.Pattern.Contains(c))))) {
                    foreach (Tuple<string, List<int>> item in possibleNumbers.Where(tuple => tuple.Item2.Count != 1)) {
                        foreach (KeyValuePair<int, PatternInfo> patternPair in patternMap.Where(kvp => kvp.Value.Pattern != "")) {
                            ExcludePossibleNumbers(patternPair.Value.Pattern, patternPair.Key, patternPair.Value.ExcludedNumbers, item, patternMap, segmentMap);
                        }
                    }
                }
                List<int> result = pattern.Item2.Select(output => patternMap.First(mapped => output.Length == mapped.Value.Pattern.Length && output.All(c => mapped.Value.Pattern.Contains(c))).Key).ToList();
                Console.WriteLine($"Done! {result[0]}{result[1]}{result[2]}{result[3]}");
                overallResult.Add(int.Parse($"{result[0]}{result[1]}{result[2]}{result[3]}"));
            }
            Console.WriteLine($"Overall result: {overallResult.Sum()}");
        }

        private void ExcludePossibleNumbers(string pattern, int patternNumber, IEnumerable<int> patternExcludedNumbers, Tuple<string, List<int>> possible, Dictionary<int, PatternInfo> patternMap, Dictionary<int, List<char>> segmentMap) {
            if (pattern.All(c => possible.Item1.Contains(c))) {
                if (possible.Item1.Length == pattern.Length) {
                    possible.Item2.Clear();
                    possible.Item2.Add(patternNumber);
                } else if (pattern.All(c => possible.Item1.Contains(c))) {
                    possible.Item2.RemoveAll(n => patternExcludedNumbers.Contains(n));
                }
            } else {
                possible.Item2.Remove(patternNumber);
            }

            if (possible.Item2.Count == 1 && patternMap[possible.Item2.First()].Pattern == "") {
                PatternInfo patternInfo = patternMap[possible.Item2.First()];
                patternInfo.Pattern = possible.Item1;
            }

            foreach (PatternInfo patternInfo in patternMap.Values.Where(map => map.Pattern != "")) {
                List<char> unidentifiedSegments = patternInfo.Pattern.ToList().Where(c => segmentMap.Values.All(segment => segment.Count != 1 || segment.First() != c)).ToList();
                List<int> identifiedSegments = patternInfo.Pattern.ToList().Select(c => segmentMap.FirstOrDefault(segment => segment.Value.Count == 1 && segment.Value.First() == c).Key).Where(i => i > 0).ToList();
                for (int i = 1; i <= 7; i++) {
                    if (identifiedSegments.Contains(i)) {
                        continue;
                    }
                    if (patternInfo.UsedSegments.Contains(i)) {
                        segmentMap[i].RemoveAll(c => !unidentifiedSegments.Contains(c));
                    } else {
                        segmentMap[i].RemoveAll(c => unidentifiedSegments.Contains(c));
                    }
                }
            }

            foreach (PatternInfo patternInfo in patternMap.Values.Where(map => map.Pattern == "")) {
                string foundPattern = "";
                bool allFound = true;
                foreach (int segment in patternInfo.UsedSegments) {
                    if (segmentMap[segment].Count == 1) {
                        foundPattern += segmentMap[segment].First().ToString();
                    } else {
                        allFound = false;
                    }
                }
                if (allFound) {
                    patternInfo.Pattern = foundPattern;
                }
            }
        }

        private class PatternInfo {
            public PatternInfo(List<int> excludedNumbers, List<int> usedSegments) {
                ExcludedNumbers = excludedNumbers;
                UsedSegments = usedSegments;
            }
            public string Pattern { get; set; } = "";

            public List<int> ExcludedNumbers { get; set; }

            public List<int> UsedSegments { get; set; }
        }
    }
}