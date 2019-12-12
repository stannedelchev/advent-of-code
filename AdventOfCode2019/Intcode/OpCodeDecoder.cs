using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal static class OpCodeDecoder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static OpCode Decode(in long[] memory, in long instructionPointer)
        {
            var encodedOpCode = memory[instructionPointer];
            var opCode = new OpCode(
                encodedOpCode % 100,
                instructionPointer,
                argument1: ((ArgumentMode)(encodedOpCode / 100 % 10), memory[instructionPointer + 1]),
                argument2: ((ArgumentMode)(encodedOpCode / 1000 % 10), memory[instructionPointer + 2]),
                argument3: ((ArgumentMode)(encodedOpCode / 10000 % 10), memory[instructionPointer + 3]),
                argument4: ((ArgumentMode)(encodedOpCode / 100000 % 10), memory[instructionPointer + 4]),
                argument5: ((ArgumentMode)(encodedOpCode / 1000000 % 10), memory[instructionPointer + 5])
            );
            return opCode;
        }
    }
}