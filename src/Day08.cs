using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day08 : IPuzzle
    {
        public int Day => 8;
        
        public object RunPart1(string[] input)
        {
            var numbers = input[0].Split(" ").Select(int.Parse).ToList();
//            var numbers = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2".Split(" ").Select(int.Parse).ToList();
            (var root, int length) = Node.Build(numbers, 0);
            return root.CalculateMetadata();
        }

        private class Node
        {
            private readonly List<int> metadata;
            private readonly List<Node> children;

            private Node()
            {
                metadata = new List<int>();
                children = new List<Node>();
            }

            public int CalculateMetadata()
            {
                int sum = metadata.Sum();
                foreach (var child in children)
                {
                    sum += child.CalculateMetadata();
                }

                return sum;
            }
            
            public static (Node Node, int Length) Build(IList<int> numbers, int startIndex)
            {
                Node node;

                int childrenCount = numbers[startIndex + 0];
                int metadataCount = numbers[startIndex + 1];
                if (childrenCount == 0)
                {
                    node = new Node();
                    node.metadata.AddRange(numbers.Skip(startIndex + 2).Take(metadataCount));
                    return (node, 2 + metadataCount);
                }
                
                node = new Node();
                int childrenLength = 0;
                for (int i = 0; i < childrenCount; i++)
                {
                    (var child, int length) = Build(numbers, startIndex + 2 + childrenLength);
                    childrenLength += length;
                    node.children.Add(child);
                }
                node.metadata.AddRange(numbers.Skip(startIndex + 2 + childrenLength).Take(metadataCount));

                return (node, 2 + childrenLength + metadataCount);
            }
        }

        public object RunPart2(string[] input)
        {
            return null;
        }
    }
}