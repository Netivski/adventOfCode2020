﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day11 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static Point Up = new Point(0, -1);
        static Point Down = new Point(0, 1);
        static Point Left = new Point(-1, 0);
        static Point Right = new Point(1, 0);
        static Point UpLeft = new Point(-1, -1);
        static Point UpRight = new Point(1, -1);
        static Point DownLeft = new Point(-1, 1);
        static Point DownRight = new Point(1, 1);

        static void PrintSeating(char[][]seats) {
            Console.WriteLine();
            for (int i=0; i<seats.Length; i++) {
                for (int j=0; j<seats[i].Length; j++) {
                    Console.Write(seats[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static int CountOccupied(char[][] seats) {
            int count = 0;
            for (int i = 0; i < seats.Length; i++) {
                for (int j = 0; j < seats[i].Length; j++) {
                    if (seats[i][j] == '#') { count++; };
                }
            }
            return count;
        }

        static bool AreSeatingsEqual(char[][] one, char[][] another) {
            for (int i = 0; i < one.Length; i++) {
                if (!new string(one[i]).Equals(new string(another[i]))) {
                    return false;
                }
            }
            return true;
        }

        static void IsAdjacentOccupied(char[][] matrix, Point seat, out int adjCount) {
            int maxCols = matrix[0].Length;
            int maxRows = matrix.Length;
            adjCount = 0;

            List<Point> area = new List<Point>() {
                new Point(seat.X + Up.X, seat.Y + Up.Y),
                new Point(seat.X + Down.X, seat.Y + Down.Y),
                new Point(seat.X + Right.X, seat.Y + Right.Y),
                new Point(seat.X + Left.X, seat.Y + Left.Y),
                new Point(seat.X + UpRight.X, seat.Y + UpRight.Y),
                new Point(seat.X + UpLeft.X, seat.Y + UpLeft.Y),
                new Point(seat.X + DownRight.X, seat.Y + DownRight.Y),
                new Point(seat.X + DownLeft.X, seat.Y + DownLeft.Y),
            };

            foreach (Point p in area) {
                if (p.X >= 0 && p.X < maxCols && p.Y >= 0 && p.Y < maxRows) {
                    if (matrix[p.Y][p.X] == '#') { adjCount++; }
                }
            }

        }

        static void IncreaseBoundary(ref Point[] points) {
            points[0].X += UpLeft.X; points[0].Y += UpLeft.Y;
            points[1].X += Up.X; points[1].Y += Up.Y;
            points[2].X += UpRight.X; points[2].Y += UpRight.Y;
            points[3].X += Left.X; points[3].Y += Left.Y;
            points[4].X += Right.X; points[4].Y += Right.Y;
            points[5].X += DownLeft.X; points[5].Y += DownLeft.Y;
            points[6].X += Down.X; points[6].Y += Down.Y;
            points[7].X += DownRight.X; points[7].Y += DownRight.Y;
        }

        static bool IsFullyOutsideBounds(Point[] points, int maxCols, int maxRows) {
            int outsideCount = 0;
            for (int i = 0; i < points.Length; i++) {
                if (points[i].X >= maxCols || points[i].X < 0 
                    || points[i].Y < 0 || points[i].Y >= maxRows) { 
                    outsideCount++; 
                }
            }
            return outsideCount == 8;
        }

        static void IsAdjacentOccupiedV2(char[][] matrix, Point seat, out int adjCount) {
            int maxCols = matrix[0].Length;
            int maxRows = matrix.Length;
            adjCount = 0;
            //Starting from upper left clockwise
            //   012
            //   3 4
            //   567

            Point[] boundary = new Point[] {
                new Point(seat.X + UpLeft.X, seat.Y + UpLeft.Y),
                new Point(seat.X + Up.X, seat.Y + Up.Y),
                new Point(seat.X + UpRight.X, seat.Y + UpRight.Y),
                new Point(seat.X + Left.X, seat.Y + Left.Y),
                new Point(seat.X + Right.X, seat.Y + Right.Y),
                new Point(seat.X + DownLeft.X, seat.Y + DownLeft.Y),
                new Point(seat.X + Down.X, seat.Y + Down.Y),
                new Point(seat.X + DownRight.X, seat.Y + DownRight.Y),
            };
            int[] occupied = new int[8]; // 0 - empty | 1 - free | 2 - occupied

            while (!IsFullyOutsideBounds(boundary, maxCols, maxRows)) {
                for (int i = 0; i < boundary.Length; i++) {

                    if (boundary[i].X >= 0 && boundary[i].X < maxCols && boundary[i].Y >= 0 && boundary[i].Y < maxRows) {
                        if (matrix[boundary[i].Y][boundary[i].X] == '#') {
                            if (occupied[i] == 0) {
                                occupied[i] = 2;
                                adjCount++;
                            }
                        } else if (matrix[boundary[i].Y][boundary[i].X] == 'L') {
                            if (occupied[i] == 0) {
                                occupied[i] = 1;
                            }
                        }
                    }
                }
                IncreaseBoundary(ref boundary);
            }
        }

        public static void First() {
            var roundSeating = Utils.ReadCharMatrix(Path.Combine(Inputs, "Day11.txt"));
            int maxCols = roundSeating[0].Length;
            int maxLines = roundSeating.Length;

            Point p = new Point();

            char[][] roundCopy = roundSeating.Select(a => a.ToArray()).ToArray();
            int iterCount = 0;
            bool seatingChanged = true;

            while (seatingChanged) {
                iterCount++;
                p.X = 0;
                p.Y = 0;
                while (p.Y < maxLines) {

                    int adjCount = 0;
                    IsAdjacentOccupied(roundSeating, p, out adjCount);

                    if (roundSeating[p.Y][p.X] == 'L' && adjCount == 0) {
                        roundCopy[p.Y][p.X] = '#';
                    } else if (roundSeating[p.Y][p.X] == '#' && adjCount >= 4) {
                        roundCopy[p.Y][p.X] = 'L';
                    }
                    p.Y += (p.X + 1) == maxCols ? 1 : 0;
                    p.X += 1;
                    p.X = p.X % maxCols;
                }
                if (AreSeatingsEqual(roundCopy, roundSeating)) {
                    seatingChanged = false;
                } 
                    roundSeating = roundCopy.Select(a => a.ToArray()).ToArray();
            }
            Console.WriteLine("Number of occupied seats: {0}", CountOccupied(roundSeating));
        }

        public static void Second() {
            var roundSeating = Utils.ReadCharMatrix(Path.Combine(Inputs, "Day11.txt"));
            int maxCols = roundSeating[0].Length;
            int maxLines = roundSeating.Length;

            Point p = new Point();

            char[][] roundCopy = roundSeating.Select(a => a.ToArray()).ToArray();
            int iterCount = 0;
            bool seatingChanged = true;
            //PrintSeating(roundSeating);
            while (seatingChanged) {
                
                iterCount++;
                p.X = 0;
                p.Y = 0;
                while (p.Y < maxLines) {

                    int adjCount = 0;
                    IsAdjacentOccupiedV2(roundSeating, p, out adjCount);

                    if (roundSeating[p.Y][p.X] == 'L' && adjCount == 0) {
                        roundCopy[p.Y][p.X] = '#';
                    } else if (roundSeating[p.Y][p.X] == '#' && adjCount >= 5) {
                        roundCopy[p.Y][p.X] = 'L';
                    }
                    p.Y += (p.X + 1) == maxCols ? 1 : 0;
                    p.X += 1;
                    p.X = p.X % maxCols;
                }
                //PrintSeating(roundCopy);
                if (AreSeatingsEqual(roundCopy, roundSeating)) {
                    seatingChanged = false;
                }
                roundSeating = roundCopy.Select(a => a.ToArray()).ToArray();
            }
            Console.WriteLine("Number of occupied seats: {0}", CountOccupied(roundSeating));

        }
    }
}