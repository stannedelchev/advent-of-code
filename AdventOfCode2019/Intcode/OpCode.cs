namespace AdventOfCode2019.Intcode
{
    internal readonly ref struct OpCode
    {
        public readonly long Type;
        public readonly long InstructionPointer;
        public readonly (ArgumentMode mode, long value) Argument1;
        public readonly (ArgumentMode mode, long value) Argument2;
        public readonly (ArgumentMode mode, long value) Argument3;
        public readonly (ArgumentMode mode, long value) Argument4;
        public readonly (ArgumentMode mode, long value) Argument5;

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
}