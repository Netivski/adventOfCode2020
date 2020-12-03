using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace AdventOfCode {
    class Day3 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static int TraverseMap(Point slope) {
            int numTrees = 0, numMoves = 0;

            var matrix = Utils.ReadCharMatrix(Path.Combine(Inputs, "Day3.txt"));
            int maxCols = matrix[0].Length;
            int maxLines = matrix.Length - 1;

            Point p = new Point(0, 0);
            while (p.Y < maxLines) {
                numMoves++;
                p.X = p.X + slope.X;
                p.Y = p.Y + slope.Y;
                if (p.Y > maxLines) { p.Y = maxLines; }
                if (matrix[p.Y][p.X % maxCols] == '#') {
                    numTrees++;
                    matrix[p.Y][p.X % maxCols] = 'X';
                } else {
                    matrix[p.Y][p.X % maxCols] = 'O';
                }
            }
            //Console.WriteLine("Number of moves is: {0}", numMoves);
            //Console.WriteLine("Number of trees is: {0}", numTrees);
            return numTrees;
        }

        public static void First() {
            Console.WriteLine("Number of trees is: {0}", TraverseMap(new Point(3, 1)));
        }
        public static void Second() {
            //Right 1, down 1.
            //Right 3, down 1. (This is the slope you already checked.)
            //Right 5, down 1.
            //Right 7, down 1.
            //Right 1, down 2.
            int a = TraverseMap(new Point(1, 1));
            int b = TraverseMap(new Point(3, 1));
            int c = TraverseMap(new Point(5, 1));
            int d = TraverseMap(new Point(7, 1));
            int e = TraverseMap(new Point(1, 2));
            long total = (long) a * b * c * d * e;
            Console.WriteLine("Total number of trees multiplied is {0}", total);

        }
    }
}
