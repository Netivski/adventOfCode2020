using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
    class Day1 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day1.txt"));
            var vals = new HashSet<int>(lines);
            var sumTotal = 2020;

            for (int i = 0; i < lines.Length; i++) {
                if (i == 0) { vals.Add(lines[i]); }
                int match = sumTotal - lines[i];
                if (vals.Contains(match)) {
                    Console.WriteLine("Found it! {0}+{1}={2}", lines[i], match, sumTotal);
                    Console.WriteLine("Answer is {0}*{1}={2}", lines[i], match, lines[i] * match);
                    return;
                }
            }
        }

        public static void Second() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day1.txt"));
            for (int i = 0; i < lines.Length; i++) {
                var left = lines[i];
                for (int j = i + 1; j < lines.Length; j++) {
                    var right = lines[j];
                    for (int k = j + 1; k < lines.Length; k++) {
                        var righter = lines[k];
                        if (left + right + righter == 2020) {
                            Console.WriteLine("Found it! {0}+{1}+{2}={3}", left, right, righter, left + right + righter);
                            Console.WriteLine("Answer is {0}*{1}*{2}={3}", left, right, righter, left * right * righter);
                            return;
                        }
                    }
                }
            }
        }
    }
}
