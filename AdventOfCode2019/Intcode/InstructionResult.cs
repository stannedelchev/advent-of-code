using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal readonly ref struct InstructionResult
    {
        public readonly long InstructionPointer;
        public readonly long ArithmeticResult;
        public readonly long OutputIndex;
        public readonly bool HasNewState;
        public readonly IntCodeComputerState NewState;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public InstructionResult(long instructionPointer, long arithmeticResult, long outputIndex = -1, IntCodeComputerState newState = IntCodeComputerState.NotSet)
        {
            this.InstructionPointer = instructionPointer;
            this.ArithmeticResult = arithmeticResult;
            this.OutputIndex = outputIndex;
            this.NewState = newState;
            this.HasNewState = newState != IntCodeComputerState.NotSet;
        }
    }
}