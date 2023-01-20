using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace AdventOfCode2022
{
    enum OperationTypes
    {
        None,
        Multiply,
        Plus,
        Squared
    }

    class Operation
    {
        public OperationTypes OperationType { get; set; }
        public int Value { get; set; }
    }

    class Monkey
    {
        public string Name { get; set; } = ""; 
        public List<long> Items { get; set; } = new List<long>();
        public Operation ItemOperation { get; set; } = new Operation
        {
            OperationType = OperationTypes.None,
            Value = 0
        };
        public int TestValue { get; set; } = 0;
        public string TrueMonkeyName { get; set; } = "";
        public string FalseMonkeyName { get; set; } = ""; 

        public int Inspected {get; set; } = 0;
    }

    // Took long time and could not find the right answer. Searched online and found this:
    // You can devise a least common multiple by multiplying the "divisible by" numbers together
    // and then modulo each new worry level by that number.
    internal class Day11
    {
        List<Monkey> monkeys = new List<Monkey>();       

        internal async Task<string> Process(int round, bool useFixedRelieveLevel)
        {
            List<string> data = await Utils.ReadFile("day11_1.txt");
            Monkey monkey = new Monkey();
            foreach (string s in data)
            {
                ProcessData(s, ref monkey);

                if (s.IndexOf("false") >= 0)
                {
                    monkeys.Add(monkey);
                    monkey = new Monkey();
                }
            }

            int relieveLevel=1;
            if (useFixedRelieveLevel)
            {
                relieveLevel = 3;
            }
            else
            {
                foreach (Monkey m in monkeys)
                {
                    relieveLevel *= m.TestValue;
                }
            }

            for (int i=0; i<round; i++)
            {
                Inspect(relieveLevel, useFixedRelieveLevel);
            }

            List<Monkey> sorted = monkeys.OrderByDescending(m => m.Inspected).ToList();

            return ((ulong)sorted[0].Inspected * (ulong)sorted[1].Inspected).ToString();
        }

        private void ProcessData(string s, ref Monkey monkey)
        {
            if (s.IndexOf("Monkey") == 0)
            {
                monkey.Name = s.Remove(s.Length-1);
            }
            else if (s.IndexOf("Starting items:") >= 0)
            {
                string[] items = s.Substring(18).Split(',');
                foreach (string item in items)
                {
                    monkey.Items.Add(long.Parse(item));
                }
            }
            else if (s.IndexOf("* old") >= 0)
            {
                monkey.ItemOperation = new Operation
                {
                    OperationType = OperationTypes.Squared,
                    Value = 0
                };
            }
            else if (s.IndexOf("*") >= 0)
            {
                monkey.ItemOperation = new Operation
                {
                    OperationType = OperationTypes.Multiply,
                    Value = int.Parse(s.Substring(s.IndexOf("*") + 1))
                };
            }
            else if (s.IndexOf("+") >= 0)
            {
                monkey.ItemOperation = new Operation
                {
                    OperationType = OperationTypes.Plus,
                    Value = int.Parse(s.Substring(s.IndexOf("+") + 1))
                };
            }
            else if (s.IndexOf("divisible") >= 0)
            {
                monkey.TestValue = int.Parse(s.Substring(21));
            }
            else if (s.IndexOf("true") >= 0)
            {
                monkey.TrueMonkeyName = s.Substring(s.IndexOf("to")+3);
            }
            else if (s.IndexOf("false") >= 0)
            {
                monkey.FalseMonkeyName = s.Substring(s.IndexOf("to") + 3);
            }
        }

        private void Inspect(int relieveLevel, bool useFixedRelieveLevel)
        {
            foreach (Monkey monkey in monkeys)
            {
                foreach (long item in monkey.Items)
                {
                    monkey.Inspected++;

                    long newValue = 0;
                    switch (monkey.ItemOperation.OperationType)
                    {
                        case OperationTypes.Multiply:
                            newValue = item * monkey.ItemOperation.Value;
                            break;
                        case OperationTypes.Plus: 
                            newValue = item + monkey.ItemOperation.Value;
                            break;
                        case OperationTypes.Squared: 
                            newValue = item * item;
                            break;
                    }

                    if (useFixedRelieveLevel)
                    {
                        newValue = newValue / relieveLevel;
                    }
                    else
                    {
                        newValue %= relieveLevel;
                    }

                    if (newValue % monkey.TestValue == 0)
                    {
                        var target = monkeys.Where(m => m.Name.Equals(monkey.TrueMonkeyName, StringComparison.OrdinalIgnoreCase)).First();
                        target.Items.Add(newValue);
                    }
                    else
                    {
                        var target = monkeys.Where(m => m.Name.Equals(monkey.FalseMonkeyName, StringComparison.OrdinalIgnoreCase)).First();
                        target.Items.Add(newValue);
                    }
                }
                
                monkey.Items.Clear();
            }
        }
    }
}
