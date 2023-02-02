using System.Diagnostics;

namespace AdventOfCode2022
{
    class Day20Node
    {
        public int Value { get; set; }
        public Day20Node? Previous { get; set; }
        public Day20Node? Next { get; set; }
    }

    class Number
    {
        public long Value { get; set; }
    }

    class Day20
    {
        List<Day20Node> nodes = new List<Day20Node>();
        Day20Node headNode;

        #region Part1 - a much fast version after searched online.
        internal async Task<string> Process()
        {
            List<string> input = await Utils.ReadFile("day20_1.txt");

            var numbersUnchanged = input.Select(x => new Number { Value = int.Parse(x) }).ToList();

            var numbers = numbersUnchanged.Select(x => x).ToList();

            MixNumbers(numbersUnchanged, numbers);

            int zeroIndex = numbers.IndexOf(numbers.First(x => x.Value == 0));

            return (numbers[(1000 + zeroIndex) % numbers.Count].Value +
                numbers[(2000 + zeroIndex) % numbers.Count].Value +
                numbers[(3000 + zeroIndex) % numbers.Count].Value).ToString();
        }

        private void MixNumbers(List<Number> numbersUnchanged, List<Number> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                var number = numbersUnchanged[i];

                int oldIndex = numbers.IndexOf(number);
                int newIndex = (int)((oldIndex + number.Value) % (numbers.Count - 1));

                if (newIndex <= 0 && oldIndex + number.Value != 0)
                {
                    newIndex = numbers.Count - 1 + newIndex;
                }

                numbers.RemoveAt(oldIndex);
                numbers.Insert(newIndex, number);
            }
        }
        #endregion Part1 - a much fast version after searched online.

        #region Part 1 - My own version to demostrate linked nodes, super slow when data is huge.
        internal async Task<string> Process1()
        {
            List<string> data = await Utils.ReadFile("day20_1.txt");

            Day20Node lastNode = headNode;
            foreach (string line in data)
            {
                lastNode = ProcessData(line, lastNode);
            }

            lastNode.Next = headNode;
            headNode.Previous = lastNode;

            int result = 0;
            for (int i = 1; i <= 3000; i++)
            //for (int i = 1; i <= 1; i++)
            {
                foreach (Day20Node node in nodes)
                {
                    if (node.Value == 0) continue;

                    MoveNode(node);
                    //Print();
                }

                if (i % 1000 == 0)
                {
                    result += nodes.Find(n => n.Value == 0).Next.Value;
                    Print();
                }
            }

            return result.ToString();
        }
        private void Print()
        {
            Day20Node node = headNode;
            while (true)
            {
                Debug.Write($"{node.Value},");

                node = node.Next;
                if (node == headNode) break;
            }

            Debug.WriteLine("");
        }

        private Day20Node ProcessData(string line, Day20Node lastNode)
        {
            Day20Node node = new Day20Node() { Value = int.Parse(line) };
            nodes.Add(node);

            if (lastNode == null) // head node
            {
                headNode = node;
            }
            else
            {
                lastNode.Next = node;
                node.Previous = lastNode;
            }

            return node;
        }

        private void MoveNode(Day20Node node)
        {
            if (node.Value > 0)
                MoveNext(node);
            else
                MovePrevious(node);
        }

        private void MoveNext(Day20Node node)
        {
            if (node.Value == headNode.Value) headNode = node.Next;

            Day20Node targetNode = node;
            for (int i = 0; i < node.Value; i++)
            {
                targetNode = targetNode.Next;
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            targetNode.Next.Previous = node;
            node.Next = targetNode.Next;
            node.Previous = targetNode;
            targetNode.Next = node;
        }

        private void MovePrevious(Day20Node node)
        {
            if (node.Value == headNode.Value) headNode = node.Next;

            Day20Node targetNode = node;
            for (int i = 0; i < Math.Abs(node.Value); i++)
            {
                targetNode = targetNode.Previous;
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            targetNode.Previous.Next = node;
            node.Next = targetNode;
            node.Previous = targetNode.Previous;
            targetNode.Previous = node;
        }

        #endregion Part 1 - My own version to demostrate linked nodes, super slow when data is huge.
    }
}