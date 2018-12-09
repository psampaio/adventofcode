using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day07 : IPuzzle
    {
        public int Day => 7;

        public object RunPart1(string[] input)
        {
//            input = new[]
//            {
//                "Step C must be finished before step A can begin.",
//                "Step C must be finished before step F can begin.",
//                "Step A must be finished before step B can begin.",
//                "Step A must be finished before step D can begin.",
//                "Step B must be finished before step E can begin.",
//                "Step D must be finished before step E can begin.",
//                "Step F must be finished before step E can begin."
//            };

            var stepMap = new Dictionary<int, Step>();

            foreach (string instruction in input)
            {
                if (!stepMap.TryGetValue(instruction[5], out var from))
                {
                    from = new Step(new string(new[] {instruction[5]}));
                    stepMap.Add(instruction[5], from);
                }

                if (!stepMap.TryGetValue(instruction[36], out var to))
                {
                    to = new Step(new string(new[] {instruction[36]}));
                    stepMap.Add(instruction[36], to);
                }

                from.To.Add(to);
                to.From.Add(from);
            }

            var steps = stepMap.Values.ToList();
            var startingSteps = steps.Where(s => !s.From.Any()).OrderBy(s => s.Letter).ToList();

            var orderedSteps = new List<Step>();
            var currentStep = startingSteps.First();
            var nextSteps = startingSteps.Skip(1).ToList();
            var result = new StringBuilder();
            while (steps.Any())
            {
                orderedSteps.Add(currentStep);
                steps.Remove(currentStep);
                result.Append(currentStep.Letter);
                foreach (var step in currentStep.To)
                {
                    if (!nextSteps.Contains(step))
                    {
                        nextSteps.Add(step);
                    }
                }

                if (!nextSteps.Any())
                {
                    continue;
                }
                
                nextSteps = nextSteps.OrderBy(s => s.Letter).ToList();
                currentStep = nextSteps.First(s => s.From.All(orderedSteps.Contains));
                nextSteps.Remove(currentStep);
            }

            return result.ToString();
        }

        public object RunPart2(string[] input)
        {
            return null;
        }

        private class Step
        {
            public Step(string letter)
            {
                Letter = letter;
                From = new List<Step>();
                To = new List<Step>();
            }

            public string Letter { get; }

            public IList<Step> To { get; }

            public IList<Step> From { get; }
        }
    }
}