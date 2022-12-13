using System.Runtime.CompilerServices;
using AdventOfCode.Shared;
using MoreLinq;

namespace AdventOfCode2022.Day05;

internal class Problem : IProblem
{
    public string Part1(string[] input)
    {
        const int stackDefinitionCharLength = 4;
        var stacksCount = (int)Math.Ceiling(input[0].Length / (decimal)stackDefinitionCharLength);
        var stackDefinitions = input[..input.TakeWhile(i => i.IndexOf('[') != -1).Count()];
        var stacks = Enumerable.Range(0, stacksCount)
                               .Select(_ => new FixedSizeUnsafeStack<char>(stacksCount * stackDefinitions.Length))
                               .ToArray();

        for (var i = 0; i < stacksCount; i++)
        {
            foreach (var crate in stackDefinitions.Reverse().Select(l => l[i * stackDefinitionCharLength + 1]).Where(c => c != ' '))
            {
                stacks[i].Push(crate);
            }
        }

        var movesLineIndex = input.Index()
                                  .SkipWhile(s => !string.IsNullOrEmpty(s.Value.Trim()))
                                  .Skip(1)
                                  .First().Key;

        for (var i = movesLineIndex; i < input.Length; i++)
        {
            var line = input[i].AsSpan();
            var moves = int.Parse(NextNumber(line, 0, out var nextIndex));
            var start = int.Parse(NextNumber(line, nextIndex, out nextIndex));
            var end = int.Parse(NextNumber(line, nextIndex, out nextIndex));

            for (var m = 0; m < moves; m++)
            {
                var startStack = stacks[start - 1];
                var endStack = stacks[end - 1];
                startStack.MoveTop(endStack);
            }
        }

        return string.Join(string.Empty, stacks.Select(s => s.Pop()));
    }

    public string Part2(string[] input)
    {
        const int stackDefinitionCharLength = 4;
        var stacksCount = (int)Math.Ceiling(input[0].Length / (decimal)stackDefinitionCharLength);
        var stackDefinitions = input[..input.TakeWhile(i => i.IndexOf('[') != -1).Count()];
        var stacks = Enumerable.Range(0, stacksCount)
                               .Select(_ => new FixedSizeUnsafeStack<char>(stacksCount * stackDefinitions.Length))
                               .ToArray();

        for (var i = 0; i < stacksCount; i++)
        {
            foreach (var crate in stackDefinitions.Reverse().Select(l => l[i * stackDefinitionCharLength + 1]).Where(c => c != ' '))
            {
                stacks[i].Push(crate);
            }
        }

        var movesLineIndex = input.Index()
                                  .SkipWhile(s => !string.IsNullOrEmpty(s.Value.Trim()))
                                  .Skip(1)
                                  .First().Key;

        for (var i = movesLineIndex; i < input.Length; i++)
        {
            var line = input[i].AsSpan();
            var moves = int.Parse(NextNumber(line, 0, out var nextIndex));
            var start = int.Parse(NextNumber(line, nextIndex, out nextIndex));
            var end = int.Parse(NextNumber(line, nextIndex, out nextIndex));

            var startStack = stacks[start - 1];
            var endStack = stacks[end - 1];
            startStack.MoveTopRange(endStack, moves);
        }

        return string.Join(string.Empty, stacks.Select(s => s.Pop()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ReadOnlySpan<char> NextNumber(in ReadOnlySpan<char> s, int startIndex, out int endIndex)
    {
        while (startIndex < s.Length && !char.IsAsciiDigit(s[startIndex])) startIndex++;
        endIndex = startIndex;
        while (endIndex < s.Length && char.IsAsciiDigit(s[endIndex])) endIndex++;

        return s[startIndex..endIndex];
    }
}

internal class FixedSizeUnsafeStack<T>
{
    private readonly T[] store;

    private int index;

    public FixedSizeUnsafeStack(int capacity = 128)
    {
        store = new T[capacity];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Push(T item)
    {
        store[index] = item;
        index++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Pop()
    {
        index--;
        var result = store[index];
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveTop(in FixedSizeUnsafeStack<T> other)
    {
        var item = Pop();
        other.Push(item);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveTopRange(in FixedSizeUnsafeStack<T> other, int items)
    {
        foreach (var item in store[(index - items)..index])
        {
            other.store[other.index] = item;
            other.index++;
        }

        index -= items;
    }

    public override string ToString()
    {
        return string.Join("", store);
    }
}
