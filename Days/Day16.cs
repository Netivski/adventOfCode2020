using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day16 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static Dictionary<string, IntervalPair> Validators = new Dictionary<string, IntervalPair>();

        static bool IsValidValue(int value, IntervalPair intPair) {
            return ((value >= intPair.Min1 && value <= intPair.Max1) || (value >= intPair.Min2 && value <= intPair.Max2));
        }

        public class Ticket {
            int[] fieldValues;

            public Ticket(string csvFields) {
                var strFields = csvFields.Split(',');
                fieldValues = new int[strFields.Length];
                for (int i = 0; i < strFields.Length; i++) {
                    fieldValues[i] = int.Parse(strFields[i]);
                }
            }

            public int GetField(int index) { return fieldValues[index]; }

            public int InvalidFieldsSum() {
                int total = 0;
                foreach (int field in fieldValues) {
                    if (Validators.Count(v => IsValidValue(field, v.Value)) == 0) {
                        total += field;
                    }
                }
                return total;
            }

            public void Print() {
                for (int i = 0; i < fieldValues.Length; i++) {
                    Console.Write("{0}{1}", fieldValues[i], i == fieldValues.Length - 1 ? "\n" : ",");
                }
            }

            public bool IsValid() {
                return InvalidFieldsSum() == 0;
            }
        }


        public class IntervalPair {
            public int[] left;
            public int[] right;

            public int Min1 { get { return left[0]; } }
            public int Max1 { get { return left[1]; } }
            public int Min2 { get { return right[0]; } }
            public int Max2 { get { return right[1]; } }

            public IntervalPair(int[] left, int[] right) {
                this.left = left;
                this.right = right;
            }
        }

        public static void AddValidator(string line) {
            string name = line.Substring(0, line.IndexOf(':'));
            string[] valPairs = line.Substring(line.IndexOf(':') + 1).Split(" or ");
            int[] left = valPairs[0].Split('-').Select(e => int.Parse(e.Trim())).ToArray();
            int[] right = valPairs[1].Split('-').Select(e => int.Parse(e.Trim())).ToArray();
            Validators.Add(name, new IntervalPair(left, right));
        }

        public static void ReadInput(string[] lines, out Ticket myTicket, out List<Ticket> nearbyTickets) {
            bool doSelf = false, doNearby = false;
            nearbyTickets = new List<Ticket>();
            myTicket = null;

            foreach (string line in lines) {
                if (line == string.Empty) { continue; }
                if (line.StartsWith("your ticket")) { doSelf = true; doNearby = false; continue; }
                if (line.StartsWith("nearby tickets")) { doSelf = false; doNearby = true; continue; }

                if (!doSelf && !doNearby) {
                    AddValidator(line);
                } else if (doSelf) {
                    myTicket = new Ticket(line);
                } else if (doNearby) {
                    nearbyTickets.Add(new Ticket(line));
                }
            }
        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day16.txt")).ToArray();
            Ticket myTicket;
            List<Ticket> nearbyTickets;

            ReadInput(lines, out myTicket, out nearbyTickets);

            int invalidFields = 0;
            foreach (Ticket t in nearbyTickets) {
                invalidFields += t.InvalidFieldsSum();
            }
            Console.WriteLine("Invalid fields are {0}", invalidFields);
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day16.txt")).ToArray();
            Ticket myTicket;
            List<Ticket> nearbyTickets;

            ReadInput(lines, out myTicket, out nearbyTickets);

            Dictionary<int, List<string>> fieldLocation = new Dictionary<int, List<string>>();
            int totalValidators = Validators.Keys.Count;

            var validTickets = nearbyTickets.Where(t => t.IsValid());
            //foreach (Ticket t in validTickets) { t.Print(); }

            foreach (var kvp in Validators) {
                for (int i = 0; i < totalValidators; i++) {
                    if (validTickets.Count(t => IsValidValue(t.GetField(i), kvp.Value)) == validTickets.Count()) {
                        if (!fieldLocation.ContainsKey(i)) { fieldLocation.Add(i, new List<string>()); }
                        fieldLocation[i].Add(kvp.Key);
                    }
                }
            }

            var x = fieldLocation.Where(k => k.Value.Count(l => l.StartsWith("departure")) > 0);
            foreach (var kvp in x) { kvp.Value.RemoveAll(m => !m.StartsWith("departure")); }
            long total = 1;
            int count = 1;
            List<string> keysToRemove = new List<string>();
            while (count <= x.Count()) {
                var y = x.Where(k => k.Value.Count() == count && k.Value.Count(v => keysToRemove.Contains(v)) == 0);
                if (y.Count() == 0) { count++; continue; }
                foreach (var kvp in y) {
                    total += myTicket.GetField(kvp.Key);
                    keysToRemove.AddRange(kvp.Value);
                }
                foreach(var kvp in x) {

                }
            }


            foreach (var kvp in x) {
                total = total * myTicket.GetField(kvp.Key);
            }
            Console.WriteLine("{0}", total);
        }
    }
}