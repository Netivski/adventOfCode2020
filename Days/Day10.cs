using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day10 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day10.txt"));
            HashSet<int> l = new HashSet<int>(lines);

            List<int> list = new List<int>(lines);
            list.Sort();

            int currJolts = 0;
            int[] diffCounter = new int[3];

            for (int i = 0; i < list.Count(); i++) {
                if (list[i] == currJolts + 1) {
                    diffCounter[0]++;
                    currJolts = list[i];
                } else if (list[i] == currJolts + 2) {
                    diffCounter[1]++;
                    currJolts = list[i];
                } else if (list[i] == currJolts + 3) {
                    diffCounter[2]++;
                    currJolts = list[i];
                }
            }
            Console.WriteLine("Difference of 1 count is {0}", diffCounter[0]);
            Console.WriteLine("Difference of 3 count is {0}", diffCounter[2] + 1);


        }

        static long CountIt(List<int> list) {
            Dictionary<int, long> count = new Dictionary<int, long>();
            count[list.Count() - 1] = 1;
            
            for (int i=list.Count - 2; i>=0; i--) {
                long currCount = 0;
                for (int j=i+1; j<list.Count; j++) {
                    if (!(list[j] - list[i] <= 3)) { break; }
                    currCount += count[j];
                }
            count[i] = currCount;
            }
            return count[0];
        }

        static long Launch(int currJolts, List<int> adapters, int currPos) {
            long x1 = 0, x2 = 0, x3 = 0;

            Thread thread1 = new Thread(() => { x1 = DoCount(currJolts, adapters, currPos + 1); });
            thread1.Start();
            thread1.Join();

            Thread thread2 = new Thread(() => { x2 = DoCount(currJolts, adapters, currPos + 2); });
            thread2.Start();
            thread2.Join();

            Thread thread3 = new Thread(() => { x3 = DoCount(currJolts, adapters, currPos + 3); });
            thread3.Start();
            thread3.Join();

            return x1 + x2 + x3;

        }

        static long DoCount(int currJolts, List<int> adapters, int currPos) {

            long x = 0;
            bool foundIt = false;

            if (currPos >= adapters.Count()) {
                //Console.WriteLine("*OUTOFBOUNDS* CurJolts: {0} | Pos: {1}", currJolts, pos);
                return 0;
            }
            int newJolts = 0;
            if (adapters[currPos] == currJolts + 1) {
                //Console.WriteLine("CurJolts: {0} | Pos: {1} | CurrValue: {2}", currJolts, currPos, adapters[currPos]);
                //Console.Write(" {0} - ", currJolts); 
                foundIt = true;
                newJolts = adapters[currPos];
                x += DoCount(newJolts, adapters, currPos + 1);
                x += DoCount(newJolts, adapters, currPos + 2);
                x += DoCount(newJolts, adapters, currPos + 3);
            }
            if (adapters[currPos] == currJolts + 2) {
                //Console.WriteLine("CurJolts: {0} | Pos: {1} | CurrValue: {2}", currJolts, currPos, adapters[currPos]);
                //Console.Write(" {0} - ", currJolts);
                foundIt = true;
                newJolts = adapters[currPos];
                x += DoCount(newJolts, adapters, currPos + 1);
                x += DoCount(newJolts, adapters, currPos + 2);
                x += DoCount(newJolts, adapters, currPos + 3);
            }
            if (adapters[currPos] == currJolts + 3) {
                //Console.WriteLine("CurJolts: {0} | Pos: {1} | CurrValue: {2}", currJolts, currPos, adapters[currPos]);
                //Console.Write(" {0} - ", currJolts);
                foundIt = true;
                newJolts = adapters[currPos];
                x += DoCount(newJolts, adapters, currPos + 1);
                x += DoCount(newJolts, adapters, currPos + 2);
                x += DoCount(newJolts, adapters, currPos + 3);
            }
            if (currPos == adapters.Count() - 1 && foundIt) {
                //Console.WriteLine("*LAST* CurJolts: {0} | Pos: {1} | CurrValue: {2}", currJolts, currPos, adapters[currPos]);
                //Console.Write(" {0} \n", newJolts);
                x++;
            }
            return x;
        }

        public static void Second() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "test1.txt"));
            HashSet<int> l = new HashSet<int>(lines);

            List<int> list = new List<int>(lines);
            list.Add(0);
            list.Sort();
            list.Add(list.Last() + 3);

            long total = 0;
            //total = DoCount(0, list, 0);
            total = CountIt(list);

            //for (int i = 0; i < list.Count(); i++) {
            //    total += DoCount(list[i], list, i);
            //}
            Console.WriteLine("Total sequences: {0}", total);

        }
    }
}