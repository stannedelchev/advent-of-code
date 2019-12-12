using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal class IntCodeComputer
    {
        private readonly long[] memory;
        private readonly LinkedList<long> outputs;

        private int instructionPointer;

        public IntCodeComputer(int memorySize)
        {
            this.memory = new long[memorySize];
            this.instructionPointer = 0;
            this.Input = new Queue<long>();
            this.State = IntCodeComputerState.InitialState;
            this.outputs = new LinkedList<long>();
        }

        public Queue<long> Input { get; }

        public event EventHandler<long> Output = (_, __) => { };

        public IEnumerable<long> Outputs => this.outputs;

        public long this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get => this.memory[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            set => this.memory[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Initialize(in long[] program)
        {
            Array.Copy(program, this.memory, program.Length);
            this.State = IntCodeComputerState.Initialized;
            this.instructionPointer = 0;
            this.Input.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void ExecuteProgram(bool keepState = false)
        {
            if (!keepState)
            {
                this.instructionPointer = 0;
                this.Input.Clear();
            }

            this.State = IntCodeComputerState.Running;

            while (true)
            {
                var length = OpCodeDecoder.MaxOpCodeArgumentLength + 1;
                var nextMemory = this.memory[this.instructionPointer..(this.instructionPointer + length)];
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

                this.instructionPointer = (int)opResult.InstructionPointer;

                if (opResult.OutputIndex != -1)
                {
                    this.memory[opResult.OutputIndex] = opResult.ArithmeticResult;
                }

                if (opResult.NewState == IntCodeComputerState.Outputting)
                {
                    this.outputs.AddLast(opResult.ArithmeticResult);
                    this.Output.Invoke(this, opResult.ArithmeticResult);
                }

                if (opResult.HasNewState && opResult.NewState != IntCodeComputerState.Outputting)
                {
                    this.State = opResult.NewState;
                }

                if (this.State == IntCodeComputerState.Halted ||
                    this.State == IntCodeComputerState.WaitingForInput)
                {
                    return;
                }
            }
        }

        public IntCodeComputerState State { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpSum(in OpCode opCode)
        {
            var op1 = this.GetValue(opCode.Argument1);
            var op2 = this.GetValue(opCode.Argument2);
            var resultIndex = opCode.Argument3.value;

            return new InstructionResult(opCode.InstructionPointer + 4, op1 + op2, resultIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpMultiply(in OpCode opCode)
        {
            var op1 = this.GetValue(opCode.Argument1);
            var op2 = this.GetValue(opCode.Argument2);
            var resultIndex = opCode.Argument3.value;

            return new InstructionResult(opCode.InstructionPointer + 4, op1 * op2, resultIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpHalt(in OpCode opCode)
        {
            return new InstructionResult(opCode.InstructionPointer, 0, -1, IntCodeComputerState.Halted);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpInput(in OpCode opCode)
        {
            if (this.Input.TryDequeue(out var value))
            {
                return new InstructionResult(opCode.InstructionPointer + 2, value, opCode.Argument1.value);
            }

            return new InstructionResult(opCode.InstructionPointer, -1, -1, IntCodeComputerState.WaitingForInput);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpOutput(in OpCode opCode)
        {
            var value = this.GetValue(opCode.Argument1);
            return new InstructionResult(opCode.InstructionPointer + 2, value, -1, IntCodeComputerState.Outputting);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpJumpIfTrue(in OpCode opCode)
        {
            var value = this.GetValue(opCode.Argument1);

            var instructionPointer = opCode.InstructionPointer + 3;
            if (value != 0)
            {
                instructionPointer = this.GetValue(opCode.Argument2);
            }

            return new InstructionResult(instructionPointer, 0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpJumpIfFalse(in OpCode opCode)
        {

            var value = this.GetValue(opCode.Argument1);

            var instructionPointer = opCode.InstructionPointer + 3;
            if (value == 0)
            {
                instructionPointer = this.GetValue(opCode.Argument2);
            }

            return new InstructionResult(instructionPointer, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpLessThan(in OpCode opCode)
        {
            var op1 = this.GetValue(opCode.Argument1);
            var op2 = this.GetValue(opCode.Argument2);

            var resultIndex = opCode.Argument3.value;
            var result = op1 < op2 ? 1 : 0;
            return new InstructionResult(opCode.InstructionPointer + 4, result, resultIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private InstructionResult OpEquals(in OpCode opCode)
        {
            var op1 = this.GetValue(opCode.Argument1);
            var op2 = this.GetValue(opCode.Argument2);

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

        private string DumpCore()
        {
            return string.Join(" ", this.memory);
        }
    }
}
