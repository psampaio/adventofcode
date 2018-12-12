using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day10 : IPuzzle
    {
        public int Day => 10;

        public object RunPart1(string[] input)
        {
            return RunBothParts(input).Output;
        }

        public object RunPart2(string[] input)
        {
            return RunBothParts(input).Iteration;
        }

        private static (string Output, int Iteration) RunBothParts(IEnumerable<string> input)
        {
            var stars = input.Select(Star.Parse).ToList();

            int lengthX = stars.Max(s => s.Position.X) - stars.Min(s => s.Position.X);
            int lengthY = stars.Max(s => s.Position.Y) - stars.Min(s => s.Position.Y);

            int iteration = 0;
            bool contracting;
            do
            {
                foreach (var star in stars)
                {
                    star.Position.X += star.Velocity.X;
                    star.Position.Y += star.Velocity.Y;
                }

                int newLengthX = stars.Max(s => s.Position.X) - stars.Min(s => s.Position.X);
                int newLengthY = stars.Max(s => s.Position.Y) - stars.Min(s => s.Position.Y);

                if (lengthX + lengthY >= newLengthX + newLengthY)
                {
                    lengthX = newLengthX;
                    lengthY = newLengthY;
                    contracting = true;
                }
                else
                {
                    contracting = false;
                }

                iteration++;
            } while (contracting);

            int minX = int.MaxValue;
            int minY = int.MaxValue;
            foreach (var star in stars)
            {
                star.Position.X -= star.Velocity.X;
                star.Position.Y -= star.Velocity.Y;
                if (star.Position.X < minX)
                {
                    minX = star.Position.X;
                }
                if (star.Position.Y < minY)
                {
                    minY = star.Position.Y;
                }
            }
            iteration--;

            var grid = new bool[++lengthX, ++lengthY];
            foreach (var star in stars)
            {
                grid[star.Position.X - minX, star.Position.Y - minY] = true;
            }

            var output = new StringBuilder();
            output.AppendLine($"After {iteration} seconds:");
            for (int j = 0; j < lengthY; j++)
            {
                for (int i = 0; i < lengthX; i++)
                {
                    output.Append(grid[i, j] ? "#" : ".");
                }

                output.AppendLine();
            }

            return (output.ToString(), iteration);
        }

        private class Star
        {
            private static readonly Regex LinePattern = new Regex(@"position=<(.*), (.*)> velocity=<(.*), (.*)>");

            private Star(Point velocity)
            {
                Velocity = velocity;
            }

            public Point Position { get; set; }

            public Point Velocity { get; }

            public static Star Parse(string line)
            {
                var match = LinePattern.Match(line);
                int posX = int.Parse(match.Groups[1].Value);
                int posY = int.Parse(match.Groups[2].Value);

                int velX = int.Parse(match.Groups[3].Value);
                int velY = int.Parse(match.Groups[4].Value);

                return new Star(new Point(velX, velY))
                {
                    Position = new Point(posX, posY)
                };
            }
        }

        private class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }

            public int Y { get; set; }
        }
    }
}