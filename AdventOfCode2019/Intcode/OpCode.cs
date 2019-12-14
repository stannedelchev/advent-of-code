using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal interface IOpCode
    {
        public IntCodeComputerState Execute(in IntCodeComputer computer);
    }

    internal readonly ref struct OpCode
    {
        public readonly long Type;
        public readonly long InstructionPointer;
        public readonly (ArgumentMode mode, long value) Argument1;
        public readonly (ArgumentMode mode, long value) Argument2;
        public readonly (ArgumentMode mode, long value) Argument3;
        public readonly (ArgumentMode mode, long value) Argument4;
        public readonly (ArgumentMode mode, long value) Argument5;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public OpCode(long type,
            long instructionPointer,
            (ArgumentMode mode, long value) argument1 = default,
            (ArgumentMode mode, long value) argument2 = default,
            (ArgumentMode mode, long value) argument3 = default,
            (ArgumentMode mode, long value) argument4 = default,
            (ArgumentMode mode, long value) argument5 = default)
        {
            Type = type;
            InstructionPointer = instructionPointer;
            Argument1 = argument1;
            Argument2 = argument2;
            Argument3 = argument3;
            Argument4 = argument4;
            Argument5 = argument5;
        }
    }

    internal class OpCodeSum : IOpCode
    {
        public OpCodeSum(long op1Index, long op2Index, long outputIndex)
        {
            Op1Index = op1Index;
            Op2Index = op2Index;
            OutputIndex = outputIndex;
        }

        public long Op1Index { get; }
        public long Op2Index { get; }
        public long OutputIndex { get; }
        public IntCodeComputerState Execute(in IntCodeComputer computer)
        {
            var op1 = computer[Op1Index];
            var op2 = computer[Op2Index];

            computer[OutputIndex] = op1 + op2;
            computer.InstructionPointer += 4;
            return IntCodeComputerState.Running;
        }
    }

    internal class OpCodeMultiply : IOpCode
    {
        public OpCodeMultiply(long op1Index, long op2Index, long outputIndex)
        {
            Op1Index = op1Index;
            Op2Index = op2Index;
            OutputIndex = outputIndex;
        }

        public long Op1Index { get; }
        public long Op2Index { get; }
        public long OutputIndex { get; }
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