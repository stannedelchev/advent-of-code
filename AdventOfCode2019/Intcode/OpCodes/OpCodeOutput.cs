using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal class OpCodeOutput : IOpCode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IOpCode From(in long op1Index)
        {
            Op1Index = op1Index;
            return this;
        }

        public long Op1Index;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            var value = computer[Op1Index];
            computer.InstructionPointer += 2;
            computer.AppendOutput(value);
            return IntCodeComputerState.Outputting;
        }
    }
}