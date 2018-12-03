using System.Threading.Tasks;

namespace AdventOfCode
{
    public interface IPuzzle
    {
        Task<int> RunPart1(string input);
        Task<int> RunPart2(string input);
        int Day { get; }
    }
}