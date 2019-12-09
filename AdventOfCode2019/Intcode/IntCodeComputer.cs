using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal readonly ref struct InstructionResult
    {
        public readonly long InstructionPointer;
        public readonly long ArithmeticResult;
        public readonly long OutputIndex;
        public readonly bool ShouldHalt;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public InstructionResult(long instructionPointer, long arithmeticResult, long outputIndex = -1, bool shouldHalt = false)
        {
            InstructionPointer = instructionPointer;
            ArithmeticResult = arithmeticResult;
            OutputIndex = outputIndex;
            ShouldHalt = shouldHalt;
        }
    }

    internal class IntCodeComputer
    {
        private readonly long[] memory;
        private readonly Queue<long> input;
        private readonly List<long> outputs;

        private int instructionPointer;

        public IntCodeComputer(int memorySize)
        {
            this.memory = new long[memorySize];
            this.input = new Queue<long>(128);
            this.outputs = new List<long>(128);
            this.instructionPointer = 0;
        }

        public IEnumerable<long> Output => this.outputs;

        public long this[int index]
        {
            get => this.memory[index];
            set => this.memory[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Initialize(long[] program)
        {
            Array.Copy(program, this.memory, program.Length);
        }

        public void QueueInput(long input)
        {
            this.input.Enqueue(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void ExecuteProgram()
        {
            this.instructionPointer = 0;

            while (true)
            {
                var length = OpCodeDecoder.MaxOpCodeArgumentLength + 1;
                var nextMemory = memory[this.instructionPointer..(this.instructionPointer + length)];
                var opCode = OpCodeDecoder.Decode(nextMemory, this.instructionPointer);
                var opResult = opCode.Type switch
                {
                    1 => this.OpSum(opCode),
                    2 => this.OpMultiply(opCode),
                    3 => this.OpInput(opCode),
                    4 => this.OpOutput(opCode),
                    5 => this.OpJumpIfTrue(opCode),
                    6 => this.OpJumpIfFalse(opCode),
                    7 => this.OpLessThan(opCode),
                    8 => this.OpEquals(opCode),
                    99 => this.OpHalt(opCode),
                    _ => default
                };

                if (opResult.OutputIndex != -1)
                {
                    this.memory[opResult.OutputIndex] = opResult.ArithmeticResult;
                }

                if (opResult.ShouldHalt)
                {
                    return;
                }

                this.instructionPointer = (int)opResult.InstructionPointer;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpSum(in OpCode opCode)
        {
            var op1 = GetValue(opCode.Argument1);
            var op2 = GetValue(opCode.Argument2);
            var resultIndex = opCode.Argument3.value;

            return new InstructionResult(opCode.InstructionPointer + 4, op1 + op2, resultIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpMultiply(in OpCode opCode)
        {
            var op1 = GetValue(opCode.Argument1);
            var op2 = GetValue(opCode.Argument2);
            var resultIndex = opCode.Argument3.value;

            return new InstructionResult(opCode.InstructionPointer + 4, op1 * op2, resultIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpHalt(in OpCode opCode)
        {
            return new InstructionResult(opCode.InstructionPointer, 0, -1, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpInput(in OpCode opCode)
        {
            var input = this.input.Dequeue();
            return new InstructionResult(opCode.InstructionPointer + 2, input, opCode.Argument1.value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpOutput(in OpCode opCode)
        {
            var value = GetValue(opCode.Argument1);
            this.outputs.Add(value);
            return new InstructionResult(opCode.InstructionPointer + 2, 0, -1, value != 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpJumpIfTrue(in OpCode opCode)
        {
            var value = GetValue(opCode.Argument1);

            var instructionPointer = opCode.InstructionPointer + 3;
            if (value != 0)
            {
                instructionPointer = GetValue(opCode.Argument2);
            }

            return new InstructionResult(instructionPointer, 0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpJumpIfFalse(in OpCode opCode)
        {

            var value = GetValue(opCode.Argument1);

            long instructionPointer = opCode.InstructionPointer + 3;
            if (value == 0)
            {
                instructionPointer = GetValue(opCode.Argument2);
            }

            return new InstructionResult(instructionPointer, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpLessThan(in OpCode opCode)
        {
            var op1 = GetValue(opCode.Argument1);
            var op2 = GetValue(opCode.Argument2);

            var resultIndex = opCode.Argument3.value;
            var result = op1 < op2 ? 1 : 0;
            return new InstructionResult(opCode.InstructionPointer + 4, result, resultIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpEquals(in OpCode opCode)
        {
            var op1 = GetValue(opCode.Argument1);
            var op2 = GetValue(opCode.Argument2);

            var resultIndex = opCode.Argument3.value;
            var result = op1 == op2 ? 1 : 0;
            return new InstructionResult(opCode.InstructionPointer + 4, result, resultIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private long GetValue(in (ArgumentMode mode, long value) argument)
        {
            var (mode, value) = argument;
            return mode == ArgumentMode.Positional
                ? this.memory[value]
                : value;
        }
    }
}
