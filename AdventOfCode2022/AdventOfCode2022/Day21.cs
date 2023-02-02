namespace AdventOfCode2022
{
    class Day21Monkey
    {
        public string Name { get; set; }
        public long[] Values { get; set; } = new long[2];
        public char Operator { get; set; }
        public long Result { get; set; } = -1;
        public List<string> MonkeyNames { get; set; } = new List<string>();
        public bool Replaced { get; set; } = false;
    }

    class Day21
    {
        List<Day21Monkey> monkeys = new List<Day21Monkey>();

        #region Part1
        internal async Task<string> Process()
        {
            List<string> data = await Utils.ReadFile("day21_1.txt");

            foreach (string line in data)
            {
                ProcessData(line);
            }

            bool done = false;
            while (!done)
            {
                foreach (Day21Monkey monkey in monkeys)
                {
                    if (monkey.Replaced) continue;

                    if (monkey.Result > 0)
                    {
                        if (ReplaceMonkeyResult(monkey))
                        {
                            done = true; break;
                        }
                        monkey.Replaced = true;
                    }
                }
            }

            return monkeys.Where(m => m.Name == "root").First().Result.ToString();
        }

        private void ProcessData(string line)
        {
            Day21Monkey monkey = new Day21Monkey();

            int posColon = line.IndexOf(':');
            string secondPart = line.Substring(posColon + 1).Trim();

            monkey.Name = line.Substring(0, posColon);
            if ((secondPart.IndexOf(' ') > 0)) // || (line.IndexOf('-') > 0) || (line.IndexOf('*') > 0) || (line.IndexOf('/') > 0))
                ProcessOperator(monkey, secondPart);
            else
                monkey.Result = int.Parse(secondPart);

            monkeys.Add(monkey);
        }

        private void ProcessOperator(Day21Monkey monkey, string line)
        {
            int firstSpace = line.IndexOf(' ');

            monkey.MonkeyNames.Add(line.Substring(0, firstSpace));
            monkey.Operator = line[firstSpace + 1];
            monkey.MonkeyNames.Add(line.Substring(line.IndexOf(' ', firstSpace + 1) + 1));
        }

        private bool ReplaceMonkeyResult(Day21Monkey monkey)
        {
            var monkeysFound = monkeys.Where(m => m.MonkeyNames.Any(n => n == monkey.Name));
            foreach (Day21Monkey m in monkeysFound)
            {
                int i = m.MonkeyNames.IndexOf(monkey.Name);
                m.Values[i] = monkey.Result;
                m.MonkeyNames[i] = "";
                if (string.IsNullOrEmpty(m.MonkeyNames[0]) && string.IsNullOrEmpty(m.MonkeyNames[1]))
                {
                    m.Result = Calculate(m);
                    if (m.Name == "root") return true;
                }
            }

            return false;
        }

        private long Calculate(Day21Monkey m)
        {
            long result = 0;
            switch (m.Operator)
            {
                case '+':
                    result = m.Values[0] + m.Values[1];
                    break;
                case '-':
                    result = m.Values[0] - m.Values[1];
                    break;
                case '*':
                    result = m.Values[0] * m.Values[1];
                    break;
                case '/':
                    result = m.Values[0] / m.Values[1];
                    break;
            };

            return result;
        }



        #endregion Part1
    }
}