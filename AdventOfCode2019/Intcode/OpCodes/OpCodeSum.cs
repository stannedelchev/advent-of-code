using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeSum : IOpCode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public OpCodeSum(in long op1Index, in long op2Index, in long outputIndex)
        {
            Op1Index = op1Index;
            Op2Index = op2Index;
            OutputIndex = outputIndex;
        }

        public long Op1Index { get; }
        public long Op2Index { get; }
        public long OutputIndex { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            var op1 = computer[Op1Index];
            var op2 = computer[Op2Index];

            computer[OutputIndex] = op1 + op2;
            computer.InstructionPointer += 4;
            return IntCodeComputerState.Running;
        }
    }
}