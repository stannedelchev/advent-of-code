using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeInput : IOpCode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IOpCode From(in long outputIndex)
        {
            OutputIndex = outputIndex;
            return this;
        }

        public long OutputIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            if (computer.Input.TryDequeue(out var value))
            {
                computer[OutputIndex] = value;
                computer.InstructionPointer += 2;
                return IntCodeComputerState.Running;
            }

            return IntCodeComputerState.WaitingForInput;
        }
    }
}