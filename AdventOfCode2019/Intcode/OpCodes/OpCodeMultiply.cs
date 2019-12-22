using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeMultiply : IOpCode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IOpCode From(in long op1Index, in long op2Index, in long outputIndex)
        {
            Op1Index = op1Index;
            Op2Index = op2Index;
            OutputIndex = outputIndex;
            return this;
        }

        public long Op1Index;
        public long Op2Index;
        public long OutputIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            var op1 = computer[Op1Index];
            var op2 = computer[Op2Index];

            computer[OutputIndex] = op1 * op2;
            computer.InstructionPointer += 4;
            return IntCodeComputerState.Running;
        }
    }
}