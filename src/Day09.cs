using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day09 : IPuzzle
    {
        public int Day => 9;

        public object RunPart1(string[] input)
        {
            var tokens = input[0].Split(' ');
            int playersCount = int.Parse(tokens[0]);
            int lastMarble = int.Parse(tokens[6]);

            return CalculateScore(playersCount, lastMarble);
        }

        public object RunPart2(string[] input)
        {
            var tokens = input[0].Split(' ');
            int playersCount = int.Parse(tokens[0]);
            int lastMarble = int.Parse(tokens[6]);

            return CalculateScore(playersCount, lastMarble * 100);
        }

        private static object CalculateScore(int playersCount, int lastMarble)
        {
            var players = new long[playersCount];

            var marbles = new LinkedList<long>();
            marbles.AddFirst(0);
            var currentMarble = marbles.First;

            for (int i = 1; i < lastMarble + 1; i++)
            {
                int currentPlayer = i % playersCount;
                if (currentPlayer == 0)
                {
                    currentPlayer = playersCount;
                }

                if (i % 23 == 0)
                {
                    players[currentPlayer - 1] += i;

                    for (int j = 0; j < 7; j++)
                    {
                        currentMarble = currentMarble.Previous ?? marbles.Last;
                    }

                    players[currentPlayer - 1] += currentMarble.Value;
                    var nextMarble = currentMarble.Next ?? marbles.First;
                    marbles.Remove(currentMarble);
                    currentMarble = nextMarble;
                }
                else
                {
                    currentMarble = marbles.AddAfter(currentMarble.Next ?? marbles.First, i);
                }
            }

            return players.Max();
        }
    }
}