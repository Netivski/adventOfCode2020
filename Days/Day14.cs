using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day14 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static ulong MaskInput(string currMask, ulong value) {
            ulong newValue = value & Convert.ToUInt64(currMask.Replace('X', '1'), 2);
            newValue = newValue | Convert.ToUInt64(currMask.Replace('X', '0'), 2);
            return newValue;
        }
        static void SetMemory(Dictionary<int, ulong> mem, string memLine, string currMask) {
            memLine = memLine.Replace("mem[", "").Replace("] = ", "=");
            int idx = int.Parse(memLine.Substring(0, memLine.IndexOf('=')));
            ulong newValue = ulong.Parse(memLine.Substring(memLine.IndexOf('=') + 1));
            if (!mem.ContainsKey(idx)) { mem.Add(idx, 0); }
            mem[idx] = MaskInput(currMask, newValue);
        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day14.txt")).ToArray();
            string currMask = "";
            ulong bit36 = 68719476735;
            Dictionary<int, ulong> memory = new Dictionary<int, ulong>();
            for (int i = 0; i < lines.Count(); i++) {
                if (lines[i].StartsWith("mask")) {
                    currMask = lines[i].Substring(7);
                } else {
                    SetMemory(memory, lines[i], currMask);
                }
            }
            ulong total = 0;
            foreach (int idx in memory.Keys) {
                total = total + (memory[idx] & bit36);
            }
            Console.WriteLine("Total is {0}", total);
        }

        static void WriteMemoryMasked(string currMask, Dictionary<ulong, ulong> mem, string memLine) {
            memLine = memLine.Replace("mem[", "").Replace("] = ", "=");
            
            ulong idx = ulong.Parse(memLine.Substring(0, memLine.IndexOf('=')));
            ulong value = ulong.Parse(memLine.Substring(memLine.IndexOf('=') + 1));
            int numXs = currMask.Count(c => c == 'X');
            ulong numIndexes = (ulong)Math.Pow(2, numXs);
            //Console.WriteLine("Mask: {0} | Xs: {1} | Indexes: {2}", currMask, numXs, numIndexes );
            ulong idxWithOnes = idx | Convert.ToUInt64(currMask.Replace('X', '0'), 2);

            ulong xAsZero = idxWithOnes & Convert.ToUInt64(currMask.Replace('0', '1').Replace('X', '0'), 2); // To Set to Zero
            ulong xAsOne = idxWithOnes | Convert.ToUInt64(currMask.Replace('1', '0').Replace('X', '1'), 2); // To Set To One

            for (ulong i = 0; i < numIndexes / 2; i++) {

                if (!mem.ContainsKey(xAsZero + i)) { mem.Add(xAsZero + i, 0); }
                mem[xAsZero + i] = value;

                if (!mem.ContainsKey(xAsOne - i)) { mem.Add(xAsOne - i, 0); }
                mem[xAsOne - i] = value;
            }
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day14.txt")).ToArray();
            string currMask = "";
            Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();
            for (int i = 0; i < lines.Count(); i++) {
                if (lines[i].StartsWith("mask")) {
                    currMask = lines[i].Substring(7);
                } else {
                    WriteMemoryMasked(currMask, memory, lines[i]);
                }
            }
            ulong total = 0;
            foreach (ulong idx in memory.Keys) {
                total += memory[idx];
            }
            Console.WriteLine("Total is {0}", total);
        }
    }
}