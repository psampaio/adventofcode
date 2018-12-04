using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day03 : IPuzzle
    {
        private const int FabricSize = 1000;

        public int Day => 3;

        public object RunPart1(string[] lines)
        {
            //lines = new []
            //{
            //    "#1 @ 1,3: 4x4",
            //    "#2 @ 3,1: 4x4",
            //    "#3 @ 5,5: 2x2"
            //};

            var fabric = CalculateFabric(lines);

            int sum = 0;
            for (int i = 0; i < FabricSize; i++)
            {
                for (int j = 0; j < FabricSize; j++)
                {
                    if (fabric[i, j]?.Claims.Count > 1)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        public object RunPart2(string[] lines)
        {
            var fabric = CalculateFabric(lines);

            var candidateClaims = new List<Claim>();
            for (int i = 0; i < FabricSize; i++)
            {
                for (int j = 0; j < FabricSize; j++)
                {
                    if (fabric[i, j]?.Claims.Count == 1)
                    {
                        candidateClaims.Add(fabric[i, j].Claims.First());
                    }
                }
            }

            return candidateClaims
                .GroupBy(c => c.Id)
                .Select(group => new
                {
                    Claim = group.First(),
                    Count = group.Count()
                })
                .Where(x => x.Count == x.Claim.Area)
                .Select(x => x.Claim.Id)
                .First();
        }

        private static FabricUnit[,] CalculateFabric(IEnumerable<string> lines)
        {
            var fabric = new FabricUnit[FabricSize, FabricSize];
            foreach (var claim in lines.Select(Claim.Parse))
            {
                for (int i = claim.Left; i < claim.Left + claim.Width; i++)
                {
                    for (int j = claim.Top; j < claim.Top + claim.Height; j++)
                    {
                        if (fabric[i, j] == null)
                        {
                            fabric[i, j] = new FabricUnit();
                        }

                        fabric[i, j].Claims.Add(claim);
                    }
                }
            }

            return fabric;
        }

        private class FabricUnit
        {
            public FabricUnit()
            {
                Claims = new List<Claim>();
            }

            public List<Claim> Claims { get; }
        }

        private class Claim
        {
            private static readonly Regex RegexPattern = new Regex(@"#(\d*) @ (\d*),(\d*): (\d*)x(\d*)");

            private Claim()
            {
            }

            public int Id { get; private set; }
            public int Left { get; private set; }
            public int Top { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }

            public int Area => Width * Height;

            public static Claim Parse(string line)
            {
                var match = RegexPattern.Match(line);

                return new Claim
                {
                    Id = int.Parse(match.Groups[1].Value),
                    Left = int.Parse(match.Groups[2].Value),
                    Top = int.Parse(match.Groups[3].Value),
                    Width = int.Parse(match.Groups[4].Value),
                    Height = int.Parse(match.Groups[5].Value)
                };
            }
        }
    }
}