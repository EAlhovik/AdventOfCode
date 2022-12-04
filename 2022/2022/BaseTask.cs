using System;
using System.Collections.Generic;

abstract class BaseTask<T> where T : struct
{
    private readonly T expectedPart1Test;
    private readonly T expectedPart2Test;

    public BaseTask(T expectedPart1Test, T expectedPart2Test)
    {
        this.expectedPart1Test = expectedPart1Test;
        this.expectedPart2Test = expectedPart2Test;
    }

    public void Part1(string day)
    {
        var testResult = SolvePart1(Utils.GetTestLines(day));
        var result = SolvePart1(Utils.GetInputLines(day));

        Console.WriteLine(
            "Day {0} Part 1 = {1} with result {2}",
            day,
            testResult.Equals(expectedPart1Test) ? "Passed" : "FAILED",
            result);
    }

    public void Part2(string day)
    {
        var testResult = SolvePart2(Utils.GetTestLines(day));
        var result = SolvePart2(Utils.GetInputLines(day));

        Console.WriteLine(
            "Day {0} Part 2 = {1} with result {2}",
            day,
            testResult.Equals(expectedPart2Test) ? "Passed" : "FAILED",
            result);
    }

    public abstract T SolvePart1(List<string> input);

    public abstract T SolvePart2(List<string> input);
}
