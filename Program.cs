using System.Diagnostics;

namespace AdventOfCode {
    class Program {

        static void Main(string[] args) {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Day14.First();
            Day14.Second();
            sw.Stop();
            System.Console.WriteLine("Problem took {0} seconds to execute", (double)sw.ElapsedMilliseconds / 1000);

        }
    }
}
