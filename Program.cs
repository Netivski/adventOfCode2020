using System;
using System.Diagnostics;

namespace AdventOfCode {
    class Program {

        static void Main(string[] args) {
            string day = "Day 16";
            //ExecutePart(Day16.First , string.Format("{0} Pt1", day));
            ExecutePart(Day16.Second, string.Format("{0} Pt2", day));
        }

        static void ExecutePart(Action action, string msg) {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            System.Console.WriteLine("{0} : {1} seconds\n", msg, (double)sw.ElapsedMilliseconds / 1000);
        }

    }
}
