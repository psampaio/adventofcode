using System.Threading.Tasks;

namespace AdventOfCode
{
    public interface IPuzzle
    {
        int Day { get; }
        Task<object> RunPart1(string input);
        Task<object> RunPart2(string input);
    }
}