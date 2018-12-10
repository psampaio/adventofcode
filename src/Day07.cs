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
            return RunBoth(input, 1).Result;
        }

        public object RunPart2(string[] input)
        {
            return RunBoth(input, 5).EllapsedTime;
        }

        public (string Result, int EllapsedTime) RunBoth(string[] input, int workerCount, int timeOffset = 0)
        {
            var steps = ParseSteps(input);
            int stepCount = steps.Count;
            var nextSteps = steps.Where(s => !s.From.Any()).OrderBy(s => s.Letter).ToList();

            var workers = new List<Worker>();
            for (int i = 0; i < workerCount; i++)
            {
                workers.Add(new Worker());
            }

            var orderedSteps = new List<Step>();
            var result = new StringBuilder();

            int ellapsedTime = 0;
            do
            {
                foreach (var worker in workers.Where(w => w.Step != null))
                {
                    if (worker.RemainingType > 0)
                    {
                        worker.RemainingType--;
                    }

                    if (worker.RemainingType == 0)
                    {
                        var currentStep = worker.Step;
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
                    }
                }

                foreach (var worker in workers.Where(w => w.RemainingType == 0))
                {
                    if (nextSteps.Any())
                    {
                        nextSteps = nextSteps.OrderBy(s => s.Letter).ToList();
                        var currentStep = nextSteps.FirstOrDefault(s => s.From.All(orderedSteps.Contains));
                        worker.Step = currentStep;
                        if (currentStep == null)
                        {
                            continue;
                        }

                        worker.RemainingType = currentStep.Letter[0] - 4;
                        nextSteps.Remove(currentStep);
                    }
                    else
                    {
                        worker.Step = null;
                    }
                }

                ellapsedTime++;
            } while (result.Length != stepCount);

            return (result.ToString(), ellapsedTime - 1);
        }

        private static List<Step> ParseSteps(IEnumerable<string> input)
        {
            var steps = new List<Step>();
            foreach (string instruction in input)
            {
                string fromLetter = new string(new[] {instruction[5]});
                var from = steps.SingleOrDefault(s => s.Letter == fromLetter);
                if (from == null)
                {
                    from = new Step(fromLetter);
                    steps.Add(from);
                }

                string toLetter = new string(new[] {instruction[36]});
                var to = steps.SingleOrDefault(s => s.Letter == toLetter);
                if (to == null)
                {
                    to = new Step(new string(new[] {instruction[36]}));
                    steps.Add(to);
                }

                from.To.Add(to);
                to.From.Add(from);
            }

            return steps;
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

        private class Worker
        {
            public Step Step { get; set; }
            public int RemainingType { get; set; }
        }
    }
}