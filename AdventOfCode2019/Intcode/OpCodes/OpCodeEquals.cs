using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeEquals : IOpCode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IOpCode From(in long op1Index, in long op2Index, in long op3Index)
        {
            Op1Index = op1Index;
            Op2Index = op2Index;
            Op3Index = op3Index;
            return this;
        }

        public long Op1Index;
        public long Op2Index;
        public long Op3Index;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            computer[Op3Index] = computer[Op1Index] == computer[Op2Index] ? 1 : 0;
            computer.InstructionPointer += 4;
            return IntCodeComputerState.Running;
        }
    }
}