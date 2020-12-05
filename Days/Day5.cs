using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day5 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static int FindMiddle(double max, char[] ops, char upChar, char downChar) {
            double min = 0;
            for (int i = 0; i < ops.Length; i++) {
                if (ops[i] == downChar) {
                    max = max - ((max - min) / 2);
                } else {
                    min = min + ((max - min) / 2);
                }
            }
            return (int)max;
        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "test.txt"));

            var maxId = 0;
            char[] rowsArr = new char[7];
            char[] colsArr = new char[3];

            char[] seats = new char[1024];

            foreach (string line in lines) {
                Array.Copy(line.ToCharArray(), 0, rowsArr, 0, 7);
                var row = FindMiddle(127, rowsArr, 'B', 'F');

                Array.Copy(line.ToCharArray(), 7, colsArr, 0, 3);
                var seat = FindMiddle(7, colsArr, 'R', 'L');

                int currId = (row * 8) + seat;
                seats[currId] = 'X';

                if ((currId) > maxId) { maxId = currId; }
            }
            Console.WriteLine("MaxId is: {0}", maxId);

            for (int i = 8; i < 1016; i++) {
                if (seats[i] == default(char) && seats[i - 1] == 'X' && seats[i + 1] == 'X') {
                    Console.WriteLine("Found a seat: {0}", i);
                }

            }


        }
        public static void Second() {
            First();
        }
    }
}
