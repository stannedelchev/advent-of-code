using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal class OpCodeDecoder
    {
        private readonly Memory<long> memory;

        public OpCodeDecoder(in Memory<long> memory)
        {
            this.memory = memory;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IOpCode Decode(in long instructionPointer)
        {
            var encodedOpCode = memory[instructionPointer];
            var type = encodedOpCode % 100;
            var opCode1 = encodedOpCode switch
            {
                1 => OpCodeSum(ref instructionPointer),
                2 => OpCodeMultiply(),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
                //new OpCode(
                //    type,
                //    instructionPointer,
                //    argument1: ((ArgumentMode) (encodedOpCode / 100 % 10), memory[instructionPointer + 1]),
                //    argument2: ((ArgumentMode) (encodedOpCode / 1000 % 10), memory[instructionPointer + 2]),
                //    argument3: ((ArgumentMode) (encodedOpCode / 10000 % 10), memory[instructionPointer + 3]),
                //    argument4: ((ArgumentMode) (encodedOpCode / 100000 % 10), memory[instructionPointer + 4]),
                //    argument5: ((ArgumentMode) (encodedOpCode / 1000000 % 10), memory[instructionPointer + 5])
                //)
            };
            return opCode1;
        }

        private IOpCode OpCodeSum(ref long instructionPointer)
        {
            var instruction = this.memory[instructionPointer];
        }

        private IOpCode OpCodeMultiply()
        {

        }
    }
}