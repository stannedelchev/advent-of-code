using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeEquals : IOpCode
    {
        public OpCodeEquals(long op1Index, long op2Index, long op3Index)
        {
            Op1Index = op1Index;
            Op2Index = op2Index;
            Op3Index = op3Index;
        }

        public long Op1Index { get; }
        public long Op2Index { get; }
        public long Op3Index { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            computer[Op3Index] = computer[Op1Index] == computer[Op2Index] ? 1 : 0;
            computer.InstructionPointer += 4;
            return IntCodeComputerState.Running;
        }
    }
}