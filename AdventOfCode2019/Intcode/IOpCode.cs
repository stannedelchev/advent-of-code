namespace AdventOfCode2019.Intcode
{
    internal interface IOpCode
    {
        public IntCodeComputerState Execute(in IntCodeComputer computer);
    }
}