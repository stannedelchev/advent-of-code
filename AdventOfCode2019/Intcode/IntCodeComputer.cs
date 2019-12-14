using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal class IntCodeComputer
    {
        private readonly Memory<long> memory;
        private readonly LinkedList<long> outputs;

        public IntCodeComputer()
        {
            this.memory = new Memory<long>();
            this.InstructionPointer = 0;
            this.RelativeBase = 0;
            this.Input = new Queue<long>();
            this.State = IntCodeComputerState.InitialState;
            this.outputs = new LinkedList<long>();
        }

        public Queue<long> Input { get; }

        public event EventHandler<long> Output = (_, __) => { };

        public long InstructionPointer {get; internal set; }

        public long RelativeBase { get; internal set; }

        public IEnumerable<long> Outputs => this.outputs;

        public long this[long index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get => this.memory[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            set => this.memory[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Initialize(in long[] program)
        {
            this.memory.Clear();
            for (var i = 0; i < program.Length; i++)
            {
                this.memory[i] = program[i];
            }

            this.State = IntCodeComputerState.Initialized;
            this.InstructionPointer = 0;
            this.Input.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void ExecuteProgram()
        {
            this.State = IntCodeComputerState.Running;

            while (true)
            {
                var opCode = OpCodeDecoder.Decode(this.memory, this.InstructionPointer);
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
                    9 => this.OpAdjustRelativeBase(opCode),
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
        private InstructionResult OpAdjustRelativeBase(in OpCode opCode)
        {
            var value = GetValue(opCode.Argument1);
            this.relativeBase += (int)value;
            return new InstructionResult(opCode.InstructionPointer + 2,0);
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
                var outputIndex = GetValue(opCode.Argument1);
                return new InstructionResult(opCode.InstructionPointer + 2, value, outputIndex);
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

        private string DumpCore()
        {
            return string.Join(" ", this.memory);
        }
    }
}
