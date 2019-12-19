using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AdventOfCode2019.Intcode;
using Xunit;

namespace AdventOfCode.Tests
{
    public class IntCodeComputerTests
    {
        [Fact]
        public void When_OpCode109AndPositiveOffsets_WorksCorrectly()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109,6,109,7,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(13, computer.RelativeBase);
        }

        [Fact]
        public void When_OpCode109AndNegativeOffsets_WorksCorrectly()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109,-6,109,-7,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(-13, computer.RelativeBase);
        }

        [Fact]
        public void When_OpCode109AndMixedOffsets_WorksCorrectly()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109,-6,109,16,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(10, computer.RelativeBase);
        }

        [Fact]
        public void Day9_Example_Quine()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Collection(computer.Outputs,
                l => Assert.Equal(109, l),
                l => Assert.Equal(1, l),
                l => Assert.Equal(204, l),
                l => Assert.Equal(-1, l),
                l => Assert.Equal(1001, l),
                l => Assert.Equal(100, l),
                l => Assert.Equal(1, l),
                l => Assert.Equal(100, l),
                l => Assert.Equal(1008, l),
                l => Assert.Equal(100, l),
                l => Assert.Equal(16, l),
                l => Assert.Equal(101, l),
                l => Assert.Equal(1006, l),
                l => Assert.Equal(101, l),
                l => Assert.Equal(0, l),
                l => Assert.Equal(99, l));
        }

        [Fact]
        public void Day9_Example_LargeNumber()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("1102,34915192,34915192,7,4,7,99,0");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1219070632396864L, computer.Outputs.Last());
        }

        [Fact]
        public void Day9_Example_LargeNumber2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("104,1125899906842624,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1125899906842624, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109,-1,4,1,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(-1, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109, -1, 104, 1, 99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase3()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109, -1, 204, 1, 99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(109, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase4()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109, 1, 9, 2, 204, -6, 99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(204, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase5()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109, 1, 109, 9, 204, -6, 99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(204, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase6()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109, 1, 209, -1, 204, -106, 99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(204, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase7()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109, 1, 3, 3, 204, 2, 99");
            computer.Initialize(program);
            computer.Input.Enqueue(17);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(17, computer.Outputs.Last());
        }

        [Fact]
        public void Reddit_TestCase8()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("109, 1, 203, 2, 204, 2, 99");
            computer.Initialize(program);
            computer.Input.Enqueue(18);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(18, computer.Outputs.Last());
        }

        [Fact]
        public void Day2_Example1()
        {

            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("1,0,0,0,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(new long[] { 2, 0, 0, 0, 99 }, computer.Memory.Select(kvp => kvp.Value));

        }

        [Fact]
        public void Day2_Example2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("2,3,0,3,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(new long[] { 2, 3, 0, 6, 99 }, computer.Memory.Select(kvp => kvp.Value));
        }

        [Fact]
        public void Day2_Example3()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("2,4,4,5,99,0");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(new long[] { 2, 4, 4, 5, 99, 9801 }, computer.Memory.Select(kvp => kvp.Value));
        }

        [Fact]
        public void Day2_Example4()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("1,1,1,4,99,5,6,0,99");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(new long[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 }, computer.Memory.Select(kvp => kvp.Value));
        }

        [Fact]
        public void Day5_Example1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("1101,100,-1,4,0");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(new long[] { 1101, 100,-1,4,99 }, computer.Memory.Select(kvp => kvp.Value));
        }

        [Fact]
        public void Day5_Example2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("1101,100,-1,4,0");
            computer.Initialize(program);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(new long[] { 1101, 100, -1, 4, 99 }, computer.Memory.Select(kvp => kvp.Value));
        }

        [Fact]
        public void Day5_Example3_1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,9,8,9,10,9,4,9,99,-1,8");
            computer.Initialize(program);
            computer.Input.Enqueue(8);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example3_2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,9,8,9,10,9,4,9,99,-1,8");
            computer.Initialize(program);
            computer.Input.Enqueue(0);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(0, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example4_1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,9,7,9,10,9,4,9,99,-1,8");
            computer.Initialize(program);
            computer.Input.Enqueue(1);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example4_2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,9,7,9,10,9,4,9,99,-1,8");
            computer.Initialize(program);
            computer.Input.Enqueue(9);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(0, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example5_1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,3,1108,-1,8,3,4,3,99");
            computer.Initialize(program);
            computer.Input.Enqueue(8);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example6_1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.Initialize(program);
            computer.Input.Enqueue(0);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(0, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example6_2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.Initialize(program);
            computer.Input.Enqueue(5);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example7_1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.Initialize(program);
            computer.Input.Enqueue(5);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example7_2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.Initialize(program);
            computer.Input.Enqueue(5);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example8_1()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.Initialize(program);
            computer.Input.Enqueue(7);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(999, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example8_2()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.Initialize(program);
            computer.Input.Enqueue(8);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1000, computer.Outputs.Last());
        }

        [Fact]
        public void Day5_Example8_3()
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.Initialize(program);
            computer.Input.Enqueue(9);
            computer.ExecuteProgram();

            Assert.Equal(IntCodeComputerState.Halted, computer.State);
            Assert.Equal(1001, computer.Outputs.Last());
        }
    }
}
