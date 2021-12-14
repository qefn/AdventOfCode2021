namespace AdventOfCode2021.Solutions {
    public class Day14Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };
        private void Stage1() {
            Solve(10);
        }

        private void Stage2() {
            Solve(40);
        }

        private void Solve(int steps) {
            List<string> input = ReadInputFile("Day14Stage01.txt");
            string poly = input.First();
            Dictionary<string, char> insertions = input.Skip(2).ToDictionary(i => i.Split(" -> ")[0], i => i.Split(" -> ")[1][0]);
            Dictionary<string, long> PairOccurences = Enumerable.Range(0, poly.Length - 1).Select(i => $"{poly[i]}{poly[i + 1]}").GroupBy(s => s).ToDictionary(g => g.Key, g => g.LongCount()); // count all pairs
            for (int i = 0; i < steps; i++) {
                PairOccurences = PairOccurences.SelectMany(p => new List<Tuple<string, long>>{
                    new Tuple<string, long>($"{p.Key[0]}{insertions[p.Key]}", p.Value),
                    new Tuple<string, long>($"{insertions[p.Key]}{p.Key[1]}", p.Value)
                }) //each pair spawns 2 new pairs with the inserted character
                .GroupBy(p => p.Item1)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Item2)); //sum the counts of each pair
            }
            Dictionary<char, long> elementOccurences = PairOccurences.Select(p => new Tuple<char, long>(p.Key[0], p.Value)).GroupBy(p => p.Item1).ToDictionary(g => g.Key, g => g.Sum(p => p.Item2)); //group by the first character of each pair and sum the counts => this gives us the usage count of each character. would also work with taking the second character of each pair. However don't take both, otherwise we count elemts twice
            elementOccurences[poly.Last()]++; //last character in the input string is off by one, so increase it
            Console.WriteLine($"The result is: {elementOccurences.Values.Max() - elementOccurences.Values.Min()}");
        }

        /*
        Short step by step example (Because I will for sure forget how this worked xD):
        Initial string: SHO
        Step 1:

        Pairs:  SH 1
                HO 1
        By sdumming the counts of each first character we get the following counts
        Character counts:   S 1
                            H 1
                            O 1 (as said earlier the last chatacter is off by one, so we always add 1)

        Step 2:
        A and H are added in the pairs respectively (does not comply with the puzzle input, I just made those up)
        Resulting string: SAHHO

        Pairs:  SA 1
                AH 1
                HH 1
                HO 1

        By sdumming the counts of each first character we get the following counts
        Character counts:   S 1
                            A 1
                            H 2
                            O 1 (as said earlier the last chatacter is off by one, so we always add 1)

        Step 2:
        E, I, H and L are added in the pairs respectively (does not comply with the puzzle input, I just made those up)
        Resulting string: SEAIHHHLO

        Pairs:  SE 1
                EA 1
                AI 1
                IH 1
                HH 2
                HL 1
                LO 1
        
        By sdumming the counts of each first character we get the following counts
        Character counts:   S 1
                            E 1
                            A 1
                            I 1
                            H 3
                            L 1
                            O 1 (as said earlier the last chatacter is off by one, so we always add 1)
        
        ....
        */
    }
}