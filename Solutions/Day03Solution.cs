namespace AdventOfCode2021.Solutions {
    public class Day03Solution : Solution {
        public override List<Action> Stages => new List<Action> { Stage1, Stage2 };

        private void Stage1() {
            List<string> lines = ReadInputFile("Day03Stage01.txt");
            int maxLength = lines.Max(line => line.Length);
            string gammaBinary = "";
            string epsilonBinary = "";
            for (int i = 0; i < maxLength; i++) {
                int oneBits = lines.Count(line => int.Parse(line[i].ToString()) == 1);
                gammaBinary += oneBits > lines.Count / 2 ? "1" : "0";
                epsilonBinary += oneBits > lines.Count / 2 ? "0" : "1";
            }
            int gamma = Convert.ToInt32(gammaBinary, 2);
            int epsilon = Convert.ToInt32(epsilonBinary, 2);
            int result = gamma * epsilon;

            Console.WriteLine($"Gamma: {gamma}; Epsilon: {epsilon}; Result: {result}");
        }

        private void Stage2() {
            List<string> oxyLines = ReadInputFile("Day03Stage02.txt");
            List<string> co2Lines = ReadInputFile("Day03Stage02.txt");
            int maxLength = oxyLines.Max(line => line.Length);
            for (int i = 0; i < maxLength; i++) {
                int oxyOneBits = oxyLines.Count(line => int.Parse(line[i].ToString()) == 1);
                int co2OneBits = co2Lines.Count(line => int.Parse(line[i].ToString()) == 1);

                if (oxyLines.Count > 1) {
                    oxyLines.RemoveAll(line => line[i] == (oxyOneBits >= oxyLines.Count - oxyOneBits ? '0' : '1'));
                }
                if (co2Lines.Count > 1) {
                    co2Lines.RemoveAll(line => line[i] == (co2OneBits >= co2Lines.Count - co2OneBits ? '1' : '0'));
                }
                if (co2Lines.Count == 1 && oxyLines.Count == 1) {
                    break;
                }
            }
            int oxy = Convert.ToInt32(oxyLines.First(), 2);
            int co2 = Convert.ToInt32(co2Lines.First(), 2);
            int result = oxy * co2;
            Console.WriteLine($"Oxygen: {oxy}; CO2: {co2}; Result: {result}");

        }
    }
}