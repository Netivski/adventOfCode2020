using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    class Utils {
        public static IEnumerable<string> ReadLines(string path) {
            return File.ReadLines(path);
        }

        public static int[] ReadIntLines(string path) {
            
            var lines = File.ReadAllLines(path);
            int[] results = new int[lines.Length];
            for (int i=0; i<lines.Length; i++) {
                results[i] = int.Parse(lines[i]);
            }
            return results;
        }

        public static char[][] ReadCharMatrix(string path) {

            IList<char[]> lines = new List<char[]>();
            int numCols, numLines;

            using (var s = new StreamReader(path)) {
                string line;
                while ((line = s.ReadLine()) != null) {
                    numCols = line.Length;
                    lines.Add(line.ToCharArray());
                }
            }
            return lines.ToArray();

        }
    }
}
