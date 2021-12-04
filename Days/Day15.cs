using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day15 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static readonly int[] Inputs = new int[] { 5, 1, 9, 18, 13, 8, 0 };
        public static readonly int[] Test = new int[] { 0, 3, 6 };
        public static readonly int[] Test1 = new int[] { 1, 3, 2 };
        public static readonly int[] Test2 = new int[] { 2, 1, 3 };
        public static readonly int[] Test3 = new int[] { 1, 2, 3 };
        public static readonly int[] Test4 = new int[] { 2, 3, 1 };
        public static readonly int[] Test5 = new int[] { 3, 2, 1 };
        public static readonly int[] Test6 = new int[] { 3, 1, 2 };


        public static void First() {
            var input = Inputs;

            Dictionary<int, int> turns = new Dictionary<int, int>();

            int previousRound = 0;
            int prevprevRound;
            int round = 1;
            for (int i = 0; i < input.Length; i++) {
                turns.Add(input[i], round);
                previousRound = input[i];
                round++;
            }

            while (round <= 30000000) {
                if (turns.ContainsKey(previousRound)) {
                    if (round == input.Length + 1) {
                        previousRound = 0;
                        round++;
                        continue;
                    }
                    prevprevRound = previousRound;
                    previousRound = (round - 1) - turns[previousRound];
                    turns[prevprevRound] = round - 1;
                } else {
                    turns[previousRound] = round - 1;
                    previousRound = 0;
                }
                round++;
            }

            Console.WriteLine("Exited at round {0}", previousRound);
        }

        public static void Second() {

        }
    }
}