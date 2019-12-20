using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeInput : IOpCode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public OpCodeInput(in long outputIndex)
        {
            OutputIndex = outputIndex;
        }

        public long OutputIndex { get; }

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