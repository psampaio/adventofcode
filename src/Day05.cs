using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day05 : IPuzzle
    {
        public int Day => 5;

        public object RunPart1(string[] input)
        {
            var list = new LinkedList<char>(input[0]);
            ReactUnits(list);
            return list.Count;
        }

        public object RunPart2(string[] input)
        {
            int listSize = int.MaxValue;
            for (int i = 97; i <= 122; i++)
            {
                var list = new LinkedList<char>(input[0]);
                var node = list.First;
                while (node != null)
                {
                    var nextNode = node.Next;

                    if (node.Value == i || node.Value == i - 32)
                    {
                        list.Remove(node);
                    }

                    node = nextNode;
                }

                ReactUnits(list);
                if (list.Count < listSize)
                {
                    listSize = list.Count;
                }
            }

            return listSize;
        }

        private static void ReactUnits(LinkedList<char> list)
        {
            var node = list.First;
            while (node != null)
            {
                var nextNode = node.Next;
                if (nextNode == null)
                {
                    break;
                }

                if (Math.Abs(node.Value - nextNode.Value) == 32)
                {
                    var previousNode = node.Previous;
                    list.Remove(node);
                    list.Remove(nextNode);
                    node = previousNode ?? list.First;
                }
                else
                {
                    node = nextNode;
                }
            }
        }
    }
}