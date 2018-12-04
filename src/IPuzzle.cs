using System.Threading.Tasks;

namespace AdventOfCode
{
    public interface IPuzzle
    {
        int Day { get; }
        object RunPart1(string[] input);
        object RunPart2(string[] input);
    }
}