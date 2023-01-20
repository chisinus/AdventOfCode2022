using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace AdventOfCode2022
{
    enum NodeType
    {
        Directory,
        File
    }

    class Node
    {
        public NodeType Type { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public Node? Parent { get; set; }
        public List<Node> Children { get; set; }
    }

    internal class Day7
    {
        // root
        Node root = new Node()
        {
            Type = NodeType.Directory,
            Name = "/",
            Size = 0,
            Parent = null,
            Children = new List<Node>()
        };

        long total = 0;
        long minSize = 0;

        internal async Task<string> Part1()
        {
            List<string> data = await Utils.ReadFile("day7_1.txt");

            Node currentNode = root;
            foreach (string s in data)
            {
                currentNode = ProcessCommand(s, currentNode);
            }

            CalculateDirectorySize(root);

            return total.ToString();
        }

        private Node ProcessCommand(string s, Node currentNode )
        {
            var list = s.Split(' ');

            Node result = currentNode;
            if (list[0] == "$") // command
            {
                if (list[1] == "cd")    // change directory
                {
                    if (list[2] == "/")
                    {
                        result = root;
                    }
                    else if (list[2] == "..")
                    {
                        result = currentNode.Parent;
                    }
                    else
                    {
                        result = currentNode.Children.First(n => n.Name == list[2] && n.Type == NodeType.Directory);
                    }
                }
                else if (list[1] == "ls")  // list content
                {
                    // nothing to do
                }
            }
            else if (list[0] == "dir")  // directory
            {
                currentNode.Children.Add(new Node()
                {
                    Type = NodeType.Directory,
                    Name = list[1],
                    Size = 0,
                    Parent = currentNode,
                    Children = new List<Node>()
                });
            }
            else  // file
            {
                currentNode.Children.Add(new Node()
                {
                    Type = NodeType.File,
                    Name = list[1],
                    Size = long.Parse(list[0]),
                    Parent = currentNode,
                    Children = new List<Node>()
                });
            }

            return result;
        }

        private long CalculateDirectorySize(Node currentNode)
        {
            long size = 0;

            foreach (Node node in currentNode.Children)
            {
                if (node.Type == NodeType.Directory)
                    size += CalculateDirectorySize(node);
                else
                    size += node.Size;
            }

            if (size <= 100000)
                total += size;

            return size;
        }

        internal async Task<string> Part2()
        {
            List<string> data = await Utils.ReadFile("day7_1.txt");

            Node currentNode = root;
            foreach (string s in data)
            {
                currentNode = ProcessCommand(s, currentNode);
            }

            long totalSize = CalculateDirectorySize(root);
            long spaceNeeded = 30000000 - (70000000 - totalSize);

            FindDirectoryToDelete(root, spaceNeeded);

            // 5756764
            return minSize.ToString();
        }

        private long FindDirectoryToDelete(Node currentNode, long spaceNeeded)
        {
            long size = 0;

            foreach (Node node in currentNode.Children)
            {
                if (node.Type == NodeType.Directory)
                    // Improvement: stop checking parent directories if it has more than spaceNeeded.
                    size += FindDirectoryToDelete(node, spaceNeeded);
                else
                    size += node.Size;
            }

            if ((size >= spaceNeeded) && (size < minSize || minSize == 0))
                minSize = size;

            return size;
        }
    }
}
