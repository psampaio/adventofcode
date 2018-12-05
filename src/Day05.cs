using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day05 : IPuzzle
    {
        public int Day => 5;

        public object RunPart1(string[] input)
        {
            var line = input[0];
            //var line = "dabAcCaCBAcCcaDA";
            var list = new LinkedList<char>(line);

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

            return list.Count;
        }

        public object RunPart2(string[] input)
        {
            return null;
        }
    }
}