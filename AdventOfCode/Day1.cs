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
            for (int i = 0; i < lines.Length - 1; i++) {
                var left = lines[i];
                for (int j = i + 1; j < lines.Length; j++) {
                    var right = lines[j];
                    if (left + right == 2020) {
                        Console.WriteLine("Found it! {0}+{1}={2}", left, right, left + right);
                        Console.WriteLine("Answer is {0}*{1}={2}", left, right, left * right);
                        return;
                    }
                }
            }
        }
        public static void Second() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day1.txt"));
            for (int i = 0; i < lines.Length - 1; i++) {
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
