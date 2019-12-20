using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeJumpIfTrue : IOpCode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public OpCodeJumpIfTrue(in long op1Index, in long op2Index)
        {
            Op1Index = op1Index;
            Op2Index = op2Index;
        }

        public long Op1Index { get; }
        public long Op2Index { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            if (computer[Op1Index] != 0)
            {
                computer.InstructionPointer = computer[Op2Index];
            }
            else
            {
                computer.InstructionPointer += 3;
            }

            return IntCodeComputerState.Running;
        }
    }
}
